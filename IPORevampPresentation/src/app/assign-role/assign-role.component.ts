import { Component, OnInit,NgZone ,OnDestroy} from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {Menu} from '../../../models/Menu';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { Subject } from 'rxjs';
import { trigger, style, animate, transition } from '@angular/animations';
import { map } from 'rxjs/operators';
declare var $ :any;

@Component({
  selector: 'app-assign-role',
  templateUrl: './assign-role.component.html',
  styleUrls: ['./assign-role.component.css']
})
export class AssignRoleComponent implements OnInit, OnDestroy {
  dtOptions:any = {};

  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  selectmenu :Menu[];
  busy: Promise<any>;
  Code: FormControl;
  id:string;
  CurrentRole:string  ;
  Description: FormControl;
  public rows = [];
  public row2 = [];
  public row3 = [];
  public row4 = [];
  public row5 = [];
  public row6 = [];
  isFirstOpen = true;
  vrole="";
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }

  loopArray(data,varray) {
var vcount = 0;
    for (var i=0; i< varray.length; i++){

     if (varray[i] === data) {
      vcount= vcount + 1;

     }



    }

    if (vcount > 0) {
      return true
    }

    else {
      return false;
    }

  }

  FieldsChange(values:any){
    alert(values.currentTarget.checked)

    }
  onChange(newValue) {

    this.vrole = newValue;

    var userid = localStorage.getItem('UserId');

    for (var i=0; i<this.row3 .length; i++){

      this.row3[i].selected = false;
    }



    this.busy =   this.registerapi
    .GetRoleById(this.vrole,userid)
    .then((response: any) => {


var varray = response.content
this.row6=[]
for (var i=0; i< varray.length; i++){

  this.row6.push(varray[i].name)



}

console.log("row6")
console.log( this.row6)


//this.row6 = response.content;

for (var i=0; i<this.row3 .length; i++){
 // this.row3[i].selected =true

 var  objIndex =this.row6.findIndex((obj =>


  obj== this.row3[i].name

   ));

   if (objIndex >=0 ) {


   this.row3[i].selected ="true"



   }

   else {
   // this.row3[i].selected = false
 //  this.selectmenu[i].selected ="false"
   }





   // this.row5.push(this.row3[i].parentId)


      }





      console.log("row3")
      console.log(this.row3)




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




    // ... do other stuff here ...
}

showcountry2() {
  this.submitted = false;
  $("#createmodel").modal('show');
}
  onSubmit() {
this.submitted = true;
    if (this.vrole =="" ) {

      Swal.fire(
       "Select Role",
        '',
        'error'
      )
      this.submitted = false;
      return;
    }
    for (var i=0; i<this.row3.length; i++){
if (this.row3[i].selected)  {

  this.row4.push(this.row3[i].id)
  this.row4.indexOf(this.row3[i].parentId) === -1  ? this.row4.push(this.row3[i].parentId) : console.log("This item already exists");
 // this.row5.push(this.row3[i].parentId)
}

    }

    if (this.row4.length ==0) {
      const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
          confirmButton: 'btn btn-success',
          cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false,
      })
      swalWithBootstrapButtons.fire({
        title: 'No Menu Was Selected ?',
        text: "",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, Continue!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
      }).then((result) => {
        if (result.value) {
          var userid =parseInt( localStorage.getItem('UserId'));
          var kk = {
            AssignedRoles: this.row4 ,
            CurrentRole:this.vrole ,

            UserId:userid


          }

          this.spinner.show();

         // $("#createmodel").scrollTop(0);

          $('#createmodel').show().scrollTop(0);

          this.busy =   this.registerapi
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

         this.row4 =[]

     //    $(document).scrollTop(0);


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
          this.submitted = false;
        }
      })
    }

    else {
    var userid =parseInt( localStorage.getItem('UserId'));
    var kk = {
      AssignedRoles: this.row4 ,
      CurrentRole:this.vrole ,

      UserId:userid


    }

    this.busy =   this.registerapi
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

   this.row4 =[]

    })
             .catch((response: any) => {
              this.spinner.hide();
               console.log(response)
               this.submitted=false;

              Swal.fire(
                response.error.message,
                '',
                'error'
              )

})

    }
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
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/AssignRole"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }



    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      dom: 'Bfrtip',
      // Configure the buttons
      buttons: [

        'colvis',
        'copy',
        'print',
        'csv',
        'excel',
        'pdf'

      ]

    };

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
.GetMenuOthers(userid)
.then((response: any) => {



  this.busy =   this.registerapi
  . GetMenuParentId("0",userid)
  .then((response: any) => {


    console.log("Response")
var kk = response.content
    kk.forEach(user=> user.selected = false)
    this.row3 = kk ;
    this.selectmenu   = kk;
    console.log("response2")
    console.log(response)

    this.dtTrigger.next();

  })
           .catch((response: any) => {

             console.log(response)


            Swal.fire(
              response.error.message,
              '',
              'error'
            )

 })

  this.row2 = response.content;
  console.log("All Assigned Role")
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
