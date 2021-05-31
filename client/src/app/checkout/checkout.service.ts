import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../models/DeliveryMethod';
import { OrderToCreate } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  OrderId: number;
  private baseUrl = environment.apiUrl;
  private deliveryUrl = this.baseUrl+'order/deliveryMethods';

  constructor(private http: HttpClient) { }

  createOrder(order: OrderToCreate){
    return this.http.post(this.baseUrl+'order', order);
  }

  getDeliveryMethods(){
    return this.http.get(this.deliveryUrl).pipe(
      map((dm: DeliveryMethod[]) =>{
        return dm.sort((a,b) => b.price - a.price);
      })
    );
  }
}
