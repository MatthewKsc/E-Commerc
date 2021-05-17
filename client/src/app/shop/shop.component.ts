import { Component, OnInit } from '@angular/core';
import { Brand } from '../models/brands';
import { Product } from '../models/product';
import { ProductType } from '../models/ProductType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: Product[];
  brands: Brand[];
  types: ProductType[];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts().subscribe(response => {
      this.products = response.items;
    }, error => {
      console.log(error);
    });
  }

  getBrands(){
    this.shopService.getBrands().subscribe(response=>{
      this.brands = response;
    }, error=>{
      console.log(error);
    });
  }

  getTypes(){
    this.shopService.getTypes().subscribe(response=>{
      this.types = response;
    }, error=>{
      console.log(error);
    });
  }
}
