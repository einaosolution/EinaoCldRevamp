import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { HomeComponent } from './home/home.component';
import {ApiClientService} from './api-client.service';

import { HttpClientModule } from '@angular/common/http';
import {NgBusyModule} from 'ng-busy';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LogoutComponent } from './logout/logout.component';
import { TaskComponent } from './task/task.component';
import {CalendarModule} from 'primeng/calendar';
import { ViewUserComponent } from './view-user/view-user.component';
import { ContactusComponent } from './contactus/contactus.component';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { EmailverificationComponent } from './emailverification/emailverification.component';
import { BsDatepickerModule } from 'ngx-bootstrap';
import { AlertModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Home2Component } from './home2/home2.component';
import { CorporateComponent } from './corporate/corporate.component';
import { IndividualComponent } from './individual/individual.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ForgetpasswordComponent } from './forgetpassword/forgetpassword.component';
import { CountryComponent } from './country/country.component';
import { StateComponent } from './state/state.component';
import { NgxEditorModule } from 'ngx-editor';
import { EmailTemplateComponent } from './email-template/email-template.component';
import { UserSecurityComponent } from './user-security/user-security.component';
import { LgaComponent } from './lga/lga.component';
import { SettingsComponent } from './settings/settings.component';
import { ModalModule } from 'ngx-bootstrap';
import { CreateRoleComponent } from './create-role/create-role.component';
import { CreateMenuComponent } from './create-menu/create-menu.component';
import { AssignRoleComponent } from './assign-role/assign-role.component';
import { AccordionModule } from 'ngx-bootstrap';
import { FilterPipe } from './filter.pipe';
import { FilterPipe2 } from './filter2.pipe';
import { DataTablesModule } from 'angular-datatables';
import { SectorComponent } from './sector/sector.component';
import { SubMenuComponent } from './sub-menu/sub-menu.component';
import { FeeListComponent } from './fee-list/fee-list.component';
import { AppStatusTmComponent } from './app-status-tm/app-status-tm.component';
import { AppStatusPtComponent } from './app-status-pt/app-status-pt.component';
import { AppStatusDsComponent } from './app-status-ds/app-status-ds.component';
import { ProductComponent } from './product/product.component';
import { DepartmentComponent } from './department/department.component';
import { AuditComponent } from './audit/audit.component';







@NgModule({
  declarations: [
    AppComponent,
    FilterPipe,
    FilterPipe2,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    LogoutComponent,
    TaskComponent,
    ViewUserComponent,
    ContactusComponent,
    HeaderComponent,
    SidebarComponent,
    EmailverificationComponent,
    DashboardComponent,
    Home2Component,
    CorporateComponent,
    IndividualComponent,
    ChangePasswordComponent,
    ForgetpasswordComponent,
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
    SubMenuComponent,
    FeeListComponent,
    AppStatusTmComponent,
    AppStatusPtComponent,
    AppStatusDsComponent,
    ProductComponent,
    DepartmentComponent,
    AuditComponent
  ],
  imports: [
    BrowserModule,
    ModalModule.forRoot() ,
    DataTablesModule,
    NgxEditorModule,
    BsDatepickerModule.forRoot(),
    AlertModule.forRoot(),
    AccordionModule.forRoot(),
    HttpClientModule,
    NgxSpinnerModule ,
    AppRoutingModule,
    ReactiveFormsModule,
    NgBusyModule,
    CalendarModule,
    FormsModule ,



    BrowserAnimationsModule
  ],
  providers: [ApiClientService ,	{ provide: LocationStrategy, useClass: HashLocationStrategy },],
  bootstrap: [AppComponent]
})
export class AppModule { }
