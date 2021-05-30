import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { DeliveryMethod } from 'src/app/models/DeliveryMethod';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {

  @Input() checkoutForm: FormGroup;
  deliveryMethods : DeliveryMethod[];

  constructor(private checkoutService: CheckoutService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe((dm: DeliveryMethod[])=>{
      this.deliveryMethods = dm;
    }, error=>{
      console.log(error);
    })
  }

}
