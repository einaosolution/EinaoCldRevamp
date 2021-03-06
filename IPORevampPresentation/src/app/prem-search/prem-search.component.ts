import { Component, OnInit,ViewChild,OnDestroy } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { Subject } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';






import { map } from 'rxjs/operators';

import 'datatables.net'
import 'datatables.net-dt'


declare var $;

@Component({
  selector: 'app-prem-search',
  templateUrl: './prem-search.component.html',
  styleUrls: ['./prem-search.component.css']
})
export class PremSearchComponent implements OnInit {

  userform: FormGroup;
  submitted:boolean=false;

  Firstname: FormControl;
  Lastname: FormControl;
  registering: FormControl;
  settingcode
  product: FormControl;
  industry: FormControl;
  UserEmail: FormControl;
  description: FormControl;
  row:any[] =[]
  rows:[] ;
  row2:any =[] ;
  row4:any =[] ;
  row5:any
  row22:any
  tot:any;
  exampleData
  row3:any ;
  busy: Promise<any>;
  vshow:boolean = false;

  settingoff:boolean =false;
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  config = {

    height: 300,
  }


  constructor(private router: Router ,private registerapi :ApiClientService) { }

  onChange(deviceValue) {
    if (deviceValue =="products") {
      this.vshow = true;

    }

    else {
      this.vshow = false;
    }
    console.log(deviceValue);
}
  onSubmit() {
    this.submitted= true;


    var vcount = this.userform.value.description.length

if (  vcount > 0) {
    for(let count = 0; count < this.userform.value.description.length; count++) {



      this.row4.push(this.userform.value.description[count])
   }

  }

  console.log("description")
  console.log(this.row4)


    if (this.userform.valid) {

      var userid =parseInt( localStorage.getItem('UserId'));

      var PrelimData = {

        Firstname: this.userform.value.Firstname,
        Lastname: this.userform.value.Lastname,
        registering: this.userform.value.registering,
        product: this.userform.value.product,
        industry: this.userform.value.industry,
        description: this.row4.toString(),
        UserEmail:this.userform.value.UserEmail ,
        userid:userid



    };

    localStorage.setItem('PrelimData',JSON.stringify( PrelimData));


if ( this.settingoff) {

  var Payment= {

      description: this.row5.description,
      quatity: "1",
      amount: this.tot ,
      paymentref:"x13389996777",
      transactionid:''



  };


localStorage.setItem('Payment',JSON.stringify( Payment));

  localStorage.setItem('PaymentType',"PrelimSearch");
 localStorage.setItem('settings',this.settingcode);

    this.router.navigateByUrl('/Dashboard/processinginvoice');


}

else {



    this.row = []
    this.row.push(1)
   // var userid = localStorage.getItem('UserId');

    var kk = {
      FeeIds:this.row ,
      UserId:userid ,



    }

    this.busy =  this.registerapi
    . InitiateRemitaPayment(kk)
    .then((response: any) => {

      console.log("RemittaResponse")
    //  this.rows = response.content;
      console.log(response.content)

      this.row22 =response.content

     // this.rrr =this.row22.rrr
     localStorage.removeItem('row22');

      localStorage.setItem('row22',JSON.stringify( this.row22));

    // this.value = this.row22.rrr


     // this.vshow2 = true;


      var Payment= {

       description: this.row5.description,
       quatity: "1",
       amount: this.tot,
       paymentref:this.row22.rrr,
       transactionid:''



   };

  localStorage.setItem('Payment',JSON.stringify( Payment));

  localStorage.setItem('PaymentType',"PrelimSearch");
    //  $(".validation-wizard").steps("next");

    this.router.navigateByUrl('/Dashboard/processinginvoice');



    })
             .catch((response: any) => {

               console.log(response)


              Swal.fire(
                response.error.message,
                '',
                'error'
              )

 })

}


    }

  }

  ngOnInit() {

   let useremail = localStorage.getItem('username');

    if (this.registerapi.checkAccess("#/Dashboard/PremSearch"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }


    this.registerapi.setPage("Prelim_Search")

    this.registerapi.VChangeEvent("Prelim_Search");
    this.Firstname = new FormControl('', [
      Validators.required
    ]);

    this.Lastname = new FormControl('', [
      Validators.required
    ]);

    this.registering = new FormControl('', [
      Validators.required
    ]);

    this.product = new FormControl('', [
      Validators.required
    ]);

    this.industry = new FormControl('', [
      Validators.required
    ]);

    this.UserEmail = new FormControl('', [
      Validators.required , Validators.email
    ]);




    this.description = new FormControl('', [

    ]);


    this.userform = new FormGroup({

      Firstname: this.Firstname,
      Lastname: this.Lastname ,
      registering: this.registering ,
      product: this.product,
      industry: this.industry,
      description: this.description,
      UserEmail:this.UserEmail



    });
    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .GetAllSectors(userid)
    .then((response: any) => {

      console.log("Sector Response")
      this.rows = response.content;

      for(let count = 0; count < response.content.length; count++) {

if (response.content[count].description =="Cbn") {
  (<FormControl> this.userform.controls['industry']).setValue(response.content[count].id);
}


     }


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



this.busy =   this.registerapi
.GetSettingsById("34",userid)
.then((response: any) => {

var Settings = response.content;
console.log("Settins Value")
  console.log(response.content)
this.settingcode =Settings.itemValue
if (Settings.itemValue =="0"    ) {
  // alert("off")
  this.settingoff =true;
 }

 else {
   this.settingoff =false;

 }

})
         .catch((response: any) => {

           console.log(response)


          Swal.fire(
            response.error.message,
            '',
            'error'
          )

})

this.busy =   this.registerapi
.GetAllFeeLists(userid)
.then((response: any) => {

  console.log("fee  Response")


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


this.busy =   this.registerapi
.GetFeeListByName("PRELIMINARY SEARCH REPORT" ,userid)
.then((response: any) => {

  console.log("fee  Response")
  this.row5 = response.content;
  localStorage.setItem('description',this.row5.description);
  this.tot = parseInt(this.row5.init_amt ) +  parseInt(this.row5.technologyFee )


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

this.busy =   this.registerapi
.GetEmail(useremail)
.then((response: any) => {



  (<FormControl> this.userform.controls['Firstname']).setValue(response.firstName);
  (<FormControl> this.userform.controls['Lastname']).setValue(response.lastName);
  (<FormControl> this.userform.controls['UserEmail']).setValue(response.email);








})
         .catch((response: any) => {

           console.log(response)


          Swal.fire(
            response.error.message,
            '',
            'error'
          )

})






this.dropdownSettings = {
  singleSelection: false,
  idField: 'itemName',
  textField: 'itemName',
  selectAllText: 'Select All',
  maxHeight:80,
  itemsShowLimit: 80,
  allowSearchFilter: true
};




}



}
