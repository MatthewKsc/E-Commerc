import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Brand } from '../models/brands';
import { Pagination } from '../models/pagination';
import { ProductType } from '../models/ProductType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if(shopParams.brandId){
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if(shopParams.typeId){
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if(shopParams.sort){
      params = params.append('sort', shopParams.sort);
    }

    return this.http.get<Pagination>(this.baseUrl + 'products', {observe: 'response', params})
    .pipe(
      map(response =>{
        return response.body;
      })
    );
  }

  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl + 'brands');
  }

  getTypes(){
    return this.http.get<ProductType[]>(this.baseUrl + 'types');
  }
}
