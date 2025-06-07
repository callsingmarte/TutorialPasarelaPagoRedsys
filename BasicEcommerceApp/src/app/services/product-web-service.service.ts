import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { IProduct } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductWebServiceService {
  private apiUrl = 'https://localhost:7153/api/Products'
  constructor(private http: HttpClient) { }

  getProducts(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  getProductById(id: string): Observable<IProduct> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<IProduct>(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  putProductoById(id: string, product: IProduct) {
    const url = `${this.apiUrl}/${id}`;

    return this.http.put<IProduct>(url, product)
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
