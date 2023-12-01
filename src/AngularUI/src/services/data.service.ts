import { HttpClient } from "@angular/common/http";
import { Inject, Injectable, InjectionToken } from "@angular/core";
import { Observable } from "rxjs";
import { DataModel } from "../app/models/DataModel";

@Injectable({
  providedIn: 'root'
})

export class DataService {
  private baseUrl: string;
  constructor(private http: HttpClient, @Inject('baseUrl') baseUrl?: string) {
    this.baseUrl = baseUrl ? baseUrl : "";
  }

  getData(endpoint: string): Observable<any> {
    return this.http.get<DataModel>(`${this.baseUrl}/${endpoint}`);
  }
}
