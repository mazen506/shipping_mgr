import { Injectable } from "@angular/core";
import { NgNetInterceptor } from "../helpers/ngnet-interceptor";
import { Currency } from "../models/currency";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class CurrencyService {
  constructor(private http: NgNetInterceptor
                ) { }

  public getList():Observable<Currency[]> {
    return this.http.get("api/currencies/list");
  }

  public add(data: any) {
    return this.http.post("api/currencies/add", data);
  }

  public delete(data: any) {
    let url = "api/currencies/delete/" + data;
    return this.http.delete(url);
  }

  public update(data: any) {
    let url = "api/currencies/edit/";
    return this.http.update(url, data);
  }

  public getById(id: any) {
    let url = "api/currencies/list/" + id;
    return this.http.get(url);
  }
  
}
