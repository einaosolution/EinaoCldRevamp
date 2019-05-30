import { Component, OnInit ,OnDestroy } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import { Subject } from 'rxjs';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';

import { map } from 'rxjs/operators';


declare var $;
@Component({
  selector: 'app-state',
  templateUrl: './state.component.html',
  styleUrls: ['./state.component.css'] ,
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
export class StateComponent implements OnDestroy , OnInit {
  savemode:boolean = true;
  dtOptions:any = {};
  dtTrigger: Subject<any> = new Subject();
  updatemode:boolean = false;
  userform: FormGroup;
  busy: Promise<any>;
  submitted:boolean=false;
  Code: FormControl;
  Description: FormControl;
  public rows = [];
  public row2 = [];
  id:string;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }

  onSubmit() {
    this.submitted= true;



    if (this.userform.valid) {

      this.spinner.show();
      var userid =parseInt( localStorage.getItem('UserId'));
      var kk = {
        CountryID:this.userform.value.Code ,
        StateName:this.userform.value.Description ,
        CreatedBy:userid ,
        StateId:0



      }



     // this.router.navigate(['/Emailverification']);


      this.registerapi
        .SaveState(kk)
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

       this.getallstate()

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
  }

  onSubmit11() {

    this.submitted= true;

    var userid =parseInt( localStorage.getItem('UserId'));

    if (this.userform.valid) {

      this.spinner.show();

      var kk = {
        CountryID:this.userform.value.Code ,
        StateName:this.userform.value.Description ,
        StateId:this.id ,
        CreatedBy:userid



      }



     // this.router.navigate(['/Emailverification']);


      this.registerapi
        .UpdateState(kk)
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

       this.getallstate()

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

  showcountry(kk) {

    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.countryId);

    (<FormControl> this.userform.controls['Description']).setValue(kk.stateName);
    $("#createmodel").modal('show');
    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );
  }
  onSubmit3(emp) {
    this.savemode = false;
    this.updatemode = true;
    this.id = emp.id ;

    (<FormControl> this.userform.controls['Code']).setValue(emp.countryId);

    (<FormControl> this.userform.controls['Description']).setValue(emp.stateName);


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
        .DeleteState(emp.id,userid)
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

          this.getallstate()


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

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getallstate() {
    var userid = localStorage.getItem('UserId');

  this.busy =   this.registerapi
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
  }
  ngOnInit() {
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


    this.registerapi.setPage("Country")

    this.registerapi.VChangeEvent("Country");
    var userid = localStorage.getItem('UserId');

    this.busy =   this.registerapi
    .GetCountry("true",userid)
    .then((response: any) => {


      this.rows = response.content;




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
  .GetAllStates(userid)
  .then((response: any) => {


    this.row2 = response.content;
    this.dtTrigger.next();
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
