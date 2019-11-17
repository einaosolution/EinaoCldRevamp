import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
declare var jquery:any;
declare var $ :any;

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {
  userform: FormGroup;
  userform2: FormGroup;

  Password: FormControl;
  Password2: FormControl;
  submitted:boolean=false;
  passwordchanged:boolean=false;
  submitted2:boolean=false;
  Email: FormControl;
  Email2: FormControl;
  showerror:boolean= false;
  messages = ""
  public formSubmitAttempt: boolean;
  busy: Promise<any>;

  public account = {
    oldpassword: null,
   newpassword: null,
    confirmpasword:null
};
  public barLabel: string = "Password strength:";
  public myColors = ['#DD2C00', '#FF6D00', '#FFD600', '#AEEA00', '#00C853'];

  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) {

this.getIpAddress()

   }

   getIpAddress() {

    $.getJSON('https://api.ipify.org?format=json', function(data){
      console.log("ip");
      localStorage.setItem('ip',data.ip );


      console.log(data);
  });
}

loginuser(username ,password) {
  var kk = {
    Username:username  ,
    Password:password ,
    RememberMe:true



  }

  this.registerapi
  .Login(kk)
  .then((response: any) => {

 localStorage.setItem('username', localStorage.getItem('username'));

localStorage.setItem('access_tokenexpire', response.content.token);
localStorage.setItem('UserId', response.content.userId);
localStorage.setItem('ExpiryTime', response.content.expiryTime);
localStorage.setItem('Roleid', response.content.roleId);

localStorage.setItem('loggeduser', response.content.loggeduser);
localStorage.setItem('Roles',JSON.stringify( response.content.dynamicMenu));
if (response.content.profilepic ==null) {
  localStorage.setItem('profilepic', "");
}

else {
  localStorage.setItem('profilepic', response.content.profilepic);
}


this.registerapi.settoken(response.content.token) ;
localStorage.setItem('ChangePassword', "True");
localStorage.setItem('lastpasswordchange', response.content.lastpasswordchange);

console.log(response)

this.registerapi.VChangeEvent("task");




this.router.navigateByUrl('/redirect');






  })
           .catch((response: any) => {

            console.log("response error")
             console.log(response)


            Swal.fire(
              response.error.message,
              '',
              'error'
            )
   }
   );



}
  onSubmit(f) {

    this.submitted= true;



    if (f) {


      if (this.account.newpassword  === this.account.oldpassword) {
         Swal.fire(
                   "New Password Cannot Be same with old password",
                    '',
                    'error'
                  )

                  return;
      }


      if (this.account.newpassword  != this.account.confirmpasword) {
        Swal.fire(
                  "Confirm Password not equal to New Password",
                   '',
                   'error'
                 )

                 return;
     }
      this.spinner.show();
      var kk = {
        CurrentPassword:this.account.oldpassword  ,
        NewPassword:this.account.newpassword ,
        ConfirmPassword:this.account.confirmpasword ,
        Email:localStorage.getItem('username')


      }



     // this.router.navigate(['/Emailverification']);


      this.registerapi
        .ChangePassword(kk)
        .then((response: any) => {
          this.spinner.hide();

          this.submitted=false;

       //  this.router.navigate(['/Emailverification']);
      // this.userform.reset();
     //  this.registerapi.changepassword2(true )
       //this.registerapi.VChangeEvent("kkkkk")

       this.loginuser(localStorage.getItem('username')  ,this.account.newpassword )
     //  this.router.navigateByUrl('/home');
     //  this.userform.reset();

        })
                 .catch((response: any) => {
                  this.spinner.hide();
                   console.log(response)


                  Swal.fire(
                    response.error.message,
                    '',
                    'error'
                  )
         }
         );


    }

  }

  onSubmit3() {
    this.submitted2= true;

    if (this.userform2.valid) {

    }

  }

  onSubmit2() {




    $("#loginform").slideUp();
    $("#recoverform").fadeIn();


  }


  islogged() {

    if (this.registerapi.gettoken()) {

      return true ;
    }

    else {

      return false;
    }
  }



  ngOnInit() {

//if (this.registerapi.password2()) {
 /// this.router.navigateByUrl('/home');
//}

//this.registerapi.changepassword2(false )




if( window.localStorage )
{

if( !localStorage.getItem('firstLoad') )
  {
localStorage.removeItem('Roles');
    localStorage['firstLoad'] = true;
   window.location.reload();
  }
  else {

    this.Email = new FormControl('', [
      Validators.required
    ]);
    this.Email2 = new FormControl('', [
      Validators.required, Validators.email
    ]);

    this.Password = new FormControl('', [
      Validators.required

    ]);

    this.Password2 = new FormControl('', [
      Validators.required

    ]);

    this.userform = new FormGroup({

      Email: this.Email,
      Password: this.Password ,
      Password2: this.Password2 ,


    });

    this.userform2 = new FormGroup({

      Email2: this.Email2



    });

    localStorage.removeItem('firstLoad');


  }



}







  }

}
