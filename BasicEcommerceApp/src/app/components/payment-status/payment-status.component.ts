import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-payment-status',
  imports: [],
  templateUrl: './payment-status.component.html',
  styleUrl: './payment-status.component.css'
})
export class PaymentStatusComponent {

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

}
