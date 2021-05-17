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
  brandIdSelected: number = 0;
  typeIdSelected: number = 0;
  sortSelected = 'name';
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ]

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe(response => {
      this.products = response.items;
    }, error => {
      console.log(error);
    });
  }

  getBrands(){
    this.shopService.getBrands().subscribe(response=>{
      this.brands = [{id:0, name:'All'}, ...response];
    }, error=>{
      console.log(error);
    });
  }

  getTypes(){
    this.shopService.getTypes().subscribe(response=>{
      this.types =  [{id:0, name:'All'}, ...response];
    }, error=>{
      console.log(error);
    });
  }

  onBrandSelected(brandId: number){
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string){
    this.sortSelected = sort
    this.getProducts();
  }
}
