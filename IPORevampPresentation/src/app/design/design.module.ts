
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';


import { AppRoutingModule } from '../app-routing.module';



import { HashLocationStrategy, LocationStrategy } from '@angular/common';



import { HttpClientModule } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import {CalendarModule} from 'primeng/calendar';



import { NgxSpinnerModule } from 'ngx-spinner';


import { NgxEditorModule } from 'ngx-editor';

import { ModalModule } from 'ngx-bootstrap';

import { AccordionModule } from 'ngx-bootstrap';

import { DataTablesModule } from 'angular-datatables';

import { NgxSummernoteModule } from 'ngx-summernote';

import { InternationalPhoneNumberModule } from 'ngx-international-phone-number';
import { PasswordStrengthBarModule } from 'ng2-password-strength-bar';
import { DeviceDetectorModule } from 'ngx-device-detector';

import { NgxQRCodeModule } from 'ngx-qrcode2';



import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';




import {AuthGuard} from '../auth.guard';



import { SharedModule } from '../shared.module';
import { Routes, RouterModule } from '@angular/router';
import {NgBusyModule} from 'ng-busy';
import { BsDatepickerModule } from 'ngx-bootstrap';
import { AlertModule } from 'ngx-bootstrap';
import { BrowserModule } from '@angular/platform-browser';
import { NnewDesignComponent } from '../nnew-design/nnew-design.component';
import { Invoice4Component } from '../invoice4/invoice4.component';
import { DesignFreshApplicationComponent } from '../design-fresh-application/design-fresh-application.component';
import { ExaminerDesignFreshComponent } from '../examiner-design-fresh/examiner-design-fresh.component';
import { AcceptancedesignLetterComponent } from '../acceptancedesign-letter/acceptancedesign-letter.component';
import { RefusalDesignLetterComponentComponent } from '../refusal-design-letter-component/refusal-design-letter-component.component';
import { DesignRegistraCertificateComponent } from '../design-registra-certificate/design-registra-certificate.component';




const routes: Routes = [

  {
    path:  'newDesign',
    component:   NnewDesignComponent
    } ,
    {
      path:  'Invoice',
      component:   Invoice4Component
      }
      ,
      {
        path:  'FreshApplication',
        component:   DesignFreshApplicationComponent
        }

        ,
      {
        path:  'ExaminerFresh',
        component:   ExaminerDesignFreshComponent
        }

        ,
        {
          path:  'AcceptanceptanceLetter',
          component:  AcceptancedesignLetterComponent
          }

          ,
          {
            path:  'RefusalDesignLetter',
            component:  RefusalDesignLetterComponentComponent
            }

            ,
          {
            path:  'DesignCertificatePayment',
            component:  DesignRegistraCertificateComponent
            }








];

@NgModule({
  declarations: [
   NnewDesignComponent,
   Invoice4Component,
   DesignFreshApplicationComponent,
   ExaminerDesignFreshComponent,
   AcceptancedesignLetterComponent,
   RefusalDesignLetterComponentComponent,
   DesignRegistraCertificateComponent],
  imports: [
    CommonModule ,
    SharedModule,


       RouterModule.forChild(routes) ,


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







  ],
  providers: [],

})
export class DesignModule { }
