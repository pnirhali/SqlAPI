import { Component, OnInit } from '@angular/core';
import { SqlOperationService } from '../Services/sql-operation.service';
import { Sqlform } from '../Services/sqlform.model';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  hero: string = "TEST"
  data: Sqlform
  Inputform = new Sqlform();
  submitted;
  onSubmit() { this.submitted = true; }

  constructor(private service: SqlOperationService) {
  }
  

  ngOnInit() {
  }

}
