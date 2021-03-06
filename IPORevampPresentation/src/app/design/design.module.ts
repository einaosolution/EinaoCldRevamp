
import { NgModule , CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
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
import { DesignRegistraCertificate2Component } from '../design-registra-certificate2/design-registra-certificate2.component';
import { PayDesignCertificateComponent } from '../pay-design-certificate/pay-design-certificate.component';
import { NoticeDesignCertificatePaymentComponent } from '../notice-design-certificate-payment/notice-design-certificate-payment.component';
import { DesignPaidCertificateComponentComponent } from '../design-paid-certificate-component/design-paid-certificate-component.component';
import { DesignPaidCertificate2Component } from '../design-paid-certificate2/design-paid-certificate2.component';
import { DesignCertificateComponent } from '../design-certificate/design-certificate.component';
import { DesignApplicationListingComponent } from '../design-application-listing/design-application-listing.component';
import { DesignReconductSearchComponent } from '../design-reconduct-search/design-reconduct-search.component';
import { DesignKivSearchComponent } from '../design-kiv-search/design-kiv-search.component';
import { DesignpublicationpendingComponent } from '../designpublicationpending/designpublicationpending.component';
import { PublicationBatch2Component } from '../publication-batch2/publication-batch2.component';
import { PublicationDetail2Component } from '../publication-detail2/publication-detail2.component';
import { DesignAppealRefusalComponent } from '../design-appeal-refusal/design-appeal-refusal.component';
import { DesignRegistraAppealComponent } from '../design-registra-appeal/design-registra-appeal.component';
import { DesignAppealComponent } from '../design-appeal/design-appeal.component';
import { DesignReceiveAppealComponent } from '../design-receive-appeal/design-receive-appeal.component';
import { DesignKivApplicationComponent } from '../design-kiv-application/design-kiv-application.component';
import { RenewDesignComponentComponent } from '../renew-design-component/renew-design-component.component';
import { PayRenewalDesignComponent } from '../pay-renewal-design/pay-renewal-design.component';
import { GenRecordalRenewDesignComponentComponent } from '../gen-recordal-renew-design-component/gen-recordal-renew-design-component.component';
import { DesignunplishedapplicationComponent } from '../designunplishedapplication/designunplishedapplication.component';
import { DesignpreviewComponent } from '../designpreview/designpreview.component';
import { AcknowledgementDesignComponent } from '../acknowledgement-design/acknowledgement-design.component';
import { AcceptancedesignReprintletterComponent } from '../acceptancedesign-reprintletter/acceptancedesign-reprintletter.component';
import { RefusaldesignreprintletterComponent } from '../refusaldesignreprintletter/refusaldesignreprintletter.component';
import { NewdesignmigrationComponent } from '../newdesignmigration/newdesignmigration.component';
import { DesignmigrateapplicationComponent } from '../designmigrateapplication/designmigrateapplication.component';
import { GgenRecordalRenewDesign2Component } from '../ggen-recordal-renew-design2/ggen-recordal-renew-design2.component';




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
        path:  'Newdesignmigration',
        component:   NewdesignmigrationComponent
        }



        ,
        {
          path:  'AcceptanceptanceLetter',
          component:  AcceptancedesignLetterComponent
          }


                  ,

                  {
                    path:  'AcceptancedesignReprintletter',
                    component:  AcceptancedesignReprintletterComponent
                    }


                            ,


          {
            path:  'RefusalDesignLetter',
            component:  RefusalDesignLetterComponentComponent
            }

            ,
            {
              path:  'RefusalDesignPrintLetter',
              component:  RefusaldesignreprintletterComponent
              }

              ,


          {
            path:  'DesignCertificatePayment',
            component:  DesignRegistraCertificateComponent
            }

            ,
            {
              path:  'DesignCertificateExaminePayment',
              component:  DesignRegistraCertificate2Component
              }

              ,
              {
                path:  'DesignTreatMigratedApp',
                component:  DesignmigrateapplicationComponent
                }


                 ,
              {
                path:  'PayDesignCertificate',
                component:  PayDesignCertificateComponent
                }

                ,
                {
                  path:  'NoticeDesignCertificatePayment',
                  component:   NoticeDesignCertificatePaymentComponent
                  }


                  ,
                {
                  path:  'DesignVerifyPaidCertificate',
                  component:    DesignPaidCertificateComponentComponent
                  }

                  ,
                  {
                    path:  'DesignConfirmCertificate',
                    component:    DesignPaidCertificate2Component
                    }


                  ,
                  {
                    path:  'DesignCertificate',
                    component:    DesignCertificateComponent
                    }

                    ,
                    {
                      path:  'DesignApplicationListing',
                      component:    DesignApplicationListingComponent
                      }

                      ,
                      {
                        path:  'DesignReconductSearch',
                        component:    DesignReconductSearchComponent
                        }

                        ,
                      {
                        path:  'DesignKivSearch',
                        component:    DesignKivSearchComponent
                        }


                        ,
                      {
                        path:  'Designpublicationpending',
                        component:    DesignpublicationpendingComponent
                        }


                        ,
                        {
                          path:  'PublicationBatch',
                          component:    PublicationBatch2Component
                          }

                          ,
                          {
                            path:  'PublicationDetail',
                            component:    PublicationDetail2Component
                            }

                            ,
                            {
                              path:  'DesignAppealRefusal',
                              component:    DesignAppealRefusalComponent
                              }


                              ,
                              {
                                path:  'DesignRegistraAppeal',
                                component:   DesignRegistraAppealComponent
                                }

                                ,
                              {
                                path:  'DesignAppeal',
                                component:   DesignAppealComponent
                                }

                                ,
                                {
                                  path:  'DesignReceiveAppeal',
                                  component:   DesignReceiveAppealComponent
                                  }

                                  ,
                                {
                                  path:  'AcknowledgementDesign',
                                  component:   AcknowledgementDesignComponent
                                  }





                                  ,
                                  {
                                    path:  'ExaminerKiv',
                                    component:   DesignKivApplicationComponent
                                    }

                                    ,
                                  {
                                    path:  'RenewDesign',
                                    component:   RenewDesignComponentComponent
                                    }

                                    ,
                                    {
                                      path:  'PayRenewal',
                                      component:   PayRenewalDesignComponent
                                      }


                                      ,
                                      {
                                        path:  'GenRecordalRenewDesign',
                                        component:   GenRecordalRenewDesignComponentComponent
                                        }

                                        ,
                                        {
                                          path:  'TreatedRenewal',
                                          component:   GgenRecordalRenewDesign2Component
                                          }

                                        ,
                                      {
                                        path:  'GenRecordalRenewDesign',
                                        component:   GenRecordalRenewDesignComponentComponent
                                        }

                                        ,
                                        {
                                          path:  'Designunplishedapplication',
                                          component:    DesignunplishedapplicationComponent
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
   DesignRegistraCertificateComponent,
   DesignRegistraCertificate2Component,
   PayDesignCertificateComponent,
   NoticeDesignCertificatePaymentComponent,
   DesignPaidCertificateComponentComponent,
   DesignPaidCertificate2Component,
   DesignCertificateComponent,
   DesignApplicationListingComponent,
   DesignReconductSearchComponent,
   DesignKivSearchComponent,
   DesignpublicationpendingComponent,
   PublicationBatch2Component,
   PublicationDetail2Component,
   DesignAppealRefusalComponent,
   DesignRegistraAppealComponent,
   DesignAppealComponent,
   DesignReceiveAppealComponent,
   DesignKivApplicationComponent,
   RenewDesignComponentComponent,
   PayRenewalDesignComponent,
   GenRecordalRenewDesignComponentComponent,
   DesignunplishedapplicationComponent,
   DesignpreviewComponent,
   AcknowledgementDesignComponent,
   AcceptancedesignReprintletterComponent,
   RefusaldesignreprintletterComponent,
   NewdesignmigrationComponent,
   DesignmigrateapplicationComponent,
   GgenRecordalRenewDesign2Component],
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
export class DesignModule { }
