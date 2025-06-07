import { Component } from '@angular/core';
import { ProductWebServiceService } from '../../services/product-web-service.service';
import { IProduct } from '../../models/product.model';
import { catchError, of } from 'rxjs';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ModalCompraComponent } from '../modal-compra/modal-compra.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule, RouterModule, CurrencyPipe, ModalCompraComponent, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent {
  public products: IProduct[] = [];
  public errorMessage: string | null = null;
  public showPurchaseModal: boolean = false;
  public selectedProduct: IProduct | null = null;
  public customerEmail: string = '';
  constructor(
    private router: Router,
    private productWebService: ProductWebServiceService
  ) { }

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

  isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }

  searchOrders(): void {
    if (this.customerEmail && this.isValidEmail(this.customerEmail)) {
      this.router.navigate(['/my-orders'], { queryParams: { email: this.customerEmail } });
    } else {
      console.warn('Intento de búsqueda con correo electrónico inválido o vacío.');
    }
  }
}
