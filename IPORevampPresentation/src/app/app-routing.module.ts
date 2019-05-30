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


  { path: 'Dashboard', component: HeaderComponent,   children: [
{
path:  'Dashboard2',
component:  DashboardComponent
},{
path:  'Country',
component:  CountryComponent
} ,{
  path:  'State',
  component:  StateComponent
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
        path:  'Menu',
        component:  CreateMenuComponent
        } ,{
          path:  'AssignRole',
          component:  AssignRoleComponent
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
