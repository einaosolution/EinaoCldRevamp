
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

import { NewPatentComponent } from '../new-patent/new-patent.component';
import { PatentAppealUnitComponent } from '../patent-appeal-unit/patent-appeal-unit.component';


import {AuthGuard} from '../auth.guard';



import { SharedModule } from '../shared.module';
import { Routes, RouterModule } from '@angular/router';
import {NgBusyModule} from 'ng-busy';
import { BsDatepickerModule } from 'ngx-bootstrap';
import { AlertModule } from 'ngx-bootstrap';
import { BrowserModule } from '@angular/platform-browser';
import { NewPatent2Component } from '../new-patent2/new-patent2.component';
import { InventorComponent } from '../inventor/inventor.component';
import { PatentSubmittedApplicationComponent } from '../patent-submitted-application/patent-submitted-application.component';
import { PatentfreshapplictionComponent } from '../patentfreshappliction/patentfreshappliction.component';
import { ExaminerPatentFreshComponent } from '../examiner-patent-fresh/examiner-patent-fresh.component';
import { AcceptancepatentLetterComponent } from '../acceptancepatent-letter/acceptancepatent-letter.component';
import { RefusalPatentLetterComponent } from '../refusal-patent-letter/refusal-patent-letter.component';
import { PatentKivComponent } from '../patent-kiv/patent-kiv.component';
import { PatentReconductSearchComponent } from '../patent-reconduct-search/patent-reconduct-search.component';
import { PatentAppealRefusalComponent } from '../patent-appeal-refusal/patent-appeal-refusal.component';
import { PatentReceiveAppealComponent } from '../patent-receive-appeal/patent-receive-appeal.component';
import { PatentReceiveAppel2Component } from '../patent-receive-appel2/patent-receive-appel2.component';
import { PayPatentCertificateComponent } from '../pay-patent-certificate/pay-patent-certificate.component';
import { NoticePatentCertificatePaymentComponent } from '../notice-patent-certificate-payment/notice-patent-certificate-payment.component';
import { PatentPaidCertificaeComponent } from '../patent-paid-certificae/patent-paid-certificae.component';
import { PatentCertificateComponent } from '../patent-certificate/patent-certificate.component';
import { PatentconfirmcertificateComponent } from '../patentconfirmcertificate/patentconfirmcertificate.component';



const routes: Routes = [

  {
    path:  'NewPatent',
    component:  NewPatentComponent
    } ,
    {
      path:  'NewPatent2',
      component:  NewPatent2Component
      } ,
      {
        path:  'SubmittedApplication',
        component:  PatentSubmittedApplicationComponent
        }

        ,
      {
        path:  'FreshApplication',
        component:  PatentfreshapplictionComponent
        }


        ,
      {
        path:  'ExaminerFresh',
        component:  ExaminerPatentFreshComponent
        }

        ,
        {
          path:  'AcceptanceptanceLetter',
          component:  AcceptancepatentLetterComponent
          }

          ,
          {
            path:  'RefusalPatentLetter',
            component:  RefusalPatentLetterComponent
            }


            ,
            {
              path:  'PatentKiv',
              component:  PatentKivComponent
              }

              ,
              {
                path:  'PatentReconductSearch',
                component:  PatentReconductSearchComponent
                }

                ,
              {
                path:  'PatentAppealRefusal',
                component:  PatentAppealRefusalComponent
                }


                ,
                {
                  path:  'DelegateAppeal',
                  component:   PatentReceiveAppealComponent
                  }

                  ,
                  {
                    path:  'TreatAppeal',
                    component:   PatentAppealUnitComponent
                    }

                    ,
                  {
                    path:  'ReceiveAppeal',
                    component:   PatentReceiveAppel2Component
                    }

                    ,
                    {
                      path:  'PayPatentCertificate',
                      component:   PayPatentCertificateComponent
                      }


                      ,
                      {
                        path:  'NoticePatentCertificatePayment',
                        component:   NoticePatentCertificatePaymentComponent
                        }


                        ,
                        {
                          path:  'PatentPaidCertificate',
                          component:    PatentPaidCertificaeComponent
                          }


                          ,
                          {
                            path:  'PatentCertificate',
                            component:    PatentCertificateComponent
                            }


                            ,
                          {
                            path:  'Patentconfirmcertificate',
                            component:    PatentconfirmcertificateComponent
                            }

                            ,
                            {
                              path:  'PatentCertificate',
                              component:    PatentCertificateComponent
                              }











];

@NgModule({
  declarations: [

    NewPatentComponent,

    NewPatent2Component,

    InventorComponent,

    PatentSubmittedApplicationComponent,

    PatentfreshapplictionComponent,

    ExaminerPatentFreshComponent,

    AcceptancepatentLetterComponent,

    RefusalPatentLetterComponent,

    PatentKivComponent,

    PatentReconductSearchComponent,

    PatentAppealRefusalComponent,

    PatentReceiveAppealComponent ,
    PatentAppealUnitComponent,
    PatentReceiveAppel2Component,
    PayPatentCertificateComponent,
    NoticePatentCertificatePaymentComponent,
    PatentPaidCertificaeComponent,
    PatentCertificateComponent,
    PatentconfirmcertificateComponent




   ],
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
export class PatentModule { }
