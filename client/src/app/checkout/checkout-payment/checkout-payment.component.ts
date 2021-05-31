import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket } from 'src/app/models/basket';
import { Order } from 'src/app/models/order';
import { CheckoutService } from '../checkout.service';

declare var Stripe;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements AfterViewInit, OnDestroy {

  @Input() checkoutForm: FormGroup;
  @ViewChild('cardNumber', {static: true}) cardNumberElement: ElementRef;
  @ViewChild('cardExpiry', {static: true}) cardExpiryElement: ElementRef;
  @ViewChild('cardCvc', {static: true}) carCvcElement: ElementRef;
  stripe: any;
  cardNumber: any;
  cardExpiry: any;
  cardCvc: any;
  cardErrors: any;
  cardHandler = this.onChange.bind(this);

  constructor(private basketService: BasketService, private checkoutService: CheckoutService,
              private toastr: ToastrService, private router: Router) { }

  ngAfterViewInit() {
    this.stripe = Stripe('pk_test_51IwtEvLNtZioluE0dgdIqrCc46YQQ37mCp418mb046hqqg2L9uxahYqx21CDAhz7a6DcpyMjL9EICese12FnIO5U00MuWbCrIf');
    const elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber');
    this.cardNumber.mount(this.cardNumberElement.nativeElement);
    this.cardNumber.addEventListener('change', this.cardHandler);

    this.cardExpiry = elements.create('cardExpiry');
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
    this.cardExpiry.addEventListener('change', this.cardHandler);

    this.cardCvc = elements.create('cardCvc');
    this.cardCvc.mount(this.carCvcElement.nativeElement);
    this.cardCvc.addEventListener('change', this.cardHandler);
  }

  ngOnDestroy(){
    this.cardNumber.destroy();
    this.cardExpiry.destroy();
    this.cardCvc.destroy();
  }

  onChange({error}){
    if(error){
      this.cardErrors = error.message;
    }else{
      this.cardErrors = null;
    }
  }

  submitOrder(){
    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);
    this.checkoutService.createOrder(orderToCreate).subscribe((order: Order)=>{
      this.toastr.success('Order created successfully');
      this.stripe.confirmCardPayment(basket.clientSecret,{
        payment_method: {
          card: this.cardNumber,
          billing_details: {
            name: this.checkoutForm.get('paymentForm').get('nameOnCard').value
          }
        }
      }).then(result=>{
        console.log(result);
        if(result.paymentIntent){
          this.basketService.deleteLocalBasekt(basket.id);
          const navigationsExtras: NavigationExtras= {state: order};
          this.router.navigate(['checkout/success'], navigationsExtras);
        }else{
          this.toastr.error(result.error.message);
        }
      });
      
    }, error=>{
      this.toastr.error(error.message);
      console.log(error);
    })
  }

  private getOrderToCreate(basket: Basket) {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shipToAddress: this.checkoutForm.get('addressForm').value
    };
  }
}
