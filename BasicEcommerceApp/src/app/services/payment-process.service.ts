import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaymentProcessService {
  private apiUrl = 'https://localhost:7153/api/Payment'

  constructor(private http: HttpClient) { }


}
