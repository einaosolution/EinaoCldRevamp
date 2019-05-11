import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {EventEmitter} from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  public vpage :string =""
  serviceBase = 'http://localhost:5000/';
 // serviceBase = 'http://5.77.54.44/EinaoCldRevamp2/';
  navchange: EventEmitter<string> = new EventEmitter();

  constructor(private http: HttpClient) { }

  VChangeEvent(number) {
    this.navchange.emit(number);
  }

  getNavChangeEmitter() {
    return this.navchange;
  }
  settoken(kk) {
    localStorage.setItem('token',kk);
  }

  gettoken() {
    var dd = localStorage.getItem('token')
   return dd ;
  }

  getPage() {
    return     this.vpage;

      //  alert("new value =" + this.status)
    }
    setPage(kk) {
        this.vpage =kk;

      //  alert("new value =" + this.status)
    }



  Register(formData) {


    return this.http.post( this.serviceBase + 'api/users/EmailVerification', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  CreateTask(formData) {


    return this.http.post( '/user/createtask', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  UpdateTask(formData) {


    return this.http.post( '/user/updatetask', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  UserCount(pp:string) {

    var data = {
      property1: pp

  };
    return this.http.get( '/user/Count', { params: data })
                .toPromise()

                .then(data => {  return data; });

  }

  GetUserTask(pp:string) {

    var data = {
      property1: pp

  };
    return this.http.get( '/user/GetTask', { params: data })
                .toPromise()

                .then(data => {  return data; });

  }

  GetUser() {


    return this.http.get( '/user/GetUser')
                .toPromise()

                .then(data => {  return data; });

  }

  GetMap() {


    return this.http.get( '/user/Googlemap')
                .toPromise()

                .then(data => {  return data; });

  }


  DeletTask(pp:string) {

    var data = {
      property1: pp

  };
    return this.http.get( '/user/deletetask', { params: data })
                .toPromise()

                .then(data => {  return data; });

  }



  Login(formData) {


    return this.http.post( '/user/signin', formData)
                .toPromise()

                .then(data => {  return data; });

  }
}
