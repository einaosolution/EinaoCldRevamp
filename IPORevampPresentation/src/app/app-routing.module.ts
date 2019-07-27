import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { LogoutComponent } from './logout/logout.component';
import { TaskComponent } from './task/task.component';
import { ViewUserComponent } from './view-user/view-user.component';
import { ContactusComponent } from './contactus/contactus.component';
import { EmailverificationComponent } from './emailverification/emailverification.component';
import { Home2Component } from './home2/home2.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HeaderComponent } from './header/header.component';
import { CorporateComponent } from './corporate/corporate.component';
import { IndividualComponent } from './individual/individual.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ForgetpasswordComponent } from './forgetpassword/forgetpassword.component';
import { CountryComponent } from './country/country.component';
import { StateComponent } from './state/state.component';
import { EmailTemplateComponent } from './email-template/email-template.component';
import { UserSecurityComponent } from './user-security/user-security.component';
import { LgaComponent } from './lga/lga.component';
import { SettingsComponent } from './settings/settings.component';

import { CreateRoleComponent } from './create-role/create-role.component';
import { CreateMenuComponent } from './create-menu/create-menu.component';
import { AssignRoleComponent } from './assign-role/assign-role.component';
import { SectorComponent } from './sector/sector.component';
import { FeeListComponent } from './fee-list/fee-list.component';

import { AppStatusTmComponent } from './app-status-tm/app-status-tm.component';
import { AppStatusPtComponent } from './app-status-pt/app-status-pt.component';
import { AppStatusDsComponent } from './app-status-ds/app-status-ds.component';
import { ProductComponent } from './product/product.component';
import { DepartmentComponent } from './department/department.component';
import { AuditComponent } from './audit/audit.component';
import { UserAssignmentComponent } from './user-assignment/user-assignment.component';
import { BackEndUserComponent } from './back-end-user/back-end-user.component';
import { PendingUserComponent } from './pending-user/pending-user.component';
import { UnitsComponent } from './units/units.component';
import { MinistryComponent } from './ministry/ministry.component';
import { RemittaComponent } from './remitta/remitta.component';
import { PremSearchComponent } from './prem-search/prem-search.component';

import { ProductBillingComponent } from './product-billing/product-billing.component';

import { InvoiceComponent } from './invoice/invoice.component';
import { NewApplicationComponent } from './new-application/new-application.component';
import { AcknowledgementComponent } from './acknowledgement/acknowledgement.component';
import { SearchFreshAppComponent } from './search-fresh-app/search-fresh-app.component';
import { SearchKivComponent } from './search-kiv/search-kiv.component';
import { SearchPrelimComponent } from './search-prelim/search-prelim.component';
import { SearchtreatedComponent } from './searchtreated/searchtreated.component';
import { ExaminerFreshComponent } from './examiner-fresh/examiner-fresh.component';
import { AcceptanceLetterComponent } from './acceptance-letter/acceptance-letter.component';
import { RefusalLetterComponent } from './refusal-letter/refusal-letter.component';
import { UserKivComponent } from './user-kiv/user-kiv.component';
import { ExaminerKivComponent } from './examiner-kiv/examiner-kiv.component';
import { ReConductSearchComponent } from './re-conduct-search/re-conduct-search.component';
import { ExaminerTreatedComponent } from './examiner-treated/examiner-treated.component';
import { PublicationNewComponent } from './publication-new/publication-new.component';
import { PublicationBatchComponent } from './publication-batch/publication-batch.component';
import { PublicationDetailComponent } from './publication-detail/publication-detail.component';
import { RefuseApplicationComponent } from './refuse-application/refuse-application.component';
import { AssignAppealComponent } from './assign-appeal/assign-appeal.component';
import { AssignAppeal2Component } from './assign-appeal2/assign-appeal2.component';
import { ReceiveAppealComponent } from './receive-appeal/receive-appeal.component';
import { UserOppositionComponent } from './user-opposition/user-opposition.component';
import { NoticeOfOppositionComponent } from './notice-of-opposition/notice-of-opposition.component';
import { OppositionFreshComponent } from './opposition-fresh/opposition-fresh.component';
import { UsercounterOppositionComponent } from './usercounter-opposition/usercounter-opposition.component';
import { NoticeOfCounterOppositionComponent } from './notice-of-counter-opposition/notice-of-counter-opposition.component';
import { UploadJudgementComponent } from './upload-judgement/upload-judgement.component';
import { ViewJudgmentComponent } from './view-judgment/view-judgment.component';
import { TrademarkReportComponent } from './trademark-report/trademark-report.component';
import { PayCertificateComponent } from './pay-certificate/pay-certificate.component';
import { NoticeCertificatePaymentComponent } from './notice-certificate-payment/notice-certificate-payment.component';
import { GenerateCertificateComponent } from './generate-certificate/generate-certificate.component';
import { CertificateComponent } from './certificate/certificate.component';
import { RenewTrademarkComponent } from './renew-trademark/renew-trademark.component';
import { PayRenewalComponent } from './pay-renewal/pay-renewal.component';
import { GenerateIssuedCertificateComponent } from './generate-issued-certificate/generate-issued-certificate.component';
import { GenRecordalRenewComponent } from './gen-recordal-renew/gen-recordal-renew.component';
import { Invoice2Component } from './invoice2/invoice2.component';
import { MergerTrademarkComponent } from './merger-trademark/merger-trademark.component';
import { SearchDbComponent } from './search-db/search-db.component';
import { CanActivateChild } from '@angular/router';
import {AuthGuard} from './auth.guard';
import { NoticeofmergerComponent } from './noticeofmerger/noticeofmerger.component';
import { TrademarkApplicationComponent } from './trademark-application/trademark-application.component';
import { ViewPreliminarySearchComponent } from './view-preliminary-search/view-preliminary-search.component';



const routes: Routes = [
  { path: 'home', component: HomeComponent,   data: { animation: 'tiger1' } } ,
  { path: 'login', component: LoginComponent ,   data: { animation: 'tiger2' } } ,
  { path: 'login', component: LoginComponent ,   data: { animation: 'tiger2' } } ,
  { path: 'register', component: RegisterComponent ,   data: { animation: 'tiger3' } } ,
  { path: 'logout', component: LogoutComponent ,   data: { animation: 'tiger4' } } ,
  { path: 'PasswordChange', component: TaskComponent ,   data: { animation: 'tiger5' } } ,
  { path: 'ViewUser', component: ViewUserComponent ,   data: { animation: 'tiger6' } } ,
  { path: 'Contactus', component: ContactusComponent ,   data: { animation: 'tiger7' } } ,
  { path: 'Emailverification', component: EmailverificationComponent ,   data: { animation: 'tiger8' } } ,
  { path: 'info/?page', component: CorporateComponent ,   data: { animation: 'tiger7' } } ,
  { path: 'info2/:id', component: IndividualComponent ,   data: { animation: 'tiger7' } } ,
   { path: 'Forgetpassword', component: ForgetpasswordComponent ,   data: { animation: 'tiger10' } } ,
   { path: 'Forgetpassword', component: ForgetpasswordComponent ,   data: { animation: 'tiger12' } } ,
   { path: 'TrademarkReport', component: TrademarkReportComponent ,   data: { animation: 'tiger11' } } ,


  { path: 'Dashboard', component: HeaderComponent,canActivateChild: [AuthGuard] ,   children: [
{
path:  'Dashboard2',
component:  DashboardComponent
},{
path:  'Country',
component:  CountryComponent
},{
  path:  'Acknowledgement',
  component:  AcknowledgementComponent
  },{
    path:  'AcceptanceLetter',
    component:  AcceptanceLetterComponent
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
          } ,
        {
          path:  'GenRecordalRenew',
          component:  GenRecordalRenewComponent
          } ,{
        path:  'AssignAppeal2',
        component:  AssignAppeal2Component
        },{
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
        },{
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
            }] } ,
{ path: 'Corporate', component: CorporateComponent ,   data: { animation: 'tiger9' } } ,
{ path: 'Individual', component: IndividualComponent ,   data: { animation: 'tiger9' } } ,
  { path: '',   redirectTo: 'home', pathMatch: 'full' }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {


 }
