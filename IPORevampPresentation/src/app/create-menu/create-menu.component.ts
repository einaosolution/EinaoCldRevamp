import { Component, OnInit , TemplateRef,ViewChild ,OnDestroy } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
declare var $;

@Component({
  selector: 'app-create-menu',
  templateUrl: './create-menu.component.html',
  styleUrls: ['./create-menu.component.css']
})
export class CreateMenuComponent implements OnInit ,OnDestroy {
  modalRef: BsModalRef;
  dtOptions:any = {};

  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  Code: FormControl;
  id:string;
  Description: FormControl;
  icon : FormControl;
  url : FormControl;
  parent : FormControl;
  parent2 : FormControl;
  public rows = [];

  public  row2 = [];
  vshow:boolean=false;
  varray4 = [{ YearName: 'Leaf', YearCode: 'Leaf' }, {  YearName: 'Parent', YearCode: 'Parent' }   ]

  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }
  close(emp,template) {
    console.log(template)
    template.hide()
  // this.modalService.hide(template);
  // this.modalone.hide()
  }

  onSelectionChange(emp) {
  //  alert(emp.id)

  (<FormControl> this.userform.controls['parent']).setValue(emp.id);
  }

  openModal(template: TemplateRef<any>) {
    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    . GetMenuParentId("0",userid)
    .then((response: any) => {

      this.modalRef = this.modalService.show(template );
      console.log("Response")
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

  }

  onChange($event) {
    if (this.userform.value.parent2 =="Parent" ) {
this.vshow = false;
this.userform.value.parent="0";

    }

    else {
      this.vshow = true;
    }

  }
  onSubmit() {
    this.submitted= true;
    var userid =parseInt( localStorage.getItem('UserId'));
    var table = $('#myTable').DataTable();


    if (this.userform.valid) {

      if (this.userform.value.parent2 =="" ) {

        Swal.fire(
         "Select Menu Type",
          '',
          'error'
        )

        return;
      }

      this.spinner.show();

      var kk = {
        Name:this.userform.value.Code ,
        Icon:this.userform.value.icon ,
        Url:this.userform.value.url ,
        ParentId:this.userform.value.parent ,
        CreatedBy:userid



      }



     // this.router.navigate(['/Emailverification']);

     this.spinner.show()
      this.registerapi
        .SaveMenu(kk)
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
       this.vshow = false;

       $("#createmodel").modal('hide');

       table.destroy();
       this.getmenu();



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



onSubmit4() {
  this.submitted= true;

  var userid =parseInt( localStorage.getItem('UserId'));
  var table = $('#myTable').DataTable();

  if (this.userform.valid) {

    if (this.userform.value.parent2 =="" ) {

      Swal.fire(
       "Select Menu Type",
        '',
        'error'
      )

      return;
    }

    this.spinner.show();

    var kk = {
      Name:this.userform.value.Code ,
      Icon:this.userform.value.icon ,
      Url:this.userform.value.url ,
      ParentId:this.userform.value.parent ,
      CreatedBy:userid ,
      MenuId:this.id



    }





   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdateMenu(kk)
      .then((response: any) => {
        this.spinner.hide();

        this.submitted=false;
        Swal.fire(
          'Record Updated Succesfully ',
          '',
          'success'
        )
     //  this.router.navigate(['/Emailverification']);

     $("#createmodel").modal('hide');

     table.destroy();
     this.getmenu();

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

  onSubmit3(kk) {
    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.code);

    (<FormControl> this.userform.controls['Description']).setValue(kk.name);
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }


  showcountry(kk) {

    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id;

    (<FormControl> this.userform.controls['Code']).setValue(kk.name);

    (<FormControl> this.userform.controls['icon']).setValue(kk.icon);
    (<FormControl> this.userform.controls['url']).setValue(kk.url);
    (<FormControl> this.userform.controls['parent']).setValue(kk.parentId);
    if(kk.parentId =="0") {
      (<FormControl> this.userform.controls['parent2']).setValue("Parent");
    }

    else {
      (<FormControl> this.userform.controls['parent2']).setValue("Leaf");
    }
    $("#createmodel").modal('show');
    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );
  }


  showcountry2() {

    this.savemode = true;
    this.updatemode = false;
    this.vshow = false;


    (<FormControl> this.userform.controls['Code']).setValue("");

    (<FormControl> this.userform.controls['icon']).setValue("");
    (<FormControl> this.userform.controls['url']).setValue("");
    (<FormControl> this.userform.controls['parent']).setValue("");
    (<FormControl> this.userform.controls['parent2']).setValue("");
    $("#createmodel").modal('show');
    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );
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
        .DeleteMenu(emp.id,userid)
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
     this.getmenu();

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

  getmenu() {
    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
.GetMenuOthers(userid)
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
    var userid = localStorage.getItem('UserId');
    this.Code = new FormControl('', [
      Validators.required
    ]);

    this.Description = new FormControl('', [

    ]);

    this.icon  = new FormControl('', [

    ]);

    this.url  = new FormControl('', [

    ]);


    this.parent  = new FormControl('', [

    ]);

    this.parent2  = new FormControl('', [

    ]);




    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,
      icon: this.icon ,
      url: this.url ,
      parent: this.parent ,
      parent2: this.parent2,


    });

    (<FormControl> this.userform.controls['parent']).setValue("0");

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Security")

    this.registerapi.VChangeEvent("Security");



    this.busy =   this.registerapi
.GetMenuOthers(userid)
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
