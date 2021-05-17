import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Brand } from '../models/brands';
import { Pagination } from '../models/pagination';
import { ProductType } from '../models/ProductType';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts() {
    return this.http.get<Pagination>(this.baseUrl + 'products?pageSize=50');
  }

  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl + 'brands');
  }

  getTypes(){
    return this.http.get<ProductType[]>(this.baseUrl + 'types');
  }
}
