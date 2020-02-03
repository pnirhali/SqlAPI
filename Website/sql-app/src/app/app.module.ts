import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormComponent } from './form/form.component';
import { SqlOperationService } from './Services/sql-operation.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ExecuterComponent } from './executer/executer.component';

@NgModule({
  declarations: [
    AppComponent,
    FormComponent,
    ExecuterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
    
  ],
  providers: [SqlOperationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
