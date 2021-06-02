import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { cwd } from 'process';
import { Order } from 'src/app/models/order';
import { BreadcrumbService } from 'xng-breadcrumb';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit {

  order: Order;
  total: number;

  constructor(private route: ActivatedRoute, private beadcrumbService: BreadcrumbService, private orderService: OrderService) { 
    this.beadcrumbService.set('@OrderDetailed', '')
  }

  ngOnInit(): void {
    this.orderService.getOrderDetailed(+this.route.snapshot.paramMap.get('id')).subscribe((order: Order)=>{
      this.order = order;
      this.total = this.order.shippingPrice + this.order.subtotal;
      this.beadcrumbService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);
    }, error=>{
      console.log(error);
    })
  }

}
