import { NgModule, ModuleWithProviders } from '@angular/core';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {NgBusyModule} from 'ng-busy';



import { FilterPipe } from './filter.pipe';
import { FilterPipe2 } from './filter2.pipe';
import { CustomHttpInterceptorService } from './CustomHttpInterceptorService';
import {ApiClientService} from './api-client.service';
import { BsDatepickerModule } from 'ngx-bootstrap';
import { AlertModule } from 'ngx-bootstrap';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserModule } from '@angular/platform-browser';
import { NgxQRCodeModule } from 'ngx-qrcode2';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ModalModule } from 'ngx-bootstrap';
import { InternationalPhoneNumberModule } from 'ngx-international-phone-number';
import { PasswordStrengthBarModule } from 'ng2-password-strength-bar';
import { DeviceDetectorModule } from 'ngx-device-detector';
import { NgxSummernoteModule } from 'ngx-summernote';
import { DataTablesModule } from 'angular-datatables';
import { NgxEditorModule } from 'ngx-editor';
import { AccordionModule } from 'ngx-bootstrap';
import { HttpClientModule } from '@angular/common/http';

import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import {CalendarModule} from 'primeng/calendar';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

import { LogoutComponent } from './logout/logout.component';

import { TaskComponent } from './task/task.component';

import { ViewUserComponent } from './view-user/view-user.component';

import { ContactusComponent } from './contactus/contactus.component';

import { EmailverificationComponent } from './emailverification/emailverification.component';
 import { CorporateComponent } from './corporate/corporate.component';

import { IndividualComponent } from './individual/individual.component';

import { ForgetpasswordComponent } from './forgetpassword/forgetpassword.component';

@NgModule({
  declarations: [
    FilterPipe,
    FilterPipe2,


  ],
  imports: [
    NgBusyModule ,
    BsDatepickerModule.forRoot(),
    AlertModule.forRoot(),
    NgxSpinnerModule ,

    NgxQRCodeModule,
    NgMultiSelectDropDownModule.forRoot() ,
    ModalModule.forRoot() ,
    InternationalPhoneNumberModule ,
    PasswordStrengthBarModule,
    DeviceDetectorModule.forRoot(),
    NgxSummernoteModule,

    DataTablesModule,
    NgxEditorModule,


    AccordionModule.forRoot(),
    HttpClientModule,


    ReactiveFormsModule,

    CalendarModule,
    FormsModule ,




  ] ,
  exports: [
    FilterPipe,
    FilterPipe2,
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [ApiClientService ,{provide: HTTP_INTERCEPTORS, useClass: CustomHttpInterceptorService, multi: true}   ]
    };
  }
}
