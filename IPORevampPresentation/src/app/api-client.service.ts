import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import {EventEmitter} from '@angular/core';
import {Router} from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  public vpage :string =""
  public changepassword :boolean=false;
// serviceBase = 'http://localhost:5000/';
 serviceBase = 'http://5.77.54.44/EinaoCldRevamp2/';
  navchange: EventEmitter<string> = new EventEmitter();

  constructor(private http: HttpClient,private router: Router) { }

  VChangeEvent(number) {
   // this.router.navigateByUrl('/home');
    this.navchange.emit(number);
  }

  changepassword2(bb:boolean) {
    // this.router.navigateByUrl('/home');
     this.changepassword =bb;
   }

   password2() {
    // this.router.navigateByUrl('/home');
    var kk =this.changepassword;
    return  kk
   }

  getNavChangeEmitter() {
    return this.navchange;
  }
  settoken(kk) {
    localStorage.setItem('access_tokenexpire',kk);
  }

  gettoken() {
    var dd = localStorage.getItem('access_tokenexpire')
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


  ChangePassword(formData) {
    var token = localStorage.getItem('access_tokenexpire');
   // var headers = new Headers();
  //  headers.append('Authorization', 'Bearer ' + token);

    const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);

    return this.http.post( this.serviceBase + 'api/users/ChangePassword', formData,{headers})
                .toPromise()

                .then(data => {  return data; });

  }



  UpdateUser(formData) {



    //return this.http.post( this.serviceBase + 'api/users/UpdateIndividualRecord', formData)
    return this.http.post( this.serviceBase + 'api/users/UpdateUserInfo', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  UpdateUser2(formData) {



  //  return this.http.post( this.serviceBase + 'api/users/UpdateCorporateRecord', formData)
    return this.http.post( this.serviceBase + 'api/users/UpdateUserInfo', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  GetEmail(pp: string) {

		var data = {
      EmailAddress: pp
		};
		return this.http
			.get(this.serviceBase + 'api/users/GetUserFromEmail', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetEmail2(pp: string) {
		var data = {
      EmailAddress: pp
		};
		return this.http
			.get(this.serviceBase + 'api/users/GetUserFromEncryptEmail', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
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


    return this.http.post( this.serviceBase +'api/users/Authenticate', formData)
                .toPromise()

                .then(data => {  return data; });

  }
}
