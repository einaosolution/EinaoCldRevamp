import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import {EventEmitter} from '@angular/core';
import {Router} from '@angular/router';
import { DeviceDetectorService } from 'ngx-device-detector';
import * as jwt_decode from  "jwt-decode"

import { Student } from './Student';
import { Invention } from './Invention';
import { Priority } from './Priority';
import { CoApplicant } from './CoApplicant';
import { Observable } from 'rxjs';
import { UUID } from 'angular2-uuid';
declare var $ :any;





@Injectable({
  providedIn: 'root'
})


export class ApiClientService {
  public vpage :string =""
  public changepassword :boolean=false;
 //serviceBase = 'http://localhost:5000/';
serviceBase = 'http://5.77.54.44/EinaoCldRevamp2/';
serviceBase2 = 'http://5.77.54.44/EinaoCldRevamp/#/';
// serviceBase2 = 'http://localhost:4200/#/';
  navchange: EventEmitter<string> = new EventEmitter();
  invention :Invention[]=[]
  priority :Priority[]=[]
  coApplicant :CoApplicant[]=[]
  students: Student[] = [{
    id: 1,
    name: 'Krunal',
    EnrollmentNumber: 11047,
    College: 'VVP Engineering College',
    University: 'GTU'
},
{
    id: 2,
    name: 'Rushabh',
    EnrollmentNumber: 116023,
    College: 'VVP Engineering College',
    University: 'GTU'
}];



  constructor(private http: HttpClient,private router: Router , private deviceService: DeviceDetectorService) {
    this.getIpAddress()
   }


   public getStudents(): any {
    const studentsObservable = new Observable(observer => {
           setTimeout(() => {
               observer.next(this.students);
           }, 1000);
    });

    return studentsObservable;
}


public getInvention(): any {
  const inventionObservable = new Observable(observer => {
         setTimeout(() => {
             observer.next(this.invention);
         }, 1000);
  });

  return inventionObservable;
}


public getPriority(): any {
  const priorityObservable = new Observable(observer => {
         setTimeout(() => {
             observer.next(this.priority);
         }, 1000);
  });

  return priorityObservable;
}


public getCoApplicant(): any {
  const coApplicantObservable = new Observable(observer => {
         setTimeout(() => {
             observer.next(this.coApplicant);
         }, 1000);
  });

  return coApplicantObservable;
}


public RemoveCoApplicant (ids  ) {

  for (let i = 0; i < this.coApplicant.length; i++) {

    if(this.coApplicant[i].id === ids) {
      this.coApplicant.splice(i,1);
        return false;
    }

  }



}

public AddCoApplicant (CoApplicant ) {
  this.coApplicant.push(CoApplicant)
}

public getCoApplicant2 ( ) {
  return  this.coApplicant
 }
public AddInvention (Invention ) {
  this.invention.push(Invention)
}

public AddPriority (Priority ) {
  this.priority.push(Priority)
}


public getInvention2 ( ) {
 return  this.invention
}


public getPriority2 ( ) {
 return  this.priority
}



public RemoveInvention (ids  ) {

  for (let i = 0; i < this.invention.length; i++) {

    if(this.invention[i].id === ids) {
      this.invention.splice(i,1);
        return false;
    }
   // text += cars[i] + "<br>";
  }



}


public RemovePriority (ids  ) {

  for (let i = 0; i < this.priority.length; i++) {

    if(this.priority[i].id === ids) {
      this.priority.splice(i,1);
        return false;
    }
   // text += cars[i] + "<br>";
  }



}

public Reset (  ) {

  this.priority = []
  this.invention =[]
  this.coApplicant =[]

}




public GetRandomNumber ( ) {
  return UUID.UUID()
}


   GetFilepath() {
		return this.serviceBase2;
  }

  GetFilepath2() {
		return this.serviceBase;
	}

  VChangeEvent(number) {
   // this.router.navigateByUrl('/home');
    this.navchange.emit(number);
  }

  changepassword2(bb:boolean) {
    // this.router.navigateByUrl('/home');
     this.changepassword =bb;
   }


   getIpAddress() {

    $.getJSON('https://api.ipify.org?format=json', function(data){
      console.log("ip");
      localStorage.setItem('ip',data.ip );


      console.log(data);
  });
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

  checktokenstatus() {
    try {
      var token = localStorage.getItem('access_tokenexpire');
      var decoded = jwt_decode(token);
      console.log("decodedtoken =")
      console.log(decoded)
      const date = new Date()
      var d = new Date(decoded.exp*1000);
   //  alert(d)
     //alert(date)

      let tokenExpDate = date.setUTCSeconds(decoded.exp)
      console.log("tokenExpDate =")
      console.log(tokenExpDate)


      if (Date.now() >= decoded.exp * 1000) {
      //  return false;
     // alert("session has expired")
    // this.router.navigateByUrl('/logout');
    return false;
      }

      else {

        return true;

      }

  }

  catch(err) {

  }
}
  gettoken() {


    var dd = localStorage.getItem('access_tokenexpire')
   return dd ;
  }


  GetTrademarkdata() {

		return this.http
			.get('http://5.77.54.44/DashBoard3/api/account/TestGetData')
			.toPromise()
			.then((data) => {
				return data;
			});
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
     //var vip = localStorage.getItem('ip');
    // const  headers = new  HttpHeaders().set("ip", vip);
      return this.http.post( this.serviceBase + 'api/Country/SaveCountry', formData )
                  .toPromise()

                  .then(data => {  return data; });

    }

    SaveFreshAppHistory(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
       //var vip = localStorage.getItem('ip');
      // const  headers = new  HttpHeaders().set("ip", vip);
        return this.http.post( this.serviceBase + 'api/Search/SaveFreshAppHistory', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }


      SavePatentFreshAppHistory(formData) {

        //  var token = localStorage.getItem('access_tokenexpire');

         // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
         //var vip = localStorage.getItem('ip');
        // const  headers = new  HttpHeaders().set("ip", vip);
          return this.http.post( this.serviceBase + 'api/PatentSearch/SavePatentFreshAppHistory', formData )
                      .toPromise()

                      .then(data => {  return data; });

        }


        SaveDesignFreshAppHistory(formData) {

          //  var token = localStorage.getItem('access_tokenexpire');

           // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
           //var vip = localStorage.getItem('ip');
          // const  headers = new  HttpHeaders().set("ip", vip);
            return this.http.post( this.serviceBase + 'api/DesignSearch/SaveDesignFreshAppHistory', formData )
                        .toPromise()

                        .then(data => {  return data; });

          }


        SavePatentStateAppHistory(formData) {

          //  var token = localStorage.getItem('access_tokenexpire');

           // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
           //var vip = localStorage.getItem('ip');
          // const  headers = new  HttpHeaders().set("ip", vip);
            return this.http.post( this.serviceBase + 'api/PatentSearch/SavePatentStateAppHistory', formData )
                        .toPromise()

                        .then(data => {  return data; });

          }


          SaveDesignStateAppHistory(formData) {

            //  var token = localStorage.getItem('access_tokenexpire');

             // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
             //var vip = localStorage.getItem('ip');
            // const  headers = new  HttpHeaders().set("ip", vip);
              return this.http.post( this.serviceBase + 'api/DesignSearch/SaveDesignStateAppHistory', formData )
                          .toPromise()

                          .then(data => {  return data; });

            }










      SaveFreshAppHistory2(formData) {

        //  var token = localStorage.getItem('access_tokenexpire');

         // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
         //var vip = localStorage.getItem('ip');
        // const  headers = new  HttpHeaders().set("ip", vip);
          return this.http.post( this.serviceBase + 'api/Search/SaveFreshAppHistoryAttachment', formData )
                      .toPromise()

                      .then(data => {  return data; });

        }

        SaveFreshAppHistory3(formData) {

          //  var token = localStorage.getItem('access_tokenexpire');

           // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
           //var vip = localStorage.getItem('ip');
          // const  headers = new  HttpHeaders().set("ip", vip);
            return this.http.post( this.serviceBase + 'api/Search/SaveFreshAppHistoryMultiple', formData )
                        .toPromise()

                        .then(data => {  return data; });

          }



      SendAttachment(formData) {

        //  var token = localStorage.getItem('access_tokenexpire');

         // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
         //var vip = localStorage.getItem('ip');
        // const  headers = new  HttpHeaders().set("ip", vip);
          return this.http.post( this.serviceBase + 'api/Trademark/SendAttachment', formData )
                      .toPromise()

                      .then(data => {  return data; });

        }



        SendAKnowlegment(formData) {

          //  var token = localStorage.getItem('access_tokenexpire');

           // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
           //var vip = localStorage.getItem('ip');
          // const  headers = new  HttpHeaders().set("ip", vip);
            return this.http.post( this.serviceBase + 'api/Trademark/SendAKnowlegment', formData )
                        .toPromise()

                        .then(data => {  return data; });

          }


        SendAttachmentReceipt(formData) {

          //  var token = localStorage.getItem('access_tokenexpire');

           // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
           //var vip = localStorage.getItem('ip');
          // const  headers = new  HttpHeaders().set("ip", vip);
            return this.http.post( this.serviceBase + 'api/Trademark/SendAttachmentReceipt', formData )
                        .toPromise()

                        .then(data => {  return data; });

          }


          SendAttachmentAcceptance(formData) {

            //  var token = localStorage.getItem('access_tokenexpire');

             // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
             //var vip = localStorage.getItem('ip');
            // const  headers = new  HttpHeaders().set("ip", vip);
              return this.http.post( this.serviceBase + 'api/Trademark/SendAttachmentAcceptance', formData )
                          .toPromise()

                          .then(data => {  return data; });

            }


            SendAttachmentPatentAcceptance(formData) {

              //  var token = localStorage.getItem('access_tokenexpire');

               // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
               //var vip = localStorage.getItem('ip');
              // const  headers = new  HttpHeaders().set("ip", vip);
                return this.http.post( this.serviceBase + 'api/Patent/SendAttachmentAcceptance', formData )
                            .toPromise()

                            .then(data => {  return data; });

              }

            SendAttachmentRefusal(formData) {

              //  var token = localStorage.getItem('access_tokenexpire');

               // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
               //var vip = localStorage.getItem('ip');
              // const  headers = new  HttpHeaders().set("ip", vip);
                return this.http.post( this.serviceBase + 'api/Trademark/SendAttachmentRefusal', formData )
                            .toPromise()

                            .then(data => {  return data; });

              }




    SavePrelimSearch(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
       //var vip = localStorage.getItem('ip');
      // const  headers = new  HttpHeaders().set("ip", vip);
        return this.http.post( this.serviceBase + 'api/preliminary/SavePrelimSearch', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }


    SaveUser(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/UserManagement/signup2', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }


    SaveDepartment(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/Department/SaveDepartment', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }


       InitiateRemitaPayment(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/RemitaPayment/InitiateRemitaPayment', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }


      SendUserEmail(formData) {

        //  var token = localStorage.getItem('access_tokenexpire');

         // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
          return this.http.post( this.serviceBase + 'api/preliminary/SendUserEmail', formData )
                      .toPromise()

                      .then(data => {  return data; });

        }


      RemitaTransactionRequeryPayment(formData) {

        //  var token = localStorage.getItem('access_tokenexpire');

         // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
          return this.http.post( this.serviceBase + 'api/RemitaPayment/RemitaTransactionRequeryPayment', formData )
                      .toPromise()

                      .then(data => {  return data; });

        }



      RemitaTransactionInvoiceUpdate(formData) {

        //  var token = localStorage.getItem('access_tokenexpire');

         // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
          return this.http.post( this.serviceBase + 'api/RemitaPayment/UpdatePaymentWithInvoice', formData )
                      .toPromise()

                      .then(data => {  return data; });

        }




    SaveProduct(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/Product/SaveProduct', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }

    SaveTMApplicationStatus(formData) {

      //  var token = localStorage.getItem('access_tokenexpire');

       // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
        return this.http.post( this.serviceBase + 'api/TMApplicationStatus/SaveTMApplicationStatus', formData )
                    .toPromise()

                    .then(data => {  return data; });

      }


      SavePTApplicationStatus(formData) {

        //  var token = localStorage.getItem('access_tokenexpire');

         // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
          return this.http.post( this.serviceBase + 'api/PTApplicationStatus/SavePTApplicationStatus', formData )
                      .toPromise()

                      .then(data => {  return data; });

        }


        SaveDSApplicationStatus(formData) {

          //  var token = localStorage.getItem('access_tokenexpire');

           // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
            return this.http.post( this.serviceBase + 'api/DSApplicationStatus/SaveDSApplicationStatus', formData )
                        .toPromise()

                        .then(data => {  return data; });

          }

          SaveMinistry(formData) {

            //  var token = localStorage.getItem('access_tokenexpire');

             // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
              return this.http.post( this.serviceBase + 'api/Ministry/SaveMinistry', formData )
                          .toPromise()

                          .then(data => {  return data; });

            }


          SaveUnit(formData) {

            //  var token = localStorage.getItem('access_tokenexpire');

             // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
              return this.http.post( this.serviceBase + 'api/Unit/SaveUnit', formData )
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


      UpdateDepartment(formData) {

        //  var token = localStorage.getItem('access_tokenexpire');

         // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
          return this.http.post( this.serviceBase + 'api/Department/UpdateDepartment', formData )
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

    UpdateProduct(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/Product/UpdateProduct', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }


    UpdateTMApplicationStatus(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/TMApplicationStatus/UpdateTMApplicationStatus', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }

    UpdatePTApplicationStatus(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/PTApplicationStatus/UpdatePTApplicationStatus', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }

    UpdateDSApplicationStatus(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/DSApplicationStatus/UpdateDSApplicationStatus', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }

    UpdateMinistry(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/Ministry/UpdateMinistry', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }


    UpdateUnit(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/Unit/UpdateUnit', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }


    UpdateSector(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/Sector/UpdateSector', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }



    UpdateMenu(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/Menu/UpdateMenu', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }



    EditRole(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/Role/EditRole', formData ,{headers})
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

    ApproveUser(pp: string,pp2: string ,pp3: string ) {

      var data = {
        Email: pp ,
        RequestedBy: pp2 ,
        Roleid: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/UserManagement/ApproveUser', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    UpdateDesignApplicationBatch(pp2: string ) {

      var data = {

        RequestById: pp2


      };
      return this.http
        .get(this.serviceBase + 'api/DesignPublication/UpdateApplicationBatch', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetSettingsById(pp: string,pp2: string ) {

      var data = {
        SettingId: pp ,
        RequestById: pp2


      };
      return this.http
        .get(this.serviceBase + 'api/SystemSetup/GetSettingById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }
RejectUser(pp: string,pp2: string ) {

      var data = {
        Email: pp ,
        RequestedBy: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/UserManagement/RejectUser', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }



    GetPatentSearchState(pp: string,pp2: string ) {

      var data = {
        RequestById: pp ,
        ApplicationId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetPatentSearchState', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignSearchState(pp: string,pp2: string ) {

      var data = {
        RequestById: pp ,
        ApplicationId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetDesignSearchState', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
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


    DeleteUser(pp: string,pp2: string ) {

      var data = {
        UserId: pp ,
        RequestedBy: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/UserManagement/DeleteUser', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }
    LogoutUser(pp: string ) {

      var data = {

        RequestedBy: pp

      };
      return this.http
        .get(this.serviceBase + 'api/UserManagement/Logout', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    DeleteDepartment(pp: string,pp2: string ) {

      var data = {
        ProductId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Department/DeleteDepartment', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    DeleteProduct(pp: string,pp2: string ) {

      var data = {
        ProductId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Product/DeleteProduct', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    DeleteFeeList(pp: string,pp2: string ) {

      var data = {
        FeeListId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/FeeList/DeleteFeeList', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    DeleteSector(pp: string,pp2: string ) {

      var data = {
        SectorId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Sector/DeleteSector', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    DeleteTMApplicationStatu(pp: string,pp2: string ) {

      var data = {
        TMApplicationStatusId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/TMApplicationStatus/DeleteTMApplicationStatus', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    DeletePTApplicationStatus(pp: string,pp2: string ) {

      var data = {
        PTApplicationStatusId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/PTApplicationStatus/DeletePTApplicationStatus', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetUser2() {


      return this.http
        .get(this.serviceBase + 'api/UserManagement/GetUser')
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetAllTempUser() {


      return this.http
        .get(this.serviceBase + 'api/UserManagement/GetAllTempUser')
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetAllTempUser2(pp: string) {

      var data = {
        EmailAddress: pp


      };

      return this.http
        .get(this.serviceBase + 'api/UserManagement/GetAllTempUser2', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetAllTempCount(pp: string) {

      var data = {
        Userid: pp


      };

      return this.http
        .get(this.serviceBase + 'api/UserManagement/GetAllTempCount', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }



    DeleteDSApplicationStatus(pp: string,pp2: string ) {

      var data = {
        DSApplicationStatusId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/DSApplicationStatus/DeleteDSApplicationStatus', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    DeleteMinistry(pp: string,pp2: string ) {

      var data = {
        MinistryId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Ministry/DeleteMinistry', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    DeletUnit(pp: string,pp2: string ) {

      var data = {
        ProductId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Unit/DeletUnit', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    DeleteRole(pp: string,pp2: string ) {

      var data = {
        RoleId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Role/DeleteRole', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    DeleteMenu(pp: string,pp2: string ) {

      var data = {
        MenuId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Menu/DeleteMenu', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    DeleteSetting(pp: string,pp2: string ) {

      var data = {
        SettingId: pp ,
        UserId: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/SystemSetup/DeleteSetting', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    DeleteEmailTemplate(pp: string,pp2: string ,pp3: string ) {

      var data = {
        EmailName: pp ,
        UserId: pp2,
        EmailId:pp3

      };
      return this.http
        .get(this.serviceBase + 'api/EmailTemplate/DeleteEmailTemplate', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }





    GetRoleById(pp: string,pp2: string ) {

      var data = {
        RoleId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Role/GetRoleById', { params: data })
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

    GetApplicationById(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Opposition/GetApplicationById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetApplicationById2(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Certificate/GetApplicationById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetPatentSubmittedApplication( ) {


      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetPatentSubmittedApplication')
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetPatentFreshapplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetPatentFreshApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetDesignFreshapplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetDesignFreshApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetDesignReceiveAppealApplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/GetDesignReceiveAppealApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignRefuseApplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignExaminer/GetDesignRefuseApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetDesignPendingPublication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignPublication/GetDesignPendingPublication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetDesignUnpublishedPublication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignPublication/GetDesignUnpublishedPublication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignReconductSearchApplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetDesignReconductSearchApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignKivSearchApplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetDesignKivSearchApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignKivExaminerApplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetDesignKivExaminerApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetDesignRegistraFreshapplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/GetDesignFreshApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignAppealApplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/GetDesignAppealApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetDesignFreshExaminerapplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignExaminer/GetDesignFreshApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }



    GetPatentByUserId(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetPatentByUserId', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetPatentExaminerFreshapplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/GetFreshApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignCertificateFreshapplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignCertificate/GetFreshApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignCertificateConfirmapplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignCertificate/GetConfirmCertificate', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignDateFormat(pp2: string  ) {
      var data = {

        ApplicationId: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignCertificate/GetDateFormat', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetPatentCertificateFreshApplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentCertificate/GetFreshApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignListing(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetDesignListing', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }



    GetDelegateApplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentCertificate/GetDelegateApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignDelegateApplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/GetDelegateApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDelegateAppeal(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/GetDelegateAppeal', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentCertificatePayment(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentCertificate/GetPatentCertificatePayment', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignCertificatePayment(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/GetDesignCertificatePayment', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetPatentPaidCertificate(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentCertificate/GetPatentPaidCertificate', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetPatentUserByApplication(pp2: string ,pp: string  ) {
      var data = {

        RequestById: pp2 ,
        ApplicationId: pp

      };

      return this.http
        .get(this.serviceBase + 'api/PatentCertificate/GetUserByApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentConfirmedCertificate(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentCertificate/GetPatentConfirmedCertificate', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentExaminerKivapplication(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/GetPatentExaminerKiv', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentSearchKiv(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetPatentSearchKiv', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentAppealUnit(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/GetPatentAppealUnit', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentTreatedAppeal(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/GetPatentTreatedAppeal', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetApplicationTransactionAmount(pp2: string ,pp: string  ) {
      var data = {

        RequestById: pp2 ,
        TransactionId: pp

      };

      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/GetApplicationTransactionAmount', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }



    GetAllPayment(pp: string ,pp2: string ,pp3: string ,pp4: string  ) {
      var data = {

        RequestById: pp ,
        startdate: pp2 ,
        enddate: pp3 ,
        Feeid: pp4

      };

      return this.http
        .get(this.serviceBase + 'api/RemitaPayment/GetAllPayment', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetPatentAppeal(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/GetPatentAppeal', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentAppealCount(pp2: string  ) {
      var data = {

        Userid: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/GetPatentAppealCount', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetDesignAppealCount(pp2: string  ) {
      var data = {

        Userid: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/GetDesignAppealCount', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignReceiveAppealCount(pp2: string  ) {
      var data = {

        Userid: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/GetDesignReceiveAppealCount', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentTreatedAppealCount(pp2: string  ) {
      var data = {

        Userid: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/GetPatentTreatedAppealCount', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetPatentExaminerReconductSearch(pp2: string  ) {
      var data = {

        RequestById: pp2

      };

      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/GetPatentExaminerReconductSearch', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentPriority(pp: string,pp2: string ) {

      var data = {
        Id: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetPatentPriority', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignPriority(pp: string,pp2: string ) {

      var data = {
        Id: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetDesignPriority', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetAddressOfServiceById2(pp: string,pp2: string ) {

      var data = {
        Id: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetAddressOfServiceById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetAddressOfServiceById3(pp: string,pp2: string ) {

      var data = {
        Id: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetAddressOfServiceById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    SendExaminerEmail(pp: string ) {

      var data = {

        RequestById: pp

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/SendExaminerEmail', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    DelegateExaminerEmail(pp: string ,pp2: string , pp3: string ) {

      var data = {

        RequestById: pp ,
        userid: pp2 ,
        Applicationid: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/DelegateExaminerEmail', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    DelegateDesignExaminerEmail(pp: string ,pp2: string , pp3: string ) {

      var data = {

        RequestById: pp ,
        userid: pp2 ,
        Applicationid: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/DelegateExaminerEmail', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    SendPatentExaminerEmail(pp: string ) {

      var data = {

        RequestById: pp

      };
      return this.http
        .get(this.serviceBase + 'api/PatentExaminer/SendExaminerEmail', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    SendDesignExaminerEmail(pp: string ) {

      var data = {

        RequestById: pp

      };
      return this.http
        .get(this.serviceBase + 'api/DesignSearch/SendExaminerEmail', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }



    SendPatentUserEmail(pp: string ,pp2: string  ,pp3: string  ) {

      var data = {

        RequestById: pp ,
        appid: pp2 ,
        comment:pp3

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/SendUserEmail', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    SendDesignUserEmail(pp: string ,pp2: string  ,pp3: string  ) {

      var data = {

        RequestById: pp ,
        appid: pp2 ,
        comment:pp3

      };
      return this.http
        .get(this.serviceBase + 'api/DesignSearch/SendUserEmail', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    SendEmailForCertificate(pp: string ,pp2: string  ,pp3: string  ) {

      var data = {

        RequestById: pp ,
        appid: pp2 ,
        comment:pp3

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/SendEmailForCertificate', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    SendDesignEmailForCertificate(pp: string ,pp2: string  ,pp3: string  ) {

      var data = {

        RequestById: pp ,
        appid: pp2 ,
        comment:pp3

      };
      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/SendEmailForCertificate', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentInventorById(pp: string,pp2: string ) {

      var data = {
        Id: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetPatentInventorById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetDesignInventorById(pp: string,pp2: string ) {

      var data = {
        Id: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetDesignInventorById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetDesignCoApplicantById(pp: string,pp2: string ) {

      var data = {
        Id: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/DesignSearch/GetDesignCoApplicantById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetPatentFreshApplication(pp: string ) {

      var data = {

        RequestById: pp

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetPatentFreshApplication', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetAddressOfServiceById(pp: string,pp2: string ) {

      var data = {
        Id: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/PatentSearch/GetAddressOfServiceById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetApplicationByDocumentId(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Recordal/GetApplicationByDocumentId', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetRecordalApplicationById2(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Recordal/GetApplicationById2', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    UpdateApplicationById(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Certificate/GetUpdateApplicationById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetOpposeFormById(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Opposition/GetOpposeFormById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetCounterOpposeFormById(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Opposition/GetCounterOpposeFormById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetRenewalApplicationById(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Recordal/GetRenewalApplicationById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetMergerApplicationById(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Recordal/GetMergerApplicationById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

//
    GetMergerApplicationByAppId(pp: string,pp2: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Recordal/GetMergerApplicationByAppId', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }



    GetOppositionByUserid(pp: string,pp2: string ) {

      var data = {
        userId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Opposition/GetOppositionByUserid', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetCounterOppositionByUserid(pp: string,pp2: string ) {

      var data = {
        userId: pp ,
        RequestById: pp2

      };
      return this.http
        .get(this.serviceBase + 'api/Opposition/GetCounterOppositionByUserid', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }




    UpdateOpposeFormById2(pp: string,pp2: string ,pp3: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2 ,
        TransactionId: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/Opposition/UpdateOpposeFormById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }



    UpdateCounterOpposeFormById(pp: string,pp2: string ,pp3: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2 ,
        TransactionId: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/Opposition/UpdateCounterOpposeFormById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    UpdateRenewalFormById(pp: string,pp2: string ,pp3: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2 ,
        TransactionId: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/Recordal/UpdateRenewalFormById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    UpdateRenewalDesignById(pp: string,pp2: string ,pp3: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2 ,
        TransactionId: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/Recordal/UpdateRenewalDesignById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    UpdateMergerFormById(pp: string,pp2: string ,pp3: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2 ,
        TransactionId: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/Recordal/UpdateMergerFormById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetApplicationCount(pp: string,pp2: string  ) {

      var data = {
        applicationid: pp ,
        userid: pp2


      };
      return this.http
        .get(this.serviceBase + 'api/Trademark/GetApplicationUserCount', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetAllApplicationCount(pp2: string  ) {

      var data = {

        userid: pp2


      };
      return this.http
        .get(this.serviceBase + 'api/Trademark/GetAllApplicationUserCount', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    GetDesignRefuseDate(pp: string ) {

      var data = {
        ApplicationId: pp



      };
      return this.http
        .get(this.serviceBase + 'api/DesignExaminer/GetDesignRefuseDate', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    GetPatentRefuseDate(pp: string ) {

      var data = {
        ApplicationId: pp



      };
      return this.http
        .get(this.serviceBase + 'api/DesignExaminer/GetPatentRefuseDate', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

 UpdateRenewalFormStatus(pp: string,pp2: string  ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2


      };
      return this.http
        .get(this.serviceBase + 'api/Recordal/UpdateRenewalFormStatus', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }
    UpdateCertPaymentById(pp: string,pp2: string ,pp3: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2 ,
        TransactionId: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/Certificate/UpdateCertPaymentById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }


    UpdateCertPatentPaymentById(pp: string,pp2: string ,pp3: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2 ,
        TransactionId: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/PatentCertificate/UpdateCertPaymentById', { params: data })
        .toPromise()
        .then((data) => {
          return data;
        });
    }

    UpdateCertDesignPaymentById(pp: string,pp2: string ,pp3: string ) {

      var data = {
        ApplicationId: pp ,
        RequestById: pp2 ,
        TransactionId: pp3

      };
      return this.http
        .get(this.serviceBase + 'api/DesignRegistra/UpdateCertPaymentById', { params: data })
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


    SaveFeeList(formData) {

      var token = localStorage.getItem('access_tokenexpire');

      const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
      return this.http.post( this.serviceBase + 'api/FeeList/SaveFeeList', formData ,{headers})
                  .toPromise()

                  .then(data => {  return data; });

    }




  Register(formData) {


    return this.http.post( this.serviceBase + 'api/UserManagement/EmailVerification', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  SavePwallet(formData) {


    return this.http.post( this.serviceBase + 'api/Trademark/SaveApplication', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  SavePatent(formData) {


    return this.http.post( this.serviceBase + 'api/Patent/SaveApplication', formData)
                .toPromise()

                .then(data => {  return data; });

  }

   SaveDesign(formData) {


    return this.http.post( this.serviceBase + 'api/Design/SaveApplication', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  SaveInvention(formData) {


    return this.http.post( this.serviceBase + 'api/Patent/SavePatentInvention', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  SaveDesignInvention(formData) {


    return this.http.post( this.serviceBase + 'api/Design/SaveDesignInvention', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  SavePriority(formData) {


    return this.http.post( this.serviceBase + 'api/Patent/SavePatentPriority', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  SaveDesignPriority(formData) {


    return this.http.post( this.serviceBase + 'api/Design/SaveDesignPriority', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  SaveDesignCoApplicant(formData) {


    return this.http.post( this.serviceBase + 'api/Design/SaveDesignCoApplicant', formData)
                .toPromise()

                .then(data => {  return data; });

  }





  SaveOppositionForm(formData) {


    return this.http.post( this.serviceBase + 'api/Opposition/SaveOppositionForm', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  SaveCounterOppositionForm(formData) {


    return this.http.post( this.serviceBase + 'api/Opposition/SaveCounterOppositionForm', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  SaveRenewalForm(formData) {


    return this.http.post( this.serviceBase + 'api/Recordal/SaveRenewalForm', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  SaveRenewalDesignForm(formData) {


    return this.http.post( this.serviceBase + 'api/Recordal/SaveRenewalDesignForm', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  SaveMergerForm(formData) {


    return this.http.post( this.serviceBase + 'api/Recordal/SaveMergerForm', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  SaveCertificatePayment(formData) {


    return this.http.post( this.serviceBase + 'api/Certificate/SaveCertificatePayment', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  ForgotPassword(formData) {


    return this.http.post( this.serviceBase + 'api/UserManagement/ForgotPassword', formData)
                .toPromise()

                .then(data => {  return data; });

  }



  ChangePassword(formData) {
  //  var token = localStorage.getItem('access_tokenexpire');
    var token = localStorage.getItem('access_tokenexpire');





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


  UpdateProfile(formData) {



    //return this.http.post( this.serviceBase + 'api/UserManagement/UpdateIndividualRecord', formData)
    return this.http.post( this.serviceBase + 'api/UserManagement/UpdateProfile', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  GetImage(formData) {



    //return this.http.post( this.serviceBase + 'api/UserManagement/UpdateIndividualRecord', formData)
    return this.http.post( this.serviceBase + 'api/Trademark/GetImage', formData)
                .toPromise()

                .then(data => {  return data; });

  }


  AssignUser(formData) {



    //return this.http.post( this.serviceBase + 'api/UserManagement/UpdateIndividualRecord', formData)
    return this.http.post( this.serviceBase + 'api/UserManagement/AssignUser', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  checkAccess(vurl) {
    var pp3 =[]
    var ppp2 = localStorage.getItem('Roles');
     pp3= JSON.parse(ppp2);


    let obj = pp3.find(o => o.url.toUpperCase() === vurl.toUpperCase());

    if (obj) {
      return true
    }
    else {
      return false
    }
  }

  UpdateUserFunction(formData) {



    //return this.http.post( this.serviceBase + 'api/UserManagement/UpdateIndividualRecord', formData)
    return this.http.post( this.serviceBase + 'api/UserManagement/UpdateUser', formData)
                .toPromise()

                .then(data => {  return data; });

  }

  UpdateFeeList(formData) {



    //return this.http.post( this.serviceBase + 'api/UserManagement/UpdateIndividualRecord', formData)
    return this.http.post( this.serviceBase + 'api/FeeList/UpdateFeeList', formData)
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


   GetUserFromDepartment() {


		return this.http
			.get(this.serviceBase + 'api/UserManagement/GetUserFromDepartment')
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetUserFromDesignDepartment() {


		return this.http
			.get(this.serviceBase + 'api/UserManagement/GetUserFromDesignDepartment')
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetUserFromDesignAppealDepartment() {


		return this.http
			.get(this.serviceBase + 'api/UserManagement/GetUserFromDesignAppealDepartment')
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetUserById(pp: string) {

		var data = {
      Id: pp
		};
		return this.http
			.get(this.serviceBase + 'api/UserManagement/GetUserFromId', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetAknwoledgment(pp: string) {

		var data = {
      pwalletid: pp
		};
		return this.http
			.get(this.serviceBase + 'api/Trademark/GetAknwoledgment', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetTrademarkLogo() {


		return this.http
			.get(this.serviceBase + 'api/Trademark/GetTrademarkLogo')
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetTrademarkUser() {


		return this.http
			.get(this.serviceBase + 'api/UserManagement/GetTrademarkUser')
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetPatentUser() {


		return this.http
			.get(this.serviceBase + 'api/UserManagement/GetPatentUser')
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetDesignUser() {


		return this.http
			.get(this.serviceBase + 'api/UserManagement/GetDesignUser')
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetAknwoledgmentByUserid(pp: string) {

		var data = {
      userid: pp
		};
		return this.http
			.get(this.serviceBase + 'api/Trademark/GetAknwoledgmentByUserid', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }
  UpDatePwalletById(pp: string ,pp2: string) {

		var data = {
      pwalletid: pp ,
      transid: pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Trademark/UpDatePwalletById', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


UpDatePatentTransactionById(pp: string ,pp2: string) {

		var data = {
      pwalletid: pp ,
      transid: pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Patent/UpDatePatentTransactionById', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  UpDateDesignTransactionById(pp: string ,pp2: string) {

		var data = {
      pwalletid: pp ,
      transid: pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Design/UpDateDesignTransactionById', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  all() {
    var token = localStorage.getItem('access_tokenexpire');

    const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);

		return this.http
			.get(this.serviceBase + 'api/audit/all',{headers})
			.toPromise()
			.then((data) => {
				return data;
			});
  }



  GetPatentApplicationById(pp : string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      Applicationid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Patent/GetPatentApplicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetDesignApplicationById(pp : string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      Applicationid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Design/GetDesignApplicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  CancelDesignApplicationById(pp : string ,pp2 : string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {
      userid:pp ,
      Applicationid:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Design/CancelDesignApplicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  CancelPatentApplicationById(pp : string ,pp2 : string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {
      userid:pp ,
      Applicationid:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Design/CancelPatentApplicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetPatentApplicationByUserId(pp : string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      userid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Patent/GetPatentApplicationByUserId', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetDesignApplicationByUserId(pp : string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      userid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Design/GetDesignApplicationByUserId', { params: data,headers })
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


   GetPatentType(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Patent/GetAllPatentType', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetAllDesignType(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/DesignType/GetAllDesignType', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetPrelimSearch(pp : string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp
		};
		return this.http
			.get(this.serviceBase + 'api/preliminary/GetPrelimSearch', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetPrelimSearchByUserid(pp : string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp
		};
		return this.http
			.get(this.serviceBase + 'api/preliminary/GetPrelimSearchByUserid', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetFreshApplication(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Search/GetFreshApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetExactSearch(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      title:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Search/GetExactSearch', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetMeaningSearch(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      title:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Search/GetMeaningSearch', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetOppositionFreshApplication(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Opposition/GetFreshApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetCertificateFreshApplication(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Certificate/GetFreshApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetCertificateApplicationById(pp2:string,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      ApplicationId:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Certificate/GetApplicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetApplicationById3(pp2:string,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      ApplicationId:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Recordal/GetApplicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetDesignApplicationById3(pp2:string,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      ApplicationId:pp
		};
		return this.http
			.get(this.serviceBase + 'api/DesignCertificate/GetApplicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetApplicationByAppid(pp2:string,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      ApplicationId:pp
		};
		return this.http
			.get(this.serviceBase + 'api/DesignCertificate/GetApplicationByAppid', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetDesignApplicationByIdCount(pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {


      ApplicationId:pp
		};
		return this.http
			.get(this.serviceBase + 'api/DesignCertificate/ApplicationCount', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetByRegistrationId(pp2:string,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      ApplicationId:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Recordal/GetByRegistrationId', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetPaidCertificate(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Certificate/GetPaidCertificate', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetIssuedCertificate(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Certificate/GetIssuedCertificate', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetIssuedCertificate2(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Recordal/GetIssuedCertificate', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetRecordalRenewalCertificate(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Recordal/GetRecordalRenewalCertificate', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetRecordalRenewalDesignCertificate(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Recordal/GetRecordalRenewalDesignCertificate', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetRecordalUpdateRenewalDesign(pp:string,pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp ,
      AppId:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Recordal/GetRecordalUpdateRenewalDesign', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }




  GetNewJudgment(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Opposition/GetNewJudgment', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }



  GetUserAppeal(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Registra/GetUserAppeal', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetAppealCount(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Registra/GetAppealCount', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


GetReceiveAppealCount(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Registra/GetReceiveAppealCount', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  TrademarkUserRoleCount() {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);

		return this.http
			.get(this.serviceBase + 'api/UserManagement/TrademarkUserRoleCount')
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  PatentUserRoleCount() {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);

		return this.http
			.get(this.serviceBase + 'api/UserManagement/PatentUserRoleCount')
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  DesignUserRoleCount() {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);

		return this.http
			.get(this.serviceBase + 'api/UserManagement/DesignUserRoleCount')
			.toPromise()
			.then((data) => {
				return data;
			});
  }




  GetAppeal(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Registra/GetAppeal', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  TreatUserAppeal(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Registra/TreatUserAppeal', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  UpdateBatch(pp2:string ,pp:string[]) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      BatchData:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Publication/UpdateBatch', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetUserKivApplication(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/GetUserKivApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }



  GetUserKivApplicationCount(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/GetUserKivApplicationCount', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }



  GetExaminerReconductSearch(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/GetExaminerReconductSearch', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetExaminerKiv(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/GetExaminerKivApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }
  GetRefuseApplicationById(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      ApplicationId:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/GetRefuseApplicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  SendUserEmail2(pp2:string ,pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      userid:pp ,
      Comment:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/SendUserEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  PatentCertificateSendUserEmail(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      applicationId:pp

		};
		return this.http
			.get(this.serviceBase + 'api/PatentCertificate/SendUserEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  DesignCertificateSendUserEmail(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      applicationId:pp

		};
		return this.http
			.get(this.serviceBase + 'api/DesignCertificate/SendUserEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  OppositionSendUserEmail2(pp2:string ,pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      userid:pp ,
      Comment:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/Opposition/SendUserEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  SendUserEmail3(pp2:string ,pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      userid:pp ,
      Comment:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/MailToReconductSearch', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }



  SendMailReconductSearch(pp2:string ,pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      userid:pp ,
      Comment:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/PatentExaminer/MailToReconductSearch', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  SendMailReconductSearch2(pp2:string ,pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      userid:pp ,
      Comment:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/DesignExaminer/MailToReconductSearch', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetExaminerFreshApplication(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/GetFreshApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetPublicationFreshApplication(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Publication/GetFreshApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  SendRegistraEmail(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      Appid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Publication/SendRegistraEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  SendRegistraAppealEmail(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      Appid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/PatentExaminer/SendRegistraAppealEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  SendRegistraDesignAppealEmail(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      Appid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/DesignExaminer/SendRegistraAppealEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  SendRegistraAppealReply(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      Appid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/DesignExaminer/SendRegistraAppealReply', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  SendAppealUnitEmail(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      Appid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Registra/SendAppealUnitEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetRefuseApplicationByUserid(pp2:string ) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2

		};
		return this.http
			.get(this.serviceBase + 'api/Publication/GetRefuseApplicationByUserid', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetPatentRefuseApplicationByUserid(pp2:string ) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2

		};
		return this.http
			.get(this.serviceBase + 'api/PatentExaminer/GetRefuseApplicationByUserid', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }



  GetBatches(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Publication/GetBatches', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetDesignBatches(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/DesignPublication/GetDesignBatches', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetPublicationById(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      Id:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Publication/GetPublicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetDesignPublicationById(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      Id:pp
		};
		return this.http
			.get(this.serviceBase + 'api/DesignPublication/GetPublicationById', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetPublicationByRegistrationId(pp2:string ,pp:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2 ,
      Id:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Publication/GetPublicationByRegistrationId', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetExaminerTreatedApplication(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/GetTreatedApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetApplicationByUserid(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/GetApplicationByUserid', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetPreviousComment(pp2:string[] ,pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      status:pp2,
      RequestById:pp ,
      ID:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/Examiner/GetPreviousComment', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetPatentRefusalComment(pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {


      RequestById:pp ,
      ID:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/PatentExaminer/GetRefusalComment', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetDesignRefusalComment(pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {


      RequestById:pp ,
      ID:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/DesignExaminer/GetRefusalComment', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  SendPatentRegistraEmail(pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {


      RequestById:pp ,
      ID:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/PatentExaminer/SendRegistraEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  SendDesignRegistraEmail(pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {


      RequestById:pp ,
      ID:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/DesignExaminer/SendRegistraEmail', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetExaminerPreviousComment(pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {


      RequestById:pp ,
      ID:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/PatentExaminer/GetPreviousComment', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetExaminerPreviousComment2(pp:string,pp3:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {


      RequestById:pp ,
      ID:pp3
		};
		return this.http
			.get(this.serviceBase + 'api/DesignExaminer/GetPreviousComment', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }
  GetKivApplication(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Search/GetKivApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetTreatedApplication(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Search/GetTreatedApplication', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

GetTradeMarkType(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Search/GetTradeMarkType', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetNationalClass() {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);

		return this.http
			.get(this.serviceBase + 'api/ParameterSetup/GetNationalClass')
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetFeeListByName(pp : string,pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {
      FeeListName:pp,
      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/FeeList/GetFeeListByName', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetStateByCountry(pp : string) {
   // var token = localStorage.getItem('access_tokenexpire');

   //  const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {


      Countryid:pp
		};
		return this.http
			.get(this.serviceBase + 'api/State/GetStateByCountry', { params: data })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetLGAByState(pp : string) {
    // var token = localStorage.getItem('access_tokenexpire');

    //  const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
     var data = {


      State2:pp
     };
     return this.http
       .get(this.serviceBase + 'api/LGA/GetLGAByState', { params: data })
       .toPromise()
       .then((data) => {
         return data;
       });
   }


  GetAllProduct(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Product/GetAllProduct', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetAllDepartment(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Department/GetAllDepartment', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetAllSectors(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Sector/GetAllSectors', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetAllTMApplicationStatus(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/TMApplicationStatus/GetAllTMApplicationStatus', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetAllPTApplicationStatus(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      ActionBy:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/PTApplicationStatus/GetAllPTApplicationStatus', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetAllDSApplicationStatus(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      ActionBy:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/DSApplicationStatus/GetAllDSApplicationStatus', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetAllMinistry(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Ministry/GetAllMinistry', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetAllUnit(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Unit/GetAllUnit', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }

  GetAllFeeLists(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/FeeList/GetAllFeeLists', { params: data,headers })
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

      RequestById:pp2,
      PatentId:pp
		};
		return this.http
			.get(this.serviceBase + 'api/Menu/GetAllParentMenus', { params: data,headers })
			.toPromise()
			.then((data) => {
				return data;
			});
  }


  GetMenuOthers(pp2:string) {
    var token = localStorage.getItem('access_tokenexpire');

     const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
		var data = {

      RequestById:pp2
		};
		return this.http
			.get(this.serviceBase + 'api/Menu/GetAllMenus', { params: data,headers })
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


