// src/app/services/redsys-payment.service.ts (Ya deberías tener esto)

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { purchaseRequestDto } from '../models/purchaseRequestDto.model';
import { redsysPaymentDataDto } from '../models/RedsysPaymentDataDto.model';

@Injectable({
  providedIn: 'root'
})
export class RedsysPaymentService {
  private apiUrl = 'https://localhost:7153/api/Payment';

  constructor(private http: HttpClient) { }

  generateRedsysPaymentData(productId: string, quantity: number, userMail: string): Observable<redsysPaymentDataDto> {
    const requestBody: purchaseRequestDto = { productId, quantity, userMail };
    return this.http.post<redsysPaymentDataDto>(`${this.apiUrl}/GenerateRedsysData`, requestBody)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    // Aquí puedes mejorar el manejo de errores para el usuario
    let errorMessage = 'Error desconocido al procesar el pago.';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error del cliente: ${error.error.message}`;
    } else {
      errorMessage = `Error del servidor: ${error.status} - ${error.message || error.statusText}`;
      if (error.error && error.error.errors) {
        // Para errores de validación de ASP.NET Core
        Object.values(error.error.errors).forEach((val: any) => {
          errorMessage += `\n- ${val.join(', ')}`;
        });
      }
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
