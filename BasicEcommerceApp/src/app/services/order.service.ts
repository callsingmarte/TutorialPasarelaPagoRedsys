import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IOrder } from '../models/order.model';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl = 'https://localhost:7153/api/Orders'
  constructor(private http: HttpClient) { }

  getOrderByUserMail(userMail: string): Observable<IOrder[]> {
    return this.http.get<IOrder[]>(`${this.apiUrl}/user/${userMail}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  getOrderById(orderId: string): Observable<IOrder> {
    return this.http.get<IOrder>(`${this.apiUrl}/${orderId}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateOrderById(orderId: string, order: IOrder) {
    const url = `${this.apiUrl}/${orderId}`;

    return this.http.put<IOrder>(url, order)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Se ha producido un error desconocido!';
    if (error.error instanceof ErrorEvent) {
      console.error('Error del cliente:', error.error.message);
      errorMessage = `Error: ${error.error.message}`;
    } else {
      console.error(
        `Error del backend ${error.status}, ` +
        `Cuerpo del error: ${error.error}`);
      errorMessage = `error De servidor: ${error.status} - ${error.message || error.statusText}`;
      if (error.error && typeof error.error === 'string') {
        errorMessage += `\nDetalles: ${error.error}`;
      } else if (error.error && error.error.errors) {
        errorMessage += '\n Errores de Validacion:';
        for (const key in error.error.errors) {
          if (error.error.errors.hasOwnProperty(key)) {
            errorMessage += `\n  ${key}: ${error.error.errors[key].join(', ')}`;
          }
        }
      }
    }
    return throwError(() => new Error(errorMessage));
  }
}
