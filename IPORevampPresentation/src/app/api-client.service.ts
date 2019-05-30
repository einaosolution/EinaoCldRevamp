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
serviceBase = 'http://localhost:5000/';
// serviceBase = 'http://5.77.54.44/EinaoCldRevamp2/';
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



  GetUserDetail() {

    var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		return this.http
			.get(this.serviceBase + 'api/UserManagement/GetUsers' ,{headers})
			.toPromise()
			.then((data) => {
				return data;
			});
  }



  GetMenu() {

    var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		return this.http
			.get(this.serviceBase + 'api/ParameterSetup/LoadAllMenu' ,{headers})
			.toPromise()
			.then((data) => {
				return data;
			});
  }

    SaveCountry(formData) {

    //  var token = localStorage.getItem('access_tokenexpire');

     // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/Country/SaveCountry', formData )
                  .toPromise()

                  .then(data => {  return data; });

    }


    SaveSector(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/Sector/SaveSector', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }


    UpdateRolePermission(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/Role/UpdateRolePermission', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }



    SaveRole(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/Role/SaveRole', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }



    SaveMenu(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/Menu/SaveMenu', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }


    SaveEmailTemplate(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/EmailTemplate/SaveEmailTemplate', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }



    SaveSetting(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/SystemSetup/SaveSetting', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }



    SaveLGA(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/LGA/SaveLGA', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }



      UpdateSetting(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/SystemSetup/UpdateSetting', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }

    UpdateEmailTemplate(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/EmailTemplate/UpdateEmailTemplate', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }

    UpdateCountry(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/Country/UpdateCountry', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }

    UpdateState(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/State/UpdateState', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }

    UpdateLGA(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/LGA/UpdateLGA', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }


    DeleteCountry(pp: string,pp2: string ) {

      var data = {
        CountryId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Country/DeleteCountry', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    DeleteState(pp: string,pp2: string ) {

      var data = {
        StateId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/State/DeleteState', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }



    DeleteLGA(pp: string,pp2: string ) {

      var data = {
        LGAId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/LGA/DeleteLGA', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }





    SaveState(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/State/SaveState', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }



  Register(formData) {


    return this.http.post( this.serviceBase + 'api/UserManagement/EmailVerification', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  ForgotPassword(formData) {


    return this.http.post( this.serviceBase + 'api/UserManagement/ForgotPassword', formData)
                .toPromise()

                .then(data => {  return data; });

  }



  ChangePassword(formData) {
    var token = localStorage.getItem('access_tokenexpire');
   // var headers = new Headers();
  //  headers.append('Authorization', 'Bearer ' + token);

    const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);

    return this.http.post( this.serviceBase + 'api/UserManagement/ChangePassword', formData,{headers})
                .toPromise()

                .then(data => {  return data; });

  }



  UpdateUser(formData) {



    //return this.http.post( this.serviceBase + 'api/UserManagement/UpdateIndividualRecord', formData)
    return this.http.post( this.serviceBase + 'api/UserManagement/UpdateUserInfo', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  UpdateUser2(formData) {



  //  return this.http.post( this.serviceBase + 'api/UserManagement/UpdateCorporateRecord', formData)
    return this.http.post( this.serviceBase + 'api/UserManagement/UpdateUserInfo', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  ResetPassword(pp: string,pp2: string ,pp3: string ,pp4: string) {

		var data = {
      code: pp ,
      UserId: pp2 ,
      NewPassord: pp3 ,
      ConfirmPassword: pp4
		};
		return this.http
			.get(this.serviceBase + 'api/UserManagement/ResetPassword', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetEmail(pp: string) {

		var data = {
      EmailAddress: pp
		};
		return this.http
			.get(this.serviceBase + 'api/UserManagement/GetUserFromEmail', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetCountry(pp : string,pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Country/GetAllCountries', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetAllRoles(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Role/GetAllRoles', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetMenuParentId(pp : string,pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {
      ParentId:pp ,
      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Menu/GetMenuParentId', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetMenuOthers(pp : string,pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {
      ParentId:pp ,
      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Menu/GetMenuOthers', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetAllStates(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/State/GetAllStates', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetAllEmailTemplates(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/EmailTemplate/GetAllEmailTemplates', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }



  GetAllSettings(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/SystemSetup/GetAllSettings', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetAllLGAs(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/LGA/GetAllLGAs', { params: data,headers })
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
			.get(this.serviceBase + 'api/UserManagement/GetUserFromEncryptEmail', { params: data })
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


    return this.http.post( this.serviceBase +'api/UserManagement/Authenticate', formData)
                .toPromise()

                .then(data => {  return data; });

  }
}
