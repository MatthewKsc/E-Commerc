import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Brand } from '../models/brands';
import { Product } from '../models/product';
import { ProductType } from '../models/ProductType';
import { ShopParams } from '../models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  products: Product[];
  brands: Brand[];
  types: ProductType[];
  shopParams = new ShopParams();
  totalCount: number;
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
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.items;
      this.shopParams.pageIndex  = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.totalItemsCount;
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
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex =1;
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageIndex =1;
    this.getProducts();
  }

  onSortSelected(sort: string){
    this.shopParams.sort = sort
    this.getProducts();
  }

  onPageChanged(event: any){
    if(this.shopParams.pageIndex !== event){
      this.shopParams.pageIndex = event;
      this.getProducts();
    }
  }

  onSearch(){
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
