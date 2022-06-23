import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NgNetInterceptor } from '../helpers/ngnet-interceptor';
import { Order } from '../models/order';

const apiPath = "api/order/";
@Injectable({
  providedIn: "root"
})
export class OrderService {
  
  private _order = new Order();
  constructor(private http: NgNetInterceptor
                ) { }

  public list():Observable<Order[]> {
    return this.http.get( apiPath + "list");
  }

  get order(){
    return this._order;
  }

  set order(data){
    this._order = data;
  }

  public create() {
    console.log(this.order.items);
    return this.http.post( apiPath + "create", this.order);
  }

  public delete(id) {
    let url = apiPath + "?id=" + id;
    return this.http.delete(url);
  }

  public update() {
    let url = apiPath;
    return this.http.update(url, this.order);
  }

  public getById(id: any) {
    let url = apiPath +  "GetById?id=" + id;
    return this.http.get(url);
  }
  
}

