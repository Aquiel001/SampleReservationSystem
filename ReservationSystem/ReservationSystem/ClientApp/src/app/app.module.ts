import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ListReservationComponent } from './list-reservation/list-reservation.component';
import { FormReservationComponent } from './form-reservation/form-reservation.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularMyDatePickerModule } from 'angular-mydatepicker';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {isBoolean} from 'util';

let maskConfigFunction;
maskConfigFunction = false;

@NgModule({
  declarations: [
    AppComponent,
    ListReservationComponent,
    FormReservationComponent,
    NavMenuComponent
  ],
  imports: [
    NgxMaskModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AngularEditorModule,
    NgbModule,
    ReactiveFormsModule,
    FormsModule,
    AngularMyDatePickerModule,
    BrowserAnimationsModule
  ],
  providers: [
    HttpClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
