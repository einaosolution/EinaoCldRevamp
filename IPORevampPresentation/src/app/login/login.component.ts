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
  selector: 'app-login',
  templateUrl: './login.component.html',
 styleUrls: ['./login.component.css']
 //styleUrls: ['../../assets/css/style.min.css']
})
export class LoginComponent implements OnInit {

  userform: FormGroup;
  userform2: FormGroup;

  Password: FormControl;
  submitted:boolean=false;
  submitted2:boolean=false;
  Email: FormControl;
  Email2: FormControl;
  showerror:boolean= false;
  messages = ""
  public formSubmitAttempt: boolean;
  busy: Promise<any>;

  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) {

this.getIpAddress();

   }


   getIpAddress() {

    $.getJSON('https://api.ipify.org?format=json', function(data){
      console.log("ip");
      localStorage.setItem('ip',data.ip );


      console.log(data);
  });
}


  onSubmit() {

    this.submitted= true;



    if (this.userform.valid) {
      this.spinner.show();
      var kk = {
        Username:this.userform.value.Email  ,
        Password:this.userform.value.Password ,
        RememberMe:true



      }

      this.registerapi
      .Login(kk)
      .then((response: any) => {
        this.spinner.hide();

        this.submitted=false;

     //  this.router.navigate(['/Emailverification']);


     console.log(response.content)
     if (response.content.category =="1" && !response.content.registrationcomplete ) {
      localStorage.setItem('username2', this.userform.value.Email);
      localStorage.setItem('ChangePassword', "False");


      this.registerapi.settoken("");
     // localStorage.setItem('access_tokenexpire', response.content.Token);
      this.router.navigateByUrl('Individual');


      return ;
     }



     if (response.content.category =="2" && !response.content.registrationcomplete ) {
      localStorage.setItem('username2', this.userform.value.Email);
      localStorage.setItem('ChangePassword', "False");

      this.registerapi.settoken("");
     // localStorage.setItem('access_tokenexpire', response.content.Token);
      this.router.navigateByUrl('Corporate');


      return ;
     }




     if (!response.content.changepassword) {

      localStorage.setItem('username', this.userform.value.Email);
      localStorage.setItem('loggeduser', response.content.loggeduser);
      localStorage.setItem('UserId', response.content.userId);
     // this.registerapi.settoken("");
     localStorage.setItem('access_tokenexpire', response.content.token);
     localStorage.setItem('Roles',JSON.stringify( response.content.dynamicMenu));

     localStorage.setItem('ExpiryTime', response.content.expiryTime);
     if (response.content.profilepic ==null) {
      localStorage.setItem('profilepic', "");
    }

    else {
      localStorage.setItem('profilepic', response.content.profilepic);

    }

    localStorage.setItem('ChangePassword', "False");
      this.router.navigateByUrl('/PasswordChange');
      return ;
    }
    localStorage.setItem('username', this.userform.value.Email);

    localStorage.setItem('access_tokenexpire', response.content.token);
    localStorage.setItem('UserId', response.content.userId);
    localStorage.setItem('ExpiryTime', response.content.expiryTime);

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

//this.registerapi.VChangeEvent("Login");

  this.router.navigateByUrl('/home');
   //




    // this.userform.reset();

      })
               .catch((response: any) => {
                this.spinner.hide();
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

  }

  onSubmit3() {
    this.submitted2= true;

    if (this.userform2.valid) {

      this.spinner.show();
      var kk = {
        Username:this.userform2.value.Email2



      }



  this.registerapi
  .ForgotPassword(kk)
  .then((response: any) => {
    this.spinner.hide();


    this.submitted2=false;
    Swal.fire(
      'Submitted  Succesfully , email has been sent',
      '',
      'success'
    )
 //  this.router.navigate(['/Emailverification']);

 this.userform2.reset();

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

  onSubmit2() {




    $("#loginform").slideUp();
    $("#recoverform").fadeIn();


  }


  onSubmit2a() {



    $("#recoverform").slideUp();
    $("#loginform").fadeIn();



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

    if (this.islogged()) {
      this.router.navigateByUrl('/home');
     }

     else {



    this.Email = new FormControl('', [
      Validators.required, Validators.email
    ]);
    this.Email2 = new FormControl('', [
      Validators.required, Validators.email
    ]);

    this.Password = new FormControl('', [
      Validators.required

    ]);

    this.userform = new FormGroup({

      Email: this.Email,
      Password: this.Password ,


    });

    this.userform2 = new FormGroup({

      Email2: this.Email2



    });

  }


  }


}
