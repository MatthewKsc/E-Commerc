import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BaskteItem, IBasket } from '../models/basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  basket$: Observable<IBasket>;

  constructor(private basketSerivce: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketSerivce.basket$;
  }

  removeBasketItem(item: BaskteItem){
    this.basketSerivce.removeItemFromBasket(item);
  }

  incrementItemQuantity(item: BaskteItem){
    this.basketSerivce.incrementItemQuantity(item);
  }

  decrementItemQuantity(item: BaskteItem){
    this.basketSerivce.decrementItemQuantity(item);
  }
}
