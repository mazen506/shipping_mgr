import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NgNetInterceptor } from '../helpers/ngnet-interceptor';
import { Item } from '../models/item';
import { Order } from '../models/order';

const apiPath = "api/item/";
@Injectable({
  providedIn: "root"
})
export class ItemService {
  
  constructor(private http: NgNetInterceptor
                ) { }

  public list():Observable<Item[]> {
    return this.http.get( apiPath + "list");
  }

  public create(data: any) {
    return this.http.post( apiPath + "create", data);
  }

  public delete(id) {
    let url = apiPath + "delete?id=" + id;
    return this.http.delete(url);
  }

  public update(data: any) {
    let url = apiPath + "update";
    return this.http.update(url, data);
  }

  public getById(id: any) {
    let url = apiPath +  "GetById?id=" + id;
    return this.http.get(url);
  }
  
}

