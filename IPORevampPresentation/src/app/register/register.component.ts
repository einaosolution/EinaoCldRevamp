import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
//const Swal = require('sweetalert2')



@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
 // styleUrls: ['./register.component.css']
 styleUrls: ['../../assets/css/style.min.css']
})
export class RegisterComponent implements OnInit {
  userform: FormGroup;

  Password: FormControl;
  Email: FormControl;
  submitted:boolean=false;
  firstName: FormControl;
  lastName: FormControl;
  middleName: FormControl;
  address: FormControl;
  city: FormControl;
  role: FormControl;
  state: FormControl;
  program : FormControl;
  phoneNumber : FormControl;
  agreement : FormControl;
  dob: FormControl;
  busy: Promise<any>;
  pp4:boolean=false;
  public formSubmitAttempt: boolean;

  varray4 = [{ YearName: 'Applicant/Individual', YearCode: '1' }, { YearName: 'Corporate/Agent', YearCode: '2' } ]
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router ,private spinner: NgxSpinnerService) { }

  valuechange(een ) {





     var  email = this.userform.value.Email



    this.busy = this.registerapi
    .UserCount(email)
    .then((response: any) => {
     console.log("response=")
     console.log(response)
     if (response.success =="User Already Exist") {
      alert(response.success) ;
      (<FormControl> this.userform.controls['Email']).setValue("");
     }


    })
             .catch((response: any) => {
              alert("Error Occured")
     }
     );

  }
  onSubmit() {
this.submitted= true;

if (!this.userform.value.agreement) {
  Swal.fire(
    "Select Terms And Agreement",
    '',
    'error'
  )

  return;
}



if (this.userform.valid) {

  this.spinner.show();

  var kk = {
    Email:this.userform.value.Email  ,
    First_Name:this.userform.value.firstName ,
    Last_Name:this.userform.value.lastName ,
    Category:this.userform.value.program


  }



 // this.router.navigate(['/Emailverification']);


  this.registerapi
    .Register(kk)
    .then((response: any) => {
      this.spinner.hide();

      this.submitted=false;
      Swal.fire(
        'Saved Succesfully , Activation email has been sent',
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



    this.pp4= true;

    this.Email = new FormControl('', [Validators.required, Validators.email]);









    this.firstName = new FormControl('', [
      Validators.required

    ]);
    this.lastName = new FormControl('', [
      Validators.required

    ]);

    this.agreement = new FormControl('', [
      Validators.required

    ]);






    this.program = new FormControl('', [
      Validators.required

    ]);













    this.userform = new FormGroup({

      Email: this.Email,


      firstName: this.firstName,
      lastName: this.lastName,

      program: this.program ,
      agreement:this.agreement


    });

  }

  }

}
