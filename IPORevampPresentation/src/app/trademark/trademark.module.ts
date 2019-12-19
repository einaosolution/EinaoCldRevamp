
import { NgModule , CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';


import { AppRoutingModule } from '../app-routing.module';



import { HashLocationStrategy, LocationStrategy } from '@angular/common';



import { HttpClientModule } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import {CalendarModule} from 'primeng/calendar';


import { SidebarComponent } from '../sidebar/sidebar.component';





import { Home2Component } from '../home2/home2.component';


import { NgxSpinnerModule } from 'ngx-spinner';
import { ChangePasswordComponent } from '../change-password/change-password.component';




import { CountryComponent } from '../country/country.component';
import { StateComponent } from '../state/state.component';
import { NgxEditorModule } from 'ngx-editor';
import { EmailTemplateComponent } from '../email-template/email-template.component';
import { UserSecurityComponent } from '../user-security/user-security.component';
import { LgaComponent } from '../lga/lga.component';
import { SettingsComponent } from '../settings/settings.component';
import { ModalModule } from 'ngx-bootstrap';
import { CreateRoleComponent } from '../create-role/create-role.component';

import { CreateMenuComponent } from '../create-menu/create-menu.component';
import { AssignRoleComponent } from '../assign-role/assign-role.component';
import { AccordionModule } from 'ngx-bootstrap';

import { DataTablesModule } from 'angular-datatables';
import { SectorComponent } from '../sector/sector.component';

import { FeeListComponent } from '../fee-list/fee-list.component';
import { AppStatusTmComponent } from '../app-status-tm/app-status-tm.component';
import { AppStatusPtComponent } from '../app-status-pt/app-status-pt.component';
import { AppStatusDsComponent } from '../app-status-ds/app-status-ds.component';
import { ProductComponent } from '../product/product.component';
import { DepartmentComponent } from '../department/department.component';
import { AuditComponent } from '../audit/audit.component';
import { UserAssignmentComponent } from '../user-assignment/user-assignment.component';
import { BackEndUserComponent } from '../back-end-user/back-end-user.component';
import { PendingUserComponent } from '../pending-user/pending-user.component';

import { NgxSummernoteModule } from 'ngx-summernote';
import { UnitsComponent } from '../units/units.component';
import { MinistryComponent } from '../ministry/ministry.component';
import { InternationalPhoneNumberModule } from 'ngx-international-phone-number';
import { PasswordStrengthBarModule } from 'ng2-password-strength-bar';
import { DeviceDetectorModule } from 'ngx-device-detector';
import { RemittaComponent } from '../remitta/remitta.component';
import { PremSearchComponent } from '../prem-search/prem-search.component';
import { ProductBillingComponent } from '../product-billing/product-billing.component';
import { InvoiceComponent } from '../invoice/invoice.component';
import { NewApplicationComponent } from '../new-application/new-application.component';
import { AcknowledgementComponent } from '../acknowledgement/acknowledgement.component';
import { SearchFreshAppComponent } from '../search-fresh-app/search-fresh-app.component';
import { SearchKivComponent } from '../search-kiv/search-kiv.component';
import { SearchPrelimComponent } from '../search-prelim/search-prelim.component';
import { SearchtreatedComponent } from '../searchtreated/searchtreated.component';
import { ExaminerFreshComponent } from '../examiner-fresh/examiner-fresh.component';
import { NgxQRCodeModule } from 'ngx-qrcode2';

import { AcceptanceLetterComponent } from '../acceptance-letter/acceptance-letter.component';
import { RefusalLetterComponent } from '../refusal-letter/refusal-letter.component';
import { UserKivComponent } from '../user-kiv/user-kiv.component';
import { ExaminerKivComponent } from '../examiner-kiv/examiner-kiv.component';
import { ReConductSearchComponent } from '../re-conduct-search/re-conduct-search.component';
import { ExaminerTreatedComponent } from '../examiner-treated/examiner-treated.component';
import { PublicationNewComponent } from '../publication-new/publication-new.component';
import { PublicationBatchComponent } from '../publication-batch/publication-batch.component';
import { PublicationDetailComponent } from '../publication-detail/publication-detail.component';
import { RefuseApplicationComponent } from '../refuse-application/refuse-application.component';
import { AssignAppealComponent } from '../assign-appeal/assign-appeal.component';
import { AssignAppeal2Component } from '../assign-appeal2/assign-appeal2.component';
import { ReceiveAppealComponent } from '../receive-appeal/receive-appeal.component';
import { UserOppositionComponent } from '../user-opposition/user-opposition.component';
import { NoticeOfOppositionComponent } from '../notice-of-opposition/notice-of-opposition.component';

import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { OppositionFreshComponent } from '../opposition-fresh/opposition-fresh.component';
import { UsercounterOppositionComponent } from '../usercounter-opposition/usercounter-opposition.component';
import { NoticeOfCounterOppositionComponent } from '../notice-of-counter-opposition/notice-of-counter-opposition.component';
import { UploadJudgementComponent } from '../upload-judgement/upload-judgement.component';
import { ViewJudgmentComponent } from '../view-judgment/view-judgment.component';

import { PayCertificateComponent } from '../pay-certificate/pay-certificate.component';
import { NoticeCertificatePaymentComponent } from '../notice-certificate-payment/notice-certificate-payment.component';
import { GenerateCertificateComponent } from '../generate-certificate/generate-certificate.component';
import { CertificateComponent } from '../certificate/certificate.component';
import { RenewTrademarkComponent } from '../renew-trademark/renew-trademark.component';
import { PayRenewalComponent } from '../pay-renewal/pay-renewal.component';
import { GenerateIssuedCertificateComponent } from '../generate-issued-certificate/generate-issued-certificate.component';
import { ViewrenewalComponent } from '../viewrenewal/viewrenewal.component';
import { GenRecordalRenewComponent } from '../gen-recordal-renew/gen-recordal-renew.component';
import { Invoice2Component } from '../invoice2/invoice2.component';
import { MergerTrademarkComponent } from '../merger-trademark/merger-trademark.component';
import { NoticeofmergerComponent } from '../noticeofmerger/noticeofmerger.component';
import { SearchDbComponent } from '../search-db/search-db.component';
import {AuthGuard} from '../auth.guard';
import { TrademarkApplicationComponent } from '../trademark-application/trademark-application.component';
import { ViewPreliminarySearchComponent } from '../view-preliminary-search/view-preliminary-search.component';
import { CommentsComponent } from '../comments/comments.component';

import { SharedModule } from '../shared.module';
import { Routes, RouterModule } from '@angular/router';
import {NgBusyModule} from 'ng-busy';
import { BsDatepickerModule } from 'ngx-bootstrap';
import { AlertModule } from 'ngx-bootstrap';
import { BrowserModule } from '@angular/platform-browser';
import { BackEndUserProfileComponent } from '../back-end-user-profile/back-end-user-profile.component';
import { TrademarkPreviewComponent } from '../trademark-preview/trademark-preview.component';
import { RefuseApplicationReprintComponent } from '../refuse-application-reprint/refuse-application-reprint.component';
import { AcceptanceLetterReprintComponent } from '../acceptance-letter-reprint/acceptance-letter-reprint.component';
import { NewapplicationmigrationComponent } from '../newapplicationmigration/newapplicationmigration.component';
import { SearchmigrateappComponent } from '../searchmigrateapp/searchmigrateapp.component';
import { ChangeofnametrademarkComponent } from '../changeofnametrademark/changeofnametrademark.component';
import { NoticeofchangeofnameComponent } from '../noticeofchangeofname/noticeofchangeofname.component';
import { ViewChangeOfNameComponent } from '../view-change-of-name/view-change-of-name.component';
import { GenRecordalChangeOfNameComponent } from '../gen-recordal-change-of-name/gen-recordal-change-of-name.component';
import { GenRecordalChangeOfName2Component } from '../gen-recordal-change-of-name2/gen-recordal-change-of-name2.component';
import { ChangeofnamecertificateComponent } from '../changeofnamecertificate/changeofnamecertificate.component';
import { ChangeofaddresstrademarkComponent } from '../changeofaddresstrademark/changeofaddresstrademark.component';
import { NoticeofchangeofaddressComponent } from '../noticeofchangeofaddress/noticeofchangeofaddress.component';

import { GenRecordalChangeOfAddressComponent } from '../gen-recordal-change-of-address/gen-recordal-change-of-address.component';
import { Genrecordalchangeofaddress2Component } from '../genrecordalchangeofaddress2/genrecordalchangeofaddress2.component';
import { ChangeofaddresscertificateComponent } from '../changeofaddresscertificate/changeofaddresscertificate.component';
import { GenRecordalMergerAndAssignmentComponent } from '../gen-recordal-merger-and-assignment/gen-recordal-merger-and-assignment.component';
import { GenRecordalMergerAndAssignment2Component } from '../gen-recordal-merger-and-assignment2/gen-recordal-merger-and-assignment2.component';
import { ChangeofassignmentcertificateComponent } from '../changeofassignmentcertificate/changeofassignmentcertificate.component';
import { GenRecordalRenew2Component } from '../gen-recordal-renew2/gen-recordal-renew2.component';


const routes: Routes = [
  {
    path:  'Country',
    component:  CountryComponent
    },{
      path:  'Acknowledgement',
      component:  AcknowledgementComponent
      },{
        path:  'AcceptanceLetter',
        component:  AcceptanceLetterComponent
        } ,{
          path:  'AcceptanceLetterReprint',
          component:  AcceptanceLetterReprintComponent
          } ,{
          path:  'TrademarkApplication',
          component:  TrademarkApplicationComponent
          } ,{
            path:  'ViewPreliminarySearch',
            component:  ViewPreliminarySearchComponent
            },{
          path:  'SearchDb',
          component:  SearchDbComponent
          },{
          path:  'ReceiveAppeal',
          component:  ReceiveAppealComponent
          },{
          path:  'AssignAppeal',
          component:  AssignAppealComponent
          } ,{
            path:  'PayRenewal',
            component:  PayRenewalComponent
            } ,{
              path:  'MergerTrademark',
              component:  MergerTrademarkComponent
              } ,
            {
              path:  'Invoice2',
              component:  Invoice2Component
              }

              ,
              {
                path:  'TreatMigratedApp',
                component:  SearchmigrateappComponent
                }


              ,



              {
                path:  'Noticeofchangeofname',
                component:  NoticeofchangeofnameComponent
                }


              ,


              {
                path:  'Noticeofchangeofaddress',
                component:  NoticeofchangeofaddressComponent

                }


              ,




              {
                path:  'home2',
                component:  Home2Component
                }


              ,
            {
              path:  'processinginvoice',
              component:  Invoice2Component
              },
            {
              path:  'GenRecordalRenew',
              component:  GenRecordalRenewComponent
              } , {
                path:  'TreatedRenewal',
                component:  GenRecordalRenew2Component
                } ,{
            path:  'AssignAppeal2',
            component:  AssignAppeal2Component
            },

            {
              path:  'ChangeOfName',
              component:   ChangeofnametrademarkComponent
              },





            {
              path:  'ChangeOfAddress',
              component:    ChangeofaddresstrademarkComponent
              },






            {
          path:  'ExaminerTreated',
          component:  ExaminerTreatedComponent
          } ,{
          path:  'ReConductSearch',
          component:  ReConductSearchComponent
          } ,{
            path:  'GenerateCertificate',
            component:  GenerateCertificateComponent
            } ,{
              path:  'GenerateIssuedCertificate',
              component:  GenerateIssuedCertificateComponent
              }  ,{
              path:  'Certificate',
              component:  CertificateComponent
              },{
            path:  'PublicationNew',
            component:  PublicationNewComponent
            },

            {
              path:  'ChangeOfNameBackend',
              component:  GenRecordalChangeOfNameComponent
              },

              {
                path:  'ChangeOfAddressBackend',
                component:  GenRecordalChangeOfAddressComponent
                },




              {
                path:  'MergerAssignmentBackend',
                component:  GenRecordalMergerAndAssignmentComponent
                },

              {
                path:  'TreatedChangeOfName',
                component:  GenRecordalChangeOfName2Component
                },

                {
                  path:  'TreatedChangeOfAddress',
                  component:  Genrecordalchangeofaddress2Component
                  },

                  {
                    path:  'TreatedAssignment',
                    component:  GenRecordalMergerAndAssignment2Component
                    },





            {
              path:  'RenewTrademark',
              component:  RenewTrademarkComponent
              },{
              path:  'PayCertificate',
              component:  PayCertificateComponent
              } ,{
              path:  'UserOpposition',
              component:  UserOppositionComponent
              } ,{
                path:  'UploadJudgement',
                component:  UploadJudgementComponent
                } ,{
                path:  'NoticeOfCounterOpposition',
                component:   NoticeOfCounterOppositionComponent
                } ,{
                path:  'Noticeofmerger',
                component:   NoticeofmergerComponent
                },{
                path:  'NoticeOfOpposition',
                component:  NoticeOfOppositionComponent
                }  ,{
                  path:  'NoticeCertificatePayment',
                  component:  NoticeCertificatePaymentComponent
                  } ,{
                  path:  'ViewJudgment',
                  component: ViewJudgmentComponent
                  },{
                  path:  'OppositionFresh',
                  component:  OppositionFreshComponent
                  } ,{
                    path:  'UsercounterOpposition',
                    component:  UsercounterOppositionComponent
                    } ,{
              path:  'PublicationDetail',
              component:   PublicationDetailComponent
              },{
              path:  'PublicationBatch',
              component:  PublicationBatchComponent
              } ,{
        path:  'SearchFreshApp',
        component:  SearchFreshAppComponent
        } ,{
          path:  'UserKiv',
          component:  UserKivComponent
          } ,{
            path:  'RefuseApplication',
            component:  RefuseApplicationComponent
            }  ,{
          path:  'RefusalLetter',
          component:  RefusalLetterComponent
          } ,{
            path:  'RefusalLetterReprint',
            component:  RefuseApplicationReprintComponent
            } ,{
            path:  'ExaminerKiv',
            component:  ExaminerKivComponent
            } ,{
          path:  'SearchPrelim',
          component:  SearchPrelimComponent
          },{
            path:  'ExaminerFresh',
            component:  ExaminerFreshComponent
            },{
      path:  'Staff',
      component:  BackEndUserComponent
      } ,{
        path:  'UserProfile',
        component:  BackEndUserProfileComponent
        },{
        path:  'PremSearch',
        component:  PremSearchComponent
        }
        ,{
          path:  'SearchKiv',
          component:  SearchKivComponent
          }
          ,{
            path:  'Searchtreated',
            component:  SearchtreatedComponent
            }

        ,{
          path:  'NewApplication',
          component:  NewApplicationComponent
          }

          ,{
            path:  'Changeofnamecertificate',
            component:  ChangeofnamecertificateComponent
            }

            ,{
              path:  'Changeofaddresscertificate',
              component:  ChangeofaddresscertificateComponent
              }


              ,{
                path:  'Changeofassignmentcertificate',
                component:  ChangeofassignmentcertificateComponent
                }








          ,{
          path:  'NewApplicationMigration',
          component:  NewapplicationmigrationComponent
          }

        ,{
          path:  'Invoice',
          component:  InvoiceComponent
          }

        ,{
          path:  'ProductBilling',
          component:  ProductBillingComponent
          },{
        path:  'Unit',
        component:  UnitsComponent
        },{
          path:  'Ministry',
          component:  MinistryComponent
          },{
        path:  'PendingUser',
        component:  PendingUserComponent
        } ,{
      path:  'State',
      component:  StateComponent
      } ,{
      path:  'Product',
      component:  ProductComponent
      } ,{
        path:  'Audit',
        component:  AuditComponent
        } ,

      {
        path:  'Department',
        component:  DepartmentComponent
        } ,
      {
        path:  'FeeList',
        component:  FeeListComponent
        }

      ,{
        path:  'Lga',
        component:  LgaComponent
        },{
        path:  'EmailTemplate',
        component:  EmailTemplateComponent
        }

        ,{
          path:  'Settings',
          component:  SettingsComponent
          } ,{
          path:  'Role',
          component:  CreateRoleComponent
          },{
            path:  'AssignUser',
            component:  UserAssignmentComponent
            },{
            path:  'AppStatusTm',
            component:  AppStatusTmComponent
            } ,{
              path:  'AppStatusPt',
              component:  AppStatusPtComponent
              },{
                path:  'AppStatusDs',
                component:  AppStatusDsComponent
                },{
            path:  'Menu',
            component:  CreateMenuComponent
            } ,{
              path:  'AssignRole',
              component:  AssignRoleComponent
              },{
                path:  'Sector',
                component:  SectorComponent
                }



];

@NgModule({
  declarations: [


    SidebarComponent,


    Home2Component,


    ChangePasswordComponent,

    CountryComponent,
    StateComponent,
    EmailTemplateComponent,
    UserSecurityComponent,
    LgaComponent,
    SettingsComponent,
    CreateRoleComponent,
    CreateMenuComponent,
    AssignRoleComponent,
    SectorComponent,

    FeeListComponent,
    AppStatusTmComponent,
    AppStatusPtComponent,
    AppStatusDsComponent,
    ProductComponent,
    DepartmentComponent,
    AuditComponent,
    UserAssignmentComponent,
    BackEndUserComponent,
    PendingUserComponent,
    UnitsComponent,
    MinistryComponent,
    RemittaComponent,
    PremSearchComponent,
    ProductBillingComponent,
    InvoiceComponent,
    NewApplicationComponent,
    AcknowledgementComponent,
    SearchFreshAppComponent,
    SearchKivComponent,
    SearchPrelimComponent,
    SearchtreatedComponent,
    ExaminerFreshComponent,
    CommentsComponent ,
    AcceptanceLetterComponent,
    RefusalLetterComponent,
    UserKivComponent,
    ExaminerKivComponent,
    ReConductSearchComponent,
    ExaminerTreatedComponent,
    PublicationNewComponent,
    PublicationBatchComponent,
    PublicationDetailComponent,
    RefuseApplicationComponent,
    AssignAppealComponent,
    AssignAppeal2Component,
    ReceiveAppealComponent,
    UserOppositionComponent,
    NoticeOfOppositionComponent,
    OppositionFreshComponent,
    UsercounterOppositionComponent,
    NoticeOfCounterOppositionComponent,
    UploadJudgementComponent,
    ViewJudgmentComponent,

    PayCertificateComponent,
    NoticeCertificatePaymentComponent,
    GenerateCertificateComponent,
    CertificateComponent,
    RenewTrademarkComponent,
    PayRenewalComponent,
    GenerateIssuedCertificateComponent,
    ViewrenewalComponent,
    GenRecordalRenewComponent,
    Invoice2Component,
    MergerTrademarkComponent,
    NoticeofmergerComponent,
    SearchDbComponent,
    TrademarkApplicationComponent,
    ViewPreliminarySearchComponent,
    BackEndUserProfileComponent,
    TrademarkPreviewComponent,
    RefuseApplicationReprintComponent,
    AcceptanceLetterReprintComponent,
    NewapplicationmigrationComponent,
    SearchmigrateappComponent,
    ChangeofnametrademarkComponent,
    NoticeofchangeofnameComponent,
    ViewChangeOfNameComponent,
    GenRecordalChangeOfNameComponent,
    GenRecordalChangeOfName2Component,
    ChangeofnamecertificateComponent,
    ChangeofaddresstrademarkComponent,
    NoticeofchangeofaddressComponent,

    GenRecordalChangeOfAddressComponent ,
    Genrecordalchangeofaddress2Component,
    ChangeofaddresscertificateComponent,
    GenRecordalMergerAndAssignmentComponent,
    GenRecordalMergerAndAssignment2Component,
    ChangeofassignmentcertificateComponent,
    GenRecordalRenew2Component



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
export class TrademarkModule { }
