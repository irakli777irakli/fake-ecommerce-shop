import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'http://localhost:5000/api/';
  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    // query string builder
    let params = new HttpParams();
    
    if(shopParams.brandId !== 0) {
      // ?brandId=1
      params = params.append('brandId',shopParams.brandId.toString());
    }

    if(shopParams.typeId !== 0) {
      // &typeId=2
      params = params.append('typeId',shopParams.typeId.toString());
    }

    if(shopParams.search) {
      params = params.append('search',shopParams.search);
    }
    
    
      // &sort=name
      params = params.append('sort',shopParams.sort);

      // pagination info
      params = params.append('pageIndex',shopParams.pageNumber.toString());
      params = params.append('pageSize',shopParams.pageSize.toString());


    // gives `Http` of response not body
    return this.http.get<IPagination>(
      `${this.baseUrl}products`,
    {
      observe:'response',
      params
    }).pipe(
      map(response => {
        // extracting body of IPagination
        return response.body;
      })
    )
    
  }

  getBrands() {
    return this.http.get<IBrand[]>(`${this.baseUrl}products/brands`);
  }

  getTypes() {
    return this.http.get<IType[]>(`${this.baseUrl}products/types`);
  }

}
