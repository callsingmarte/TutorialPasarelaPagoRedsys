// src/app/modal-compra/modal-compra.component.ts
import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { IProduct } from '../../models/product.model';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { PaymentProcessService } from '../../services/payment-process.service'; // Asegúrate de que esto sea correcto
import { RedsysPaymentService } from '../../services/redsys-payment.service';
import { FormsModule } from '@angular/forms';
import { redsysPaymentDataDto } from '../../models/RedsysPaymentDataDto.model';

@Component({
  selector: 'app-modal-compra',
  standalone: true,
  imports: [CommonModule, CurrencyPipe, FormsModule], // Asegúrate de que FormsModule esté aquí
  templateUrl: './modal-compra.component.html',
  styleUrl: './modal-compra.component.css'
})
export class ModalCompraComponent implements OnInit, OnChanges {

  @Input()
  product: IProduct | null = null;

  @Input()
  modalOpen: boolean = false;

  @Output()
  closeModal = new EventEmitter<void>();

  // Este Output ya no es estrictamente necesario para la redirección, pero lo mantenemos si lo usas para otras cosas.
  @Output()
  confirmPurchase = new EventEmitter<IProduct>();

  public redsysPaymentData: redsysPaymentDataDto = {
    ds_MerchantParameters: "",
    ds_Signature: "",
    ds_SignatureVersion: "",
    redsysTpvsUrl: ""
  };

  public quantity: number = 1; // Valor por defecto
  public userMail: string = "";

  // Estado para controlar la visibilidad del segundo paso del modal
  public orderCreatedSuccessfully: boolean = false;

  constructor(
    private redsysPaymentService: RedsysPaymentService,
    // Si PaymentProcessService no se usa directamente en este componente, puedes quitarlo
    private paymentService: PaymentProcessService // Opcional, si no lo usas aquí
  ) { }

  ngOnInit(): void {
    // Puedes añadir lógica de inicialización aquí si es necesaria
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['modalOpen'] && changes['modalOpen'].currentValue === true) {
      // Cuando el modal se abre, resetear el estado y los datos de Redsys
      this.orderCreatedSuccessfully = false;
      this.redsysPaymentData = {
        ds_MerchantParameters: "",
        ds_Signature: "",
        ds_SignatureVersion: "",
        redsysTpvsUrl: ""
      };
      this.quantity = 1; // Reiniciar cantidad
      this.userMail = ""; // Reiniciar mail
    }
  }

  // Método que se llama al hacer click en "Confirmar Pedido" (primer paso del modal)
  generateRedsysData(): void {
    if (this.product?.productId && this.quantity > 0 && this.userMail) {
      console.log('Iniciando llamada a generateRedsysPaymentData...');
      this.redsysPaymentService.generateRedsysPaymentData(this.product.productId, this.quantity, this.userMail)
        .subscribe({
          next: (redsysData) => {
            console.log('✅ Datos de Redsys recibidos del backend:', redsysData);
            this.redsysPaymentData = redsysData;
            this.orderCreatedSuccessfully = true; // Indicar que el pedido se creó y los datos están listos

            // OPCIONAL: Si quieres la redirección TOTALMENTE automática al crear el pedido,
            // descomenta la siguiente línea. Pero es mejor que el usuario haga un segundo click.
            // this.redirectToRedsys(this.redsysPaymentData);

          },
          error: (err) => {
            console.error('❌ Error al generar datos de Redsys:', err);
            // Mostrar un mensaje de error más amigable al usuario
            alert('No se pudo crear el pedido o generar los datos de pago. Por favor, inténtalo de nuevo.');
            this.orderCreatedSuccessfully = false; // Asegurarse de que no pase al siguiente paso
          }
        });
    } else {
      alert('Por favor, selecciona una cantidad y proporciona tu correo electrónico.');
    }
  }

  // Método que se llama al hacer click en "Comprar Ahora" (segundo paso del modal)
  // Este es el método que ahora REALMENTE inicia la redirección a Redsys.
  onConfirmPurchase(): void {
    if (this.redsysPaymentData.redsysTpvsUrl && this.redsysPaymentData.ds_MerchantParameters && this.redsysPaymentData.ds_Signature) {
      console.log('Iniciando redirección a Redsys...');
      this.redirectToRedsys(this.redsysPaymentData);
      // Opcional: emitir el evento si el componente padre necesita saber que la compra fue confirmada
      if (this.product) {
        this.confirmPurchase.emit(this.product);
      }
      this.onCloseModal(); // Cerrar el modal una vez iniciada la redirección
    } else {
      console.error('Datos de Redsys incompletos para la redirección. No se puede continuar.');
      alert('Error interno: Faltan datos para procesar el pago. Inténtalo de nuevo.');
    }
  }

  // Método para cerrar el modal
  onCloseModal(): void {
    this.modalOpen = false;
    this.closeModal.emit();
    // Resetear el estado y datos de Redsys al cerrar el modal
    this.orderCreatedSuccessfully = false;
    this.redsysPaymentData = {
      ds_MerchantParameters: "",
      ds_Signature: "",
      ds_SignatureVersion: "",
      redsysTpvsUrl: ""
    };
  }

  // Método para construir y enviar el formulario de redirección a Redsys programáticamente
  private redirectToRedsys(redsysData: redsysPaymentDataDto): void {
    console.log('⚙️ Construyendo y enviando formulario para redirección a Redsys...');
    const form = document.createElement('form');
    form.method = 'POST';
    form.action = redsysData.redsysTpvsUrl;
    // Opcional: para abrir en una nueva pestaña (no siempre recomendado para pasarelas de pago)
    // form.target = '_blank';

    // Crear y añadir los campos ocultos
    const signatureVersionInput = document.createElement('input');
    signatureVersionInput.type = 'hidden';
    signatureVersionInput.name = 'Ds_SignatureVersion';
    signatureVersionInput.value = redsysData.ds_SignatureVersion;
    form.appendChild(signatureVersionInput);

    const merchantParametersInput = document.createElement('input');
    merchantParametersInput.type = 'hidden';
    merchantParametersInput.name = 'Ds_MerchantParameters';
    merchantParametersInput.value = redsysData.ds_MerchantParameters;
    form.appendChild(merchantParametersInput);

    const signatureInput = document.createElement('input');
    signatureInput.type = 'hidden';
    signatureInput.name = 'Ds_Signature';
    signatureInput.value = redsysData.ds_Signature;
    form.appendChild(signatureInput);

    // Añadir el formulario al body del documento y enviarlo
    document.body.appendChild(form);
    form.submit();
    console.log('✅ Formulario enviado. Se espera redirección...');

    // Limpiar el formulario del DOM después de un breve retraso
    setTimeout(() => {
      if (document.body.contains(form)) {
        document.body.removeChild(form);
        console.log('🗑️ Formulario temporal eliminado del DOM.');
      }
    }, 100); // Pequeño retraso para asegurar el envío
  }
}
