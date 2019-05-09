import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';
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
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router) {


   }
  onSubmit() {

    this.submitted= true;



    if (this.userform.valid) {

      if (this.userform.value.Email =="testuser@yahoo.com" &&  this.userform.value.Password =="pass12345" ) {

this.showerror = false;
//this.router.navigate(['/home']);
this.registerapi.settoken("kkkkk") ;
this.registerapi.VChangeEvent("kkkkk") ;
this.router.navigateByUrl('/home');




      }

      else {
        this.showerror = true;
        this.messages ="invalid username and password";

      }
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

    if (this.islogged()) {
      this.router.navigateByUrl('/logout');
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
