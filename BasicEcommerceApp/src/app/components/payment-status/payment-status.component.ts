import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IOrder } from '../../models/order.model';
import { catchError, combineLatest, map, of, switchMap } from 'rxjs';
import { OrderService } from '../../services/order.service';
import { CommonModule, CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-payment-status',
  imports: [CommonModule, CurrencyPipe],
  templateUrl: './payment-status.component.html',
  styleUrl: './payment-status.component.css'
})
export class PaymentStatusComponent {

  statusType: string | null = null;
  orderId: string | null = null;
  order: IOrder | null = null;
  isLoading: boolean = true;
  errorMessage: string | null = null;
  isSuccessStatus: boolean = false;
  isErrorStatus: boolean = false;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private orderService: OrderService
  ) { }


  ngOnInit() {
    combineLatest([
      this.activatedRoute.paramMap,    // Para el parámetro de la ruta (e.g., :statusType que será 'pago-exito')
      this.activatedRoute.queryParamMap // Para los parámetros de consulta (e.g., ?orderId=)
    ]).pipe(
      map(([paramMap, queryParamMap]) => {
        // Recuperar los parámetros de la URL
        // Aquí 'statusType' será 'pago-exito' o 'pago-error'
        this.statusType = paramMap.get('statusType');
        this.orderId = queryParamMap.get('orderId');

        // Establecer los flags de estado para el template
        this.isSuccessStatus = this.statusType === 'pago-exito';
        this.isErrorStatus = this.statusType === 'pago-error';

        // Resetear estados
        this.isLoading = true;
        this.errorMessage = null;
        this.order = null;

        console.log(`URL Path Status: ${this.statusType}, Query Order ID: ${this.orderId}`);

        return this.orderId;
      }),
      switchMap(orderId => {
        if (orderId) {
          return this.orderService.getOrderById(orderId).pipe(
            catchError(err => {
              console.error('Error al recuperar el pedido:', err);
              this.errorMessage = 'No se pudo cargar la información del pedido. Por favor, contacta con soporte.';
              this.isLoading = false;
              return of(null);
            })
          );
        } else {
          this.errorMessage = 'ID de pedido no encontrado en la URL.';
          this.isLoading = false;
          return of(null);
        }
      })
    ).subscribe(order => {
      this.order = order;
      this.isLoading = false;

      if (!order && this.orderId && !this.errorMessage) {
        this.errorMessage = 'El pedido solicitado no existe o no se pudo cargar.';
      }

      console.log('Estado de pago:', this.statusType);
      console.log('ID de Pedido:', this.orderId);
      console.log('Detalles del Pedido:', this.order);
    });
  }

  goToHomePage() {
    this.router.navigate(['/'])
  }
}
