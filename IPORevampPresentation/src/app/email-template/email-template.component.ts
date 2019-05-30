import { Component, OnInit ,OnDestroy} from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { Subject } from 'rxjs';
import { AbstractControl } from '@angular/forms';

declare var $;

@Component({
  selector: 'app-email-template',
  templateUrl: './email-template.component.html',
  styleUrls: ['./email-template.component.css']
  ,
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
export class EmailTemplateComponent implements OnInit,OnDestroy  {
  dtOptions:any = {};
  dtTrigger: Subject<any> = new Subject();
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  Code: FormControl;
  id:string;
  Description: FormControl;
  Subject: FormControl;
  Sender: FormControl;
  Template: FormControl;
  public rows = [];
  public row2 = [];
  busy: Promise<any>;

  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }

  onSubmit() {
    this.submitted= true;
    var table = $('#myTable').DataTable();
    var userid =parseInt( localStorage.getItem('UserId'));

    if (this.ContainsAlphabet(this.userform.value.Subject) && this.ContainsAlphabet(this.userform.value.Code) ) {

    }

    else {

      Swal.fire(
       "Invalid Input",
        '',
        'error'
      )

      return

    }


    if (this.userform.valid) {

      this.spinner.show();

      var kk = {
        EmailName:this.userform.value.Code ,
        EmailSubject:this.userform.value.Subject ,

        EmailBody:this.userform.value.Template ,
        CreatedBy:userid,
        EmailCode:this.userform.value.Code



      }



     // this.router.navigate(['/Emailverification']);


      this.registerapi
        .SaveEmailTemplate(kk)
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
       $("#createmodel").modal('hide');
       table.destroy();
       this.getallEmailTemplate();


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

ContainsAlphabet(str) {

  if (str.match(/[a-z]/i)) {
    return true
  }

  else {
    return false
  }

}



onSubmit4() {
  this.submitted= true;

if (this.ContainsAlphabet(this.userform.value.Subject) && this.ContainsAlphabet(this.userform.value.Code) ) {

}

else {

  Swal.fire(
   "Invalid Input",
    '',
    'error'
  )

  return

}



  var userid =parseInt( localStorage.getItem('UserId'));
  var table = $('#myTable').DataTable();

  if (this.userform.valid) {

    this.spinner.show();

    var kk = {
      EmailName:this.userform.value.Code ,
      EmailSubject:this.userform.value.Subject,

      EmailBody:this.userform.value.Template ,
      EmailTemplateId:this.id ,
      CreatedBy:userid ,
      EmailCode:this.userform.value.Code



    }




   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdateEmailTemplate(kk)
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
     $("#createmodel").modal('hide');
     table.destroy();
     this.getallEmailTemplate();

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
    var table = $('#myTable').DataTable();

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
        this.registerapi
        .DeleteEmailTemplate(emp.emailName,userid,emp.id)
        .then((response: any) => {
          this.spinner.hide();
          console.log("Response")
          this.rows = response.content;
          console.log(response)
          $("#createmodel").modal('hide');
          Swal.fire(
            'Record Deleted  Succesfully ',
            '',
            'success'
          )

          table.destroy();
          this.getallEmailTemplate();

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

  showcountry2() {

    this.savemode = true;
    this.updatemode = false;


    (<FormControl> this.userform.controls['Code']).setValue("");

    (<FormControl> this.userform.controls['Subject']).setValue("");

    (<FormControl> this.userform.controls['Template']).setValue("");
    $("#createmodel").modal('show');
    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );
  }
  showcountry(kk) {

    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.emailName);

    (<FormControl> this.userform.controls['Subject']).setValue(kk.emailSubject);

    (<FormControl> this.userform.controls['Template']).setValue(kk.emailBody);
    $("#createmodel").modal('show');
    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );
  }
  onSubmit3(kk) {
    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.emailName);

    (<FormControl> this.userform.controls['Subject']).setValue(kk.emailSubject);
    (<FormControl> this.userform.controls['Sender']).setValue(kk.emailSender);
    (<FormControl> this.userform.controls['Template']).setValue(kk.emailBody);
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getallEmailTemplate() {
    var userid = localStorage.getItem('UserId');






    this.busy =   this.registerapi
    .GetAllEmailTemplates(userid)
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

  removeSpaces(control: AbstractControl) {
    if (control && control.value && !control.value.replace(/\s/g, '').length) {
      control.setValue('');
    }
    return null;
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

    ]);


    this.Subject = new FormControl('', [
      Validators.required
    ]);

    this.Sender = new FormControl('', [

    ]);

    this.Template = new FormControl('', [
      Validators.required
    ]);



    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,
      Subject: this.Subject ,
      Sender: this.Sender ,
      Template: this.Template ,


    });

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Setup")

    this.registerapi.VChangeEvent("Setup");

   var userid = localStorage.getItem('UserId');






this.busy =   this.registerapi
.GetAllEmailTemplates(userid)
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
