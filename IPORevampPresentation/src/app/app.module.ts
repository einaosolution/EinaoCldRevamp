
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { DashboardComponent } from './dashboard/dashboard.component';

import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { HomeComponent } from './home/home.component';

import { HttpClientModule } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LogoutComponent } from './logout/logout.component';
import { TaskComponent } from './task/task.component';
import {CalendarModule} from 'primeng/calendar';
import { ViewUserComponent } from './view-user/view-user.component';
import { ContactusComponent } from './contactus/contactus.component';


import { EmailverificationComponent } from './emailverification/emailverification.component';




import { CorporateComponent } from './corporate/corporate.component';
import { IndividualComponent } from './individual/individual.component';




import { ForgetpasswordComponent } from './forgetpassword/forgetpassword.component';

import { NgxEditorModule } from 'ngx-editor';

import { ModalModule } from 'ngx-bootstrap';

import { AccordionModule } from 'ngx-bootstrap';

import { DataTablesModule } from 'angular-datatables';

import { SubMenuComponent } from './sub-menu/sub-menu.component';


import { NgxSummernoteModule } from 'ngx-summernote';

import { InternationalPhoneNumberModule } from 'ngx-international-phone-number';
import { PasswordStrengthBarModule } from 'ng2-password-strength-bar';
import { DeviceDetectorModule } from 'ngx-device-detector';

import { NgxQRCodeModule } from 'ngx-qrcode2';


import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ChartsModule } from 'ng2-charts';

import {AuthGuard} from './auth.guard';


import { SharedModule } from './shared.module';
import { Routes, RouterModule  ,PreloadAllModules } from '@angular/router';
import {NgBusyModule} from 'ng-busy';
import { BsDatepickerModule } from 'ngx-bootstrap';
import { AlertModule } from 'ngx-bootstrap';
import { NgxSpinnerModule } from 'ngx-spinner';
import { DefaultDashboardComponent } from './default-dashboard/default-dashboard.component';
import { TrademarkuserlistingComponent } from './trademarkuserlisting/trademarkuserlisting.component';
import { PatentuserlistingComponent } from './patentuserlisting/patentuserlisting.component';
import { DesignuserlistingComponent } from './designuserlisting/designuserlisting.component';
import { Defaultdashboard2Component } from './defaultdashboard2/defaultdashboard2.component';
import { Defaultdashboard3Component } from './defaultdashboard3/defaultdashboard3.component';





const routes: Routes = [
  { path: 'redirect', component: HomeComponent,   data: { animation: 'tiger1' } } ,
  { path: 'login', component: LoginComponent ,   data: { animation: 'tiger2' } } ,

  { path: 'register', component: RegisterComponent ,   data: { animation: 'tiger3' } } ,
  { path: 'Trademarkuserlisting', component: TrademarkuserlistingComponent ,   data: { animation: 'tiger4' } } ,
  { path: 'Patentuserlisting', component: PatentuserlistingComponent ,   data: { animation: 'tiger5' } } ,
  { path: 'Designuserlisting', component: DesignuserlistingComponent ,   data: { animation: 'tiger6' } } ,
  {
    path:  'home',
    component:  DashboardComponent
    } ,

  { path: 'logout', component: LogoutComponent ,   data: { animation: 'tiger4' } } ,
  { path: 'PasswordChange', component: TaskComponent ,   data: { animation: 'tiger5' } } ,
  { path: 'ViewUser', component: ViewUserComponent ,   data: { animation: 'tiger6' } } ,
  { path: 'Contactus', component: ContactusComponent ,   data: { animation: 'tiger7' } } ,
  { path: 'Emailverification', component: EmailverificationComponent ,   data: { animation: 'tiger8' } } ,
  { path: 'info/?page', component: CorporateComponent ,   data: { animation: 'tiger7' } } ,
  { path: 'info2/:id', component: IndividualComponent ,   data: { animation: 'tiger7' } } ,
   { path: 'Forgetpassword', component: ForgetpasswordComponent ,   data: { animation: 'tiger10' } } ,





{ path: 'Corporate', component: CorporateComponent ,   data: { animation: 'tiger9' } } ,
{ path: 'Individual', component: IndividualComponent ,   data: { animation: 'tiger9' } } ,
{ path: 'Dashboard',canActivateChild: [AuthGuard] , loadChildren: './trademark/trademark.module#TrademarkModule' } ,
{ path: 'Patent',canActivateChild: [AuthGuard] , loadChildren: './patent/patent.module#PatentModule' } ,
{ path: 'Design',canActivateChild: [AuthGuard] , loadChildren: './design/design.module#DesignModule' } ,
{ path: 'Reports',canActivateChild: [AuthGuard] , loadChildren: './reports/reports.module#ReportsModule' } ,
  { path: '',   redirectTo: 'redirect', pathMatch: 'full' }
];









@NgModule({
  declarations: [
    AppComponent,
    SubMenuComponent,
    DashboardComponent,
    HomeComponent,
    LoginComponent ,
    RegisterComponent,
    LogoutComponent,
    TaskComponent ,
    ViewUserComponent ,
    ContactusComponent ,
    EmailverificationComponent ,
    CorporateComponent ,
    IndividualComponent ,
    ForgetpasswordComponent,
    DefaultDashboardComponent,
    TrademarkuserlistingComponent,
    PatentuserlistingComponent,
    DesignuserlistingComponent,
    Defaultdashboard2Component,
    Defaultdashboard3Component





























  ],
  imports: [

    BrowserModule,
    BrowserAnimationsModule,

    SharedModule.forRoot(),

    [RouterModule.forRoot(routes,
      {preloadingStrategy: PreloadAllModules})] ,

    NgBusyModule ,
    BsDatepickerModule.forRoot(),
    AlertModule.forRoot(),
    ChartsModule,
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

   // AppRoutingModule,
    ReactiveFormsModule,

    CalendarModule,
    FormsModule ,





  ],
  providers: [	{ provide: LocationStrategy, useClass: HashLocationStrategy },AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
