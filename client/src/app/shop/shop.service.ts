import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Brand } from '../models/brands';
import { Pagination } from '../models/pagination';
import { ProductType } from '../models/ProductType';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(brandId?: number, typeId?: number, sort?: string) {
    let params = new HttpParams();

    if(brandId){
      params = params.append('brandId', brandId.toString());
    }

    if(typeId){
      params = params.append('typeId', typeId.toString());
    }

    if(sort){
      params = params.append('sort', sort);
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
