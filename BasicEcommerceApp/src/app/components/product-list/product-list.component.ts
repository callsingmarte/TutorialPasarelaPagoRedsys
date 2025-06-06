import { Component } from '@angular/core';
import { ProductWebServiceService } from '../../services/product-web-service.service';
import { IProduct } from '../../models/product.model';
import { catchError, of } from 'rxjs';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ModalCompraComponent } from '../modal-compra/modal-compra.component';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule, RouterModule, CurrencyPipe, ModalCompraComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent {
  public products: IProduct[] = [];
  public errorMessage: string | null = null;
  public showPurchaseModal: boolean = false;
  public selectedProduct: IProduct | null = null;
  constructor(private productWebService: ProductWebServiceService) { }

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productWebService.getProducts().pipe(
      catchError((error) => {
        console.error(error);
        this.errorMessage = "No se pudieron cargar los productos. Inténtalo de nuevo más tarde."
        return of([]);
      })
    ).subscribe({
      next: (products) => {
        this.products = products;
        console.log(products);
        this.errorMessage = null;
      }
    })
  }

  openPurchaseModal(product: IProduct): void {
    if (product) {
      this.selectedProduct = product;
      this.showPurchaseModal = true;
    }
  }

  handleModalClose(): void {
    this.showPurchaseModal = false;
    this.selectedProduct = null;
  }
}
