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
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  userform: FormGroup;
  userform2: FormGroup;

  Password: FormControl;
  Password2: FormControl;
  submitted:boolean=false;
  submitted2:boolean=false;
  Email: FormControl;
  Email2: FormControl;
  showerror:boolean= false;
  messages = ""
  public formSubmitAttempt: boolean;
  busy: Promise<any>;

  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) {



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
     if (response.content.category =="1") {
      localStorage.setItem('username', this.userform.value.Email);
      this.router.navigateByUrl('Individual');
      return ;
     }

     else {
      localStorage.setItem('username', this.userform.value.Email);
      this.router.navigateByUrl('Corporate');
      return ;
     }

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

  }


  }

}
