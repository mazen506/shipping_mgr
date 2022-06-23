import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NgNetInterceptor } from '../helpers/ngnet-interceptor';
import { Customer } from '../models/customer';

const apiPath = "api/customer/";
@Injectable({
  providedIn: "root"
})
export class CustomerService {
  
  constructor(private http: NgNetInterceptor
                ) { }

  public list():Observable<Customer[]> {
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
