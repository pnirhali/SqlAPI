import { Injectable } from '@angular/core';
import { Sqlform } from './sqlform.model';
import { HttpClient, } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { HttpHeaders, HttpClientModule } from '@angular/common/http';
let headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Access-Control-Allow-Origin': '*',
  'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, PUT, PATCH, DELETE'
});
let options = {
  headers: headers
}
@Injectable({
  providedIn: 'root'
})



export class SqlOperationService {


  formData: Sqlform
  constructor(private httpClient: HttpClient) { }


  GenerateSQL(formData) {
    return this.httpClient.post("https://localhost:44385/api/Query/Generate", formData, options)
      .map((response: Response) => response);
  }


}


