import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { IOrder } from '../../models/order.model';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { generateOrderPdf } from '../../utils/pdf-generator';

@Component({
  selector: 'app-orders',
  imports: [CommonModule],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {

  customerEmail: string | null = null;
  orders: IOrder[] = [];
  isLoading: boolean = true;
  errorMessage: string | null = null;
  expandedOrderId: string | null = null; // Para controlar qué pedido está expandido

  constructor(
    private activatedRoute: ActivatedRoute,
    private orderService: OrderService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParamMap.subscribe(params => {
      this.customerEmail = params.get('email');

      if (this.customerEmail) {
        this.loadOrders(this.customerEmail);
      } else {
        this.isLoading = false;
        this.errorMessage = 'No se proporcionó un correo electrónico para buscar pedidos.';
      }
    });
  }

  loadOrders(email: string): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.orders = [];

    this.orderService.getOrderByUserMail(email).subscribe({
      next: (data) => {
        this.orders = data;
        this.isLoading = false;
        if (this.orders.length === 0) {
          this.errorMessage = `No se encontraron pedidos para el correo electrónico: ${email}.`;
        }
      },
      error: (err) => {
        console.error('Error al cargar los pedidos:', err);
        this.isLoading = false;
        if (err.status === 404) {
          this.errorMessage = `No se encontraron pedidos para el correo electrónico: ${email}.`;
        } else {
          this.errorMessage = 'Hubo un error al recuperar tus pedidos. Por favor, inténtalo de nuevo más tarde.';
        }
      }
    });
  }

  toggleOrderItems(orderId: string): void {
    this.expandedOrderId = this.expandedOrderId === orderId ? null : orderId;
  }

  goToHomePage(): void {
    this.router.navigate(['/']);
  }

  downloadSingleOrder(order: IOrder): void {
    const filename = `pedido-${order.redsysOrderId || order.orderId}`;
    generateOrderPdf(order, filename);
    console.log("order:", order);
  }
}
