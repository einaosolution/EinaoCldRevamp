
import { NgModule , CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
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
import { PatentUserApplicationComponent } from '../patent-user-application/patent-user-application.component';
import { PatentSearchKivComponent } from '../patent-search-kiv/patent-search-kiv.component';
import { Invoice3Component } from '../invoice3/invoice3.component';
import { PatentRegistraCertificateComponent } from '../patent-registra-certificate/patent-registra-certificate.component';
import { Patentregistracertificate2Component } from '../patentregistracertificate2/patentregistracertificate2.component';
import { PatentPreviewComponent } from '../patent-preview/patent-preview.component';
import { AcknowledgementPatentComponent } from '../acknowledgement-patent/acknowledgement-patent.component';
import { RefusalPatentReprintComponent } from '../refusal-patent-reprint/refusal-patent-reprint.component';
import { AcceptancepatentreprintLetterComponent } from '../acceptancepatentreprint-letter/acceptancepatentreprint-letter.component';
import { NewpatentmigrationComponent } from '../newpatentmigration/newpatentmigration.component';
import { PatentmigrationapplictionComponent } from '../patentmigrationappliction/patentmigrationappliction.component';
import { RenewPatentComponent } from '../renew-patent/renew-patent.component';
import { PayRenewalPatentComponent } from '../pay-renewal-patent/pay-renewal-patent.component';
import { GenRecordalRenewPatentComponentComponent } from '../gen-recordal-renew-patent-component/gen-recordal-renew-patent-component.component';
import { GenRecordalRenewPatent2ComponentComponent } from '../gen-recordal-renew-patent2-component/gen-recordal-renew-patent2-component.component';



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
          path:  'AcceptanceptanceReprintLetter',
          component:  AcceptancepatentreprintLetterComponent
          }

          ,
          {
            path:  'RefusalPatentLetter',
            component:  RefusalPatentLetterComponent
            }


            ,
            {
              path:  'Newpatentmigration',
              component:  NewpatentmigrationComponent
              }



            ,
            {
              path:  'RefusalReprinLetter',
              component:  RefusalPatentReprintComponent
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
                  path:  'PatentTreatMigratedApp',
                  component:  PatentmigrationapplictionComponent
                  }

,
                  {
                    path:  'PayRenewal',
                    component:   PayRenewalPatentComponent
                    }


                ,
                {
                  path:  'AcknowledgementPrint',
                  component:    AcknowledgementPatentComponent
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
                      path:  'RenewPatent',
                      component:    RenewPatentComponent
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


                              ,
                              {
                                path:  'PatentUserApplication',
                                component:    PatentUserApplicationComponent
                                }


                                ,
                              {
                                path:  'GenRecordalRenewPatent',
                                component:    GenRecordalRenewPatentComponentComponent
                                }


                                ,
                                {
                                  path:  'TreatedRenewal',
                                  component:    GenRecordalRenewPatent2ComponentComponent
                                  }


                                ,
                                {
                                  path:  'PatentSearchKiv',
                                  component:    PatentSearchKivComponent
                                  }

                                  ,
                                {
                                  path:  'Invoice',
                                  component:    Invoice3Component
                                  }


                                  ,
                                  {
                                    path:  'PatentCertificatePayment',
                                    component:    PatentRegistraCertificateComponent
                                    }

                                    ,
                                    {
                                      path:  'PatentCertificateExaminePayment',
                                      component:    Patentregistracertificate2Component
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
    PatentconfirmcertificateComponent,
    PatentUserApplicationComponent,
    PatentSearchKivComponent,
    Invoice3Component,
    PatentRegistraCertificateComponent,
    Patentregistracertificate2Component,
    PatentPreviewComponent,
    AcknowledgementPatentComponent,
    RefusalPatentReprintComponent,
    AcceptancepatentreprintLetterComponent,
    NewpatentmigrationComponent,
    PatentmigrationapplictionComponent,
    RenewPatentComponent,
    PayRenewalPatentComponent,
    GenRecordalRenewPatentComponentComponent,
    GenRecordalRenewPatent2ComponentComponent




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
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ] ,
  providers: [],

})
export class PatentModule { }
