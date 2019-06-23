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
  selector: 'app-forgetpassword',
  templateUrl: './forgetpassword.component.html',
  styleUrls: ['./forgetpassword.component.css']
})
export class ForgetpasswordComponent implements OnInit {

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
  code:string ="";
  request:string ="";
  messages = ""
  public formSubmitAttempt: boolean;
  busy: Promise<any>;
  password22:string = null;
  password23:string = null;
  public account = {
    password: null,
    confirmpasword:null
};
  public barLabel: string = "Password strength:";
  public myColors = ['#DD2C00', '#FF6D00', '#FFD600', '#AEEA00', '#00C853'];

  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) {



   }
  onSubmit(f) {

    this.submitted= true;




    if (f) {
      this.spinner.show();



     // this.router.navigate(['/Emailverification']);


      this.registerapi
        .ResetPassword(this.code,this.request,this.account.password,this.account.confirmpasword)
        .then((response: any) => {
          this.spinner.hide();

          this.submitted=false;

          Swal.fire(
            'Password reset successful ,  email has been sent to your email',
            '',
            'success'
          )
       //  this.router.navigate(['/Emailverification']);

       this.userform.reset();


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

    else {
      alert("Form not valid")
     // this.submitted= false;
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

    this.code = this.route.snapshot.queryParamMap.get('code');
    this.request= this.route.snapshot.queryParamMap.get('request');


//if (this.registerapi.password2()) {
 /// this.router.navigateByUrl('/home');
//}

//this.registerapi.changepassword2(false )


    this.Email = new FormControl('', [

    ]);
    this.Email2 = new FormControl('', [

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
