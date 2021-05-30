import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from '../models/order';
import { OrderService } from './order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orders: Order[];

  constructor(private orderService: OrderService, private router: Router) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders(){
    this.orderService.getOrdersForUser().subscribe((reponse: Order[])=>{
      this.orders = reponse;
    }, error=>{
      console.log(error);
    });
  }

  toOrder(id: number){
    this.router.navigateByUrl('/orders/'+id)
  }
}
