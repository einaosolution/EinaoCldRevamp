import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';


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
  dob: FormControl;
  busy: Promise<any>;
  pp4:boolean=false;
  public formSubmitAttempt: boolean;
  varray4 = [{ YearName: 'Applicant/Individual', YearCode: 'Applicant/Individual' }, { YearName: 'Corporate/Agent', YearCode: 'Corporate/Agent' } ]
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router) { }

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

if (this.userform.valid) {

  var kk = {
    Username:this.userform.value.Email  ,
    First_Name:this.userform.value.firstName ,
    Last_Name:this.userform.value.lastName ,
    Category:this.userform.value.program


  }



  this.router.navigate(['/Emailverification']);


  //this.registerapi
  //  .Register(kk)
    //.then((response: any) => {
    //  alert("User Registered Successfully")
    //  this.router.navigate(['/Emailverification']);

   // })
         //    .catch((response: any) => {
          //    alert("Error Occured")
    // }
    // );


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






    this.program = new FormControl('', [
      Validators.required

    ]);













    this.userform = new FormGroup({

      Email: this.Email,


      firstName: this.firstName,
      lastName: this.lastName,

      program: this.program


    });

  }

  }

}
