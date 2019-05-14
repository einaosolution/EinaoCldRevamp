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




@NgModule({
  declarations: [
    AppComponent,
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
    ChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    BsDatepickerModule.forRoot(),
    AlertModule.forRoot(),
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
