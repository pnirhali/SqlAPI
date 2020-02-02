import { Injectable } from '@angular/core';
import { Sqlform } from './sqlform.model';

@Injectable({
  providedIn: 'root'
})
export class SqlOperationService {
formData : Sqlform
  constructor() { }
}
