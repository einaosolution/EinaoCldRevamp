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
  selector: 'app-lga',
  templateUrl: './lga.component.html',
  styleUrls: ['./lga.component.css'] ,
  animations: [
    trigger(
      'enterAnimation', [
        transition(':enter', [
          style({ transform: 'translateX(100%)', opacity: 0 }),
          animate('500ms', style({ transform: 'translateX(0)', opacity: 1 }))
        ]),
        transition(':leave', [
          style({ transform: 'translateX(0)', opacity: 1 }),
          animate('500ms', style({ transform: 'translateX(100%)', opacity: 0 }))
        ])
      ]
    )
  ]
})
export class LgaComponent implements OnInit {

  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  Code: FormControl;
  Description: FormControl;
  busy: Promise<any>;
  id:string;
  public rows = [];
  public row2 = [];
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }

  onSubmit() {
    this.submitted= true;



    if (this.userform.valid) {

      this.spinner.show();
      var userid =parseInt( localStorage.getItem('UserId'));
      var kk = {
        StateId:this.userform.value.Code ,
        LGAName:this.userform.value.Description ,
        CreatedBy:userid



      }



     // this.router.navigate(['/Emailverification']);


      this.registerapi
        .SaveLGA(kk)
        .then((response: any) => {
          this.spinner.hide();

          this.submitted=false;
          Swal.fire(
            'Record Saved Succesfully ',
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

onSubmit11() {

  this.submitted= true;

  var userid =parseInt( localStorage.getItem('UserId'));

  if (this.userform.valid) {

    this.spinner.show();

    var kk = {
      StateId:this.userform.value.Code ,
      LGAName:this.userform.value.Description ,
      LGAId:this.id ,
      CreatedBy:userid



    }



   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdateLGA(kk)
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
onSubmit3(emp) {
  this.savemode = false;
  this.updatemode = true;
  this.id = emp.id ;

  (<FormControl> this.userform.controls['Code']).setValue(emp.stateId);

  (<FormControl> this.userform.controls['Description']).setValue(emp.lgaName);


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
      .DeleteLGA(emp.id,userid)
      .then((response: any) => {
        this.spinner.hide();
        console.log("Response")
        this.rows = response.content;

        this.submitted=false;
        Swal.fire(
          'Record Deleted  Succesfully ',
          '',
          'success'
        )
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
    } else if (
      // Read more about handling dismissals
      result.dismiss === Swal.DismissReason.cancel
    ) {

    }
  })
}

  onSubmit2() {
    this.savemode = true;
    this.updatemode = false;
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


    this.registerapi.setPage("Country")

    this.registerapi.VChangeEvent("Country");
    var userid = localStorage.getItem('UserId');



  this.busy =   this.registerapi
  .GetAllStates(userid)
  .then((response: any) => {


    this.rows = response.content;
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
.GetAllLGAs(userid)
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



}

}
