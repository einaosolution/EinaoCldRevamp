import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-assign-role',
  templateUrl: './assign-role.component.html',
  styleUrls: ['./assign-role.component.css']
})
export class AssignRoleComponent implements OnInit {
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  Code: FormControl;
  id:string;
  CurrentRole:string  ;
  Description: FormControl;
  public rows = [];
  public row2 = [];
  public row3 = [];
  public row4 = [];
  isFirstOpen = true;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }

  onSubmit() {
    for (var i=0; i<this.row3.length; i++){
if (this.row3[i].selected ==true)  {

  this.row4.push(this.row3[i].id)
}

    }
    var userid =parseInt( localStorage.getItem('UserId'));
    var kk = {
      AssignedRoles: this.row4 ,
      CurrentRole:this.CurrentRole ,

      UserId:userid


    }

    this.registerapi
    .UpdateRolePermission(kk)
    .then((response: any) => {
      this.spinner.hide();

      this.submitted=false;
      Swal.fire(
        'Role  Saved Succesfully ',
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

})
    console.log("row 4")
    console.log(this.row4)

}



onSubmit4() {
  this.submitted= true;

  var userid =parseInt( localStorage.getItem('UserId'));

  if (this.userform.valid) {

    this.spinner.show();

    var kk = {
      CountryCode:this.userform.value.Code ,
      CountryName:this.userform.value.Description ,
      CountryId:this.id ,
      CreatedBy:userid



    }



   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdateCountry(kk)
      .then((response: any) => {
        this.spinner.hide();

        this.submitted=false;
        Swal.fire(
          'Record Updated Succesfully ',
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

  })
}

}

  onSubmit2() {
    this.savemode = true;
    this.updatemode = false;
    this.userform.reset();

  }

  onSubmit5(emp) {
    var userid =localStorage.getItem('UserId');


    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false,
    })

    swalWithBootstrapButtons.fire({
      title: 'Are you sure?',
      text: "",
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.value) {

        this.busy =   this.registerapi
        .DeleteCountry(emp.id,userid)
        .then((response: any) => {
          this.spinner.hide();
          console.log("Response")
          this.rows = response.content;
          console.log(response)

          Swal.fire(
            'Record Deleted  Succesfully ',
            '',
            'success'
          )



        })
                 .catch((response: any) => {
                  this.spinner.hide();
                   console.log(response)


                  Swal.fire(
                    response.error.message,
                    '',
                    'error'
                  )

    })
      } else if (
        // Read more about handling dismissals
        result.dismiss === Swal.DismissReason.cancel
      ) {

      }
    })
  }

  onSubmit3(kk) {
    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.code);

    (<FormControl> this.userform.controls['Description']).setValue(kk.name);
  }

  contactSelectionChange() {
    console.log("response 3")
    console.log(this.row3)
  }
  ngOnInit() {

    this.Code = new FormControl('', [
      Validators.required
    ]);

    this.Description = new FormControl('', [
      Validators.required
    ]);



    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,


    });

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Security")

    this.registerapi.VChangeEvent("Security");

    var userid = localStorage.getItem('UserId');



    this.busy =   this.registerapi
     .GetAllRoles(userid)
     .then((response: any) => {
       this.spinner.hide();
       console.log("Response")
       this.rows = response.content;
       console.log(response)



     })
              .catch((response: any) => {
               this.spinner.hide();
                console.log(response)


               Swal.fire(
                 response.error.message,
                 '',
                 'error'
               )

 })





this.busy =   this.registerapi
.GetMenuOthers("0",userid)
.then((response: any) => {



  this.busy =   this.registerapi
  . GetMenuParentId("0",userid)
  .then((response: any) => {


    console.log("Response")
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

  this.row3 = response.content;
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



  }

}
