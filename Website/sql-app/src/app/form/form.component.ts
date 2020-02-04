import { Component, OnInit } from '@angular/core';
import { SqlOperationService } from '../Services/sql-operation.service';
import { Sqlform } from '../Services/sqlform.model';
import { FormBuilder } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { GenerateQueryRes } from '../Models/generate-query-res';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  hero: string = "TEST"
  data: Sqlform
  Inputform = new Sqlform();
  GenerateQueryRes: GenerateQueryRes;
  submitted;

  generateSQL() {
    this.service.GenerateSQL(this.Inputform)
      .subscribe(response => {
        this.GenerateQueryRes = new GenerateQueryRes(response);
        debugger;
      }, err => this.showError(err))
    this.submitted = true;
  }
  executeSQL() {
    alert("executing sql")
  }

  constructor(private service: SqlOperationService) {
  }


  ngOnInit() {
    this.GenerateQueryRes = new GenerateQueryRes(null);
  }

  showError(error: any) {
    var message = error.error.title;
    alert(message);
  }

}

/**
 * class res
 */
