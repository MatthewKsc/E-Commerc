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
  shopParams: ShopParams;
  totalCount: number;
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ]

  constructor(private shopService: ShopService) {
    this.shopParams = this.shopService.getShopParams();
  }

  ngOnInit(): void {
    this.getProducts(true);
    this.getBrands();
    this.getTypes();
  }

  getProducts(useCache = false){
    this.shopService.getProducts(useCache).subscribe(response => {
      this.products = response.items;
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
    const params = this.shopService.getShopParams();
    params.brandId = brandId;
    params.pageIndex = 1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    const params = this.shopService.getShopParams();
    params.typeId = typeId;
    params.pageIndex =1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onSortSelected(sort: string){
    const params = this.shopService.getShopParams();
    params.sort = sort;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onPageChanged(event: any){
    const params = this.shopService.getShopParams();
    if(params.pageIndex !== event){
      params.pageIndex = event;
      this.shopService.setShopParams(params);
      this.getProducts(true);
    }
  }

  onSearch(){
    const params = this.shopService.getShopParams();
    params.search = this.searchTerm.nativeElement.value;
    params.pageIndex =1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.shopService.setShopParams(this.shopParams);
    this.getProducts();
  }
}
