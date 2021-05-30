import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket, BaskteItem, IBasket } from 'src/app/models/basket';

@Component({
  selector: 'app-basket-summarry',
  templateUrl: './basket-summarry.component.html',
  styleUrls: ['./basket-summarry.component.scss']
})
export class BasketSummarryComponent implements OnInit {

  basket$: Observable<Basket>
  @Output() decrement: EventEmitter<BaskteItem> = new EventEmitter<BaskteItem>();
  @Output() increment: EventEmitter<BaskteItem> = new EventEmitter<BaskteItem>();
  @Output() remove: EventEmitter<BaskteItem> = new EventEmitter<BaskteItem>();
  @Input() isBasket = true;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  decrementItemQuantity(item: BaskteItem){
    this.decrement.emit(item);
  }

  incrementItemQuantity(item: BaskteItem){
    this.increment.emit(item);
  }

  removeBasketItem(item: BaskteItem){
    this.remove.emit(item);
  }

}
