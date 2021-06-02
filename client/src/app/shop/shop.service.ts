import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Brand } from '../models/brands';
import { Pagination, PaginationCache } from '../models/pagination';
import { ProductType } from '../models/ProductType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../models/shopParams';
import { Product } from '../models/product';
import { from, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';
  products: Product[] =[];
  brands: Brand[] =[];
  types: ProductType[] =[];
  pagination = new PaginationCache();
  shopParams = new ShopParams();
  productCache = new Map();

  constructor(private http: HttpClient) { }

  getProducts(useCache: boolean) {

    if(useCache === false){
      this.productCache = new Map();
    }

    if(this.productCache.size>0 && useCache === true){
      if(this.productCache.has(Object.values(this.shopParams).join('-'))){
        this.pagination.items = this.productCache.get(Object.values(this.shopParams).join('-'));
        return of(this.pagination);
      }
    }

    let params = new HttpParams();

    if(this.shopParams.brandId !==0){
      params = params.append('brandId', this.shopParams.brandId.toString());
    }

    if(this.shopParams.typeId !==0){
      params = params.append('typeId', this.shopParams.typeId.toString());
    }

    if(this.shopParams.search){
      params = params.append('search', this.shopParams.search);
    }
    
    params = params.append('sort', this.shopParams.sort);
    params = params.append('pageIndex', this.shopParams.pageIndex.toString());
    params = params.append('pageSize', this.shopParams.pageSize.toString());

    return this.http.get<Pagination>(this.baseUrl + 'products', {observe: 'response', params})
    .pipe(
      map(response =>{
        this.productCache.set(Object.values(this.shopParams).join('-'), response.body.items);
        this.pagination = response.body;
        return this.pagination;
      })
    );
  }

  setShopParams(params: ShopParams){
    this.shopParams = params;
  }

  getShopParams(){
    return this.shopParams;
  }

  getProduct(id: number){
    let product: Product;
    this.productCache.forEach((products: Product[])=>{
      product = products.find(p=> p.id === id);
    })

    if(product){
      return of(product);
    }

    return this.http.get<Product>(this.baseUrl +'products/' + id);
  }

  getBrands(){
    if(this.brands.length>0){
      return of(this.brands);
    }

    return this.http.get<Brand[]>(this.baseUrl + 'brands').pipe(
      map(response => {
        this.brands = response;
        return response;
      })
    );
  }

  getTypes(){
    if(this.types.length>0){
      return of(this.types);
    }

    return this.http.get<ProductType[]>(this.baseUrl + 'types').pipe(
      map(response => {
        this.types = response;
        return response;
      })
    );
  }
}
