import { Injectable } from '@angular/core';
import { Sqlform } from './sqlform.model';
import { HttpClient, } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { HttpHeaders, HttpClientModule } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { saveAs } from 'file-saver';
import { GenerateQueryEndpoint, ExecuteQueryEndpoint } from './constants';

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

  constructor(private httpClient: HttpClient) { }

  saveFile() {
    const headers = new Headers();
    headers.append('Accept', 'text/plain');
    this.httpClient.get('/api/files', options)
      .toPromise()
      .then(response => this.saveToFileSystem(response));
  }

  private saveToFileSystem(response) {
    const contentDispositionHeader: string = response.headers.get('Content-Disposition');
    const parts: string[] = contentDispositionHeader.split(';');
    const filename = parts[1].split('=')[1];
    const blob = new Blob([response._body], { type: 'text/plain' });
    saveAs(blob, filename);
  }


  GenerateSQL(formData: Sqlform) {
    return this.httpClient.post(GenerateQueryEndpoint, formData)
      .map((response: Response) => response);
  }

  ExecuteSQL(query: string) {
    return this.httpClient.post(ExecuteQueryEndpoint, { "query": query })
      .map((response: Response) => response);
  }


}


