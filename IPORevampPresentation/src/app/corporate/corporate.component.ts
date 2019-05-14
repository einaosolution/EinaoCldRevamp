
import { Component, OnInit ,ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import {ActivatedRoute} from "@angular/router";
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-corporate',
  templateUrl: './corporate.component.html',
  styleUrls: ['./corporate.component.css']
})
export class CorporateComponent implements OnInit {
  userform: FormGroup;
  @ViewChild("fileInput") fileInput;
  companyname: FormControl;
 CompanyRegistration: FormControl;
 companytelephone: FormControl;
 companyemail: FormControl;
 companywebsite: FormControl;
   FirstName: FormControl;
   LastName: FormControl;
   Gender: FormControl;
   DateofBirth: FormControl;
   MeansofIdentification: FormControl;
   MobileNumber: FormControl;
   Street: FormControl;
   City: FormControl;
   State: FormControl;
   PostCode: FormControl;
   Country: FormControl;
   queryParam: string;
   submitted:boolean=false;
   UserEmail:string ="";

  varray4 = [{ YearName: 'Argentina', YearCode: 'AR' }, {  YearName: 'Austria', YearCode: 'AT' }  ,{  YearName: 'Cameroon', YearCode: 'CM' },{  YearName: 'China', YearCode: 'CN' } ,{  YearName: 'Nigeria', YearCode: 'NG' } ]
  varray44 = [{ YearName: 'Driver License', YearCode: 'Driver License' }, {  YearName: 'International Passport', YearCode: 'International Passport' }  ,{  YearName: 'National Identity Card', YearCode: 'National Identity Card' },{  YearName: 'Voters Registration Card', YearCode: 'Voters Registration Card' }  ]
  constructor(private fb: FormBuilder ,private route: ActivatedRoute ,private registerapi :ApiClientService ,private router: Router ,private spinner: NgxSpinnerService) { }


  onSubmit2() {
    this.userform.reset();
   }

   onSubmit() {
   var  formData = new FormData();
   this.submitted= true;
   let fi = this.fileInput.nativeElement;

   if (fi.files && fi.files[0]) {
    let fileToUpload = fi.files[0];
   formData.append("FileUpload", fileToUpload);

   }

   else {

    Swal.fire(
     "Please Upload File",
      '',
      'error'
    )

    return;

   }

   if (this.userform.valid) {
    this.spinner.show();
   formData.append("Email",this.UserEmail);
   formData.append("FirstName",this.userform.value.FirstName);
   formData.append("companyname",this.userform.value.companyname);
   formData.append("CompanyRegistration",this.userform.value.CompanyRegistration);
   formData.append("companytelephone",this.userform.value.companytelephone);
   formData.append("companyemail",this.userform.value.companyemail);
   formData.append("companywebsite",this.userform.value.companywebsite);
   formData.append("LastName",this.userform.value.LastName);
   formData.append("Gender",this.userform.value.Gender);
   formData.append("DateofBirth",formatDate(this.userform.value.DateofBirth, 'MM/dd/yyyy', 'en'));
   formData.append("Identification",this.userform.value.MeansofIdentification);
   formData.append("MobileNumber",this.userform.value.MobileNumber);
   formData.append("Street",this.userform.value.Street);
   formData.append("City",this.userform.value.City);
   formData.append("State",this.userform.value.State);
   formData.append("PostCode",this.userform.value.PostCode);
   formData.append("Country",this.userform.value.Country);



   this.registerapi
   .UpdateUser2(formData)
   .then((response: any) => {
    this.spinner.hide();
    this.router.navigateByUrl('login');

   })
            .catch((response: any) => {
              this.submitted= false;
              this.spinner.hide();
              Swal.fire(
                response.error.message,
                '',
                'error'
              )
    })

  }

  else {
    alert("Form Invalid")
  }

   }
  ngOnInit() {

    var firstname = "";
    var lastname ="";
    var email = "";
    var email2 = "";
    const firstParam: string = this.route.snapshot.queryParamMap.get('page');
    if (localStorage.getItem('username')) {


      email = localStorage.getItem('username');

    }

    if (firstParam) {
      email2 = firstParam;

    }

    this.FirstName = new FormControl('', [
      Validators.required

    ]);

    this.LastName = new FormControl('', [
      Validators.required

    ]);

    this.Gender = new FormControl('', [
      Validators.required

    ]);

    this.DateofBirth = new FormControl('', [
      Validators.required

    ]);

    this.MeansofIdentification = new FormControl('', [
      Validators.required

    ]);

    this.MobileNumber = new FormControl('', [
      Validators.required

    ]);

    this.Street = new FormControl('', [
      Validators.required

    ]);

    this.City = new FormControl('', [
      Validators.required

    ]);

    this.State = new FormControl('', [
      Validators.required

    ]);

    this.PostCode = new FormControl('', [
      Validators.required

    ]);

    this.Country = new FormControl('', [
      Validators.required

    ]);

    this.companyname = new FormControl('', [
      Validators.required

    ]);

    this.CompanyRegistration = new FormControl('', [
      Validators.required

    ]);

    this.companytelephone = new FormControl('', [

    ]);

    this.companyemail = new FormControl('', [


    ]);

    this.companywebsite = new FormControl('', [

    ]);


    this.userform = new FormGroup({

      FirstName: this.FirstName,


      LastName: this.LastName,
      Gender: this.Gender,

      DateofBirth: this.DateofBirth ,
      MeansofIdentification: this.MeansofIdentification ,
      MobileNumber: this.MobileNumber ,
      Street: this.Street ,
      City: this.City ,
      State: this.State ,
      PostCode: this.PostCode ,
      Country: this.Country ,
      companyname: this.companyname ,
      CompanyRegistration: this.CompanyRegistration ,
      companytelephone: this.companytelephone ,
      companyemail: this.companyemail ,
      companywebsite: this.companywebsite




    });

    if ( email2) {
      this.registerapi
      .GetEmail2(email2)
      .then((response: any) => {

  this.UserEmail = response.email;

  (<FormControl> this.userform.controls['FirstName']).setValue(response.firstName);
  (<FormControl> this.userform.controls['LastName']).setValue( response.lastName);


   console.log("response")
   console.log(response)
      })
               .catch((response: any) => {
                 this.submitted= false;
                 alert("error occured 1")

       })

      }

      if (email) {
        this.registerapi
        .GetEmail(email)
        .then((response: any) => {
          console.log("response email")
          console.log(response)
    this.UserEmail = response.email;

    (<FormControl> this.userform.controls['FirstName']).setValue(response.firstName);
    (<FormControl> this.userform.controls['LastName']).setValue( response.lastName);


     console.log("response")
     console.log(response)
        })
                 .catch((response: any) => {
                   this.submitted= false;
                   alert("error occured 2")

         })

        }
  }

}
