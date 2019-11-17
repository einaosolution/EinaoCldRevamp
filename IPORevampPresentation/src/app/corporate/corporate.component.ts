
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
  IdentificationValue: FormControl;
   PostCode: FormControl;
   Country: FormControl;
   queryParam: string;
   submitted:boolean=false;
   UserEmail:string ="";
   public row2 = [];
   public row = [];
   public row4 = [];
   phonepattern ="^[\s()+-]*([0-9][\s()+-]*){6,20}$";
   minDate: Date;
  maxDate: Date;
   busy: Promise<any>;
     lga: FormControl;

  varray4 = [{ YearName: 'Argentina', YearCode: 'AR' }, {  YearName: 'Austria', YearCode: 'AT' }  ,{  YearName: 'Cameroon', YearCode: 'CM' },{  YearName: 'China', YearCode: 'CN' } ,{  YearName: 'Nigeria', YearCode: 'NG' } ]
  varray44 = [{ YearName: 'Driver License', YearCode: 'Driver License' }, {  YearName: 'International Passport', YearCode: 'International Passport' }  ,{  YearName: 'National Identity Card', YearCode: 'National Identity Card' },{  YearName: 'Voters Registration Card', YearCode: 'Voters Registration Card' }  ]
  constructor(private fb: FormBuilder ,private route: ActivatedRoute ,private registerapi :ApiClientService ,private router: Router ,private spinner: NgxSpinnerService) {
    this.getIpAddress()
    this.maxDate = new Date();
    this.minDate = new Date();
   }


  onSubmit2() {
    this.userform.reset();
   }


   getIpAddress() {

    $.getJSON('https://api.ipify.org?format=json', function(data){
      console.log("ip");
      localStorage.setItem('ip',data.ip );


      console.log(data);
  });
}


chng4()
{

  let test = this.fileInput.nativeElement;


let test2 = test.files[0];

if (test2.type =="image/jpeg" || test2.type =="image/png" ) {


}

else {
  alert("File Type Must be Jpeg Or Png")
 test.value = ''

 return ;
}


if(test2.size/ 1024>3000){

 alert("File Too Large,maxsize is 3mb")

 test.value = ''

 return ;

}

console.log("file input")

console.log(test2)

}
   onSubmit() {
   var  formData = new FormData();
   this.submitted= true;
   var regexp = /^[\s()+-]*([0-9][\s()+-]*){6,20}$/





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
   formData.append("meanofidentification_value",this.userform.value.IdentificationValue);
   formData.append("lgaid",this.userform.value.lga);



   this.registerapi
   .UpdateUser2(formData)
   .then((response: any) => {
    this.spinner.hide();
    this.router.navigateByUrl('logout');

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



     onChange($event) {

      // if (this.userform.value.Country =="" ) {


       this.busy =   this.registerapi
       .GetStateByCountry(this.userform.value.Country)
       .then((response: any) => {
         console.log("row2")
         console.log(response)
         this.row2 = response.content;



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


   onChange2($event) {

    // if (this.userform.value.Country =="" ) {


     this.busy =   this.registerapi
     .GetLGAByState(this.userform.value.State)
     .then((response: any) => {
       console.log("row3")
       console.log(response)
       this.row4 = response.content;



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

  ngOnInit() {

    var firstname = "";
    var lastname ="";
    var email = "";
    var email2 = "";
    const firstParam: string = this.route.snapshot.queryParamMap.get('page');
    var userid =localStorage.getItem('UserId');
    if (localStorage.getItem('username2')) {


      email = localStorage.getItem('username2');

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

    this.lga = new FormControl('', [
      Validators.required

    ]);


    this.DateofBirth = new FormControl('', [
      Validators.required

    ]);

    this.MeansofIdentification = new FormControl('', [
      Validators.required

    ]);

    this.IdentificationValue = new FormControl('', [
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
      Validators.required
    ]);

    this.companyemail = new FormControl('', [

      Validators.required,  Validators.email
    ]);

    this.companywebsite = new FormControl('', [

    ]);


    this.userform = new FormGroup({

      FirstName: this.FirstName,


      LastName: this.LastName,
      Gender: this.Gender,

      DateofBirth: this.DateofBirth ,
      MeansofIdentification: this.MeansofIdentification ,
      IdentificationValue:this.IdentificationValue,
      MobileNumber: this.MobileNumber ,
      Street: this.Street ,
      City: this.City ,
      lga:this.lga ,
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
  userid  = response.id;

  (<FormControl> this.userform.controls['FirstName']).setValue(response.firstName);
  (<FormControl> this.userform.controls['LastName']).setValue( response.lastName);





this.registerapi
.GetCountry("true",userid)
.then((response: any) => {

  console.log("Response2")
  this.row = response.content;
  console.log(response)



})
         .catch((response: any) => {

           console.log(response)


          Swal.fire(
            response.error.message,
            '',
            'error'
          )

})

   console.log("response")
   console.log(response)
      })
               .catch((response: any) => {
                 this.submitted= false;
                 alert("error occured 1")

       })

      }

     else if (email) {
        this.registerapi
        .GetEmail(email)
        .then((response: any) => {
          console.log("response email")
          console.log(response)
    this.UserEmail = response.email;
    userid  = response.id;

    (<FormControl> this.userform.controls['FirstName']).setValue(response.firstName);
    (<FormControl> this.userform.controls['LastName']).setValue( response.lastName);


    this.registerapi
    .GetAllStates(userid)
    .then((response: any) => {


      this.row2 = response.content;

      console.log(response)



    })
             .catch((response: any) => {

               console.log(response)


              Swal.fire(
                response.error.message,
                '',
                'error'
              )
  })

  this.registerapi
  .GetCountry("true",userid)
  .then((response: any) => {

    console.log("Response2")
    this.row = response.content;
    console.log(response)



  })
           .catch((response: any) => {

             console.log(response)


            Swal.fire(
              response.error.message,
              '',
              'error'
            )

  })
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
