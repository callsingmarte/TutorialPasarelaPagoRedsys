import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { OrderDetailsComponent } from './components/order-details/order-details.component';
import { PaymentStatusComponent } from './components/payment-status/payment-status.component';
import { OrdersComponent } from './components/orders/orders.component';

export const routes: Routes = [
  { path: '', component: ProductListComponent, pathMatch: 'full' },
  { path: 'product/:id', component: ProductDetailsComponent },
  { path: 'payment-status/:statusType', component: PaymentStatusComponent },
  { path: 'my-orders', component: OrdersComponent },
  { path: 'order-details/:id', component: OrderDetailsComponent },
];
