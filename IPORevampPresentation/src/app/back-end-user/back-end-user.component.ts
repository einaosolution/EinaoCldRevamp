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
  selector: 'app-back-end-user',
  templateUrl: './back-end-user.component.html',
  styleUrls: ['./back-end-user.component.css']
})
export class BackEndUserComponent implements OnDestroy ,OnInit {
  @ViewChild('dataTable') table;
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  userform2: FormGroup;
  submitted:boolean=false;
  submitted2:boolean=false;
  busy: Promise<any>;
  Code: FormControl;
  Firstname: FormControl;
  Firstname2: FormControl;
  Role2: FormControl;
  Lastname: FormControl;
  Lastname2: FormControl;
  MobileNumber: FormControl;
  Gender: FormControl;
  Email: FormControl;
  Email2: FormControl;
  Unit: FormControl;
  Street: FormControl;
  City: FormControl;
  State: FormControl;
  Postal: FormControl;
  Country: FormControl;
  Occupation2: FormControl;
  MobileNumber2: FormControl;
  id:string;
  Description: FormControl;
  Ministry: FormControl;
  StaffId: FormControl;
  Department: FormControl;
  vshow :boolean = false
  public rows = [];
  public row2 = [];
  public row3 = [];

  public row5 = [];
  public row6 = [];
  public row7 = [];
  public row8 = [];
  public row9 = [];
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }


  getrole(roleid) {
    var vrole = "";
    //this.row2

 if (roleid =="3") {
   return "Trademark"
 }

 else if  (roleid =="1") {
  return "Individual"
}

else if  (roleid =="2") {
  return "Corporate"
}
 else if  (roleid =="4") {
  return "Patent"
}

else if  (roleid =="5") {
  return "Design"
}

else if  (roleid =="6") {
  return "Registra"
}




    return vrole
  }


  getrole2(roleid) {
    var vrole = "";
    //this.row2

    for (var i=0; i<this.row5.length; i++){
      if (this.row5[i].roleId ==roleid)  {
        vrole =this.row5[i].title


      }

    }


    return vrole
  }
  onSubmit() {
    this.submitted= true;

    var table = $('#myTable').DataTable();
    var userid =parseInt( localStorage.getItem('UserId'));


    if (this.userform.valid) {

      this.spinner.show();

      var kk = {
        Firstname:this.userform.value.Firstname ,
        Lastname:this.userform.value.Lastname ,
        MobileNumber:this.userform.value.MobileNumber ,
        Gender:this.userform.value.Gender ,
        Email:this.userform.value.Email ,
        Unit:this.userform.value.Unit,
        Street:this.userform.value.Street,
        City:this.userform.value.City,
        State:this.userform.value.State,
        Postal:this.userform.value.Postal,
        Country:this.userform.value.Country,
        Username:this.userform.value.Email ,
        staffid:this.userform.value.StaffId ,
        ministry:this.userform.value.Ministry ,
        department:this.userform.value.Department ,


        CreatedBy:userid


      }



     // this.router.navigate(['/Emailverification']);

     this.spinner.show()
      this.registerapi
        .SaveUser(kk)
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

       this. getCountry()

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

  else {








    alert("All Input Must be Entered")
  }

}


public findInvalidControls() {
  const invalid = [];
  const controls = this.userform.controls;
  for (const name in controls) {
      if (controls[name].invalid) {
          invalid.push(name);
      }
  }
  return invalid;
}

valuechange(een ) {
  //  alert(this.userform.value.email);
   // this.userform.value.email ="aa@ya.com";
   let obj = this.row3.find(o => o.userName.toUpperCase() === this.userform.value.Email.toUpperCase());

   if (obj) {
    (<FormControl> this.userform.controls['Email']).setValue("");

    Swal.fire(
      "Email  Already Exist",
      '',
      'error'
    )

    return ;
   }


   let obj2 = this.row6.find(o => o.email.toUpperCase() === this.userform.value.Email.toUpperCase());

   if (obj2) {
    (<FormControl> this.userform.controls['Email']).setValue("");

    Swal.fire(
      "Email  Already Exist",
      '',
      'error'
    )

    return;
   }






  }


  onSubmit4() {
    var table = $('#myTable').DataTable();
    this.submitted2= true;
    var  formData = new FormData();


    var userid = localStorage.getItem('UserId');

    if (this.userform2.valid) {

      this.spinner.show();




      formData.append("UserId",this.id);
      formData.append("RoleId",this.userform2.value.Role2);
      formData.append("Firstname",this.userform2.value.Firstname2);
      formData.append("RequestedBy",userid);
      formData.append("Lastname",this.userform2.value.Lastname2);
      formData.append("PhoneNumber",this.userform2.value.MobileNumber2);
      formData.append("Occupation",this.userform2.value.Occupation2);



     // this.router.navigate(['/Emailverification']);


      this.registerapi
        .UpdateUserFunction(formData)
        .then((response: any) => {
          this.spinner.hide();

          this.submitted=false;
          $("#createmodel2").modal('hide');
          Swal.fire(
            'Record Updated Succesfully ',
            '',
            'success'
          )
       //  this.router.navigate(['/Emailverification']);
       table.destroy();
       this.getCountry();
       this.userform2.reset();


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
        .DeleteUser(emp.id,userid)
        .then((response: any) => {
          this.spinner.hide();
          console.log("Response")

          console.log(response)
          $("#createmodel").modal('hide');
          Swal.fire(
            'Record Deleted  Succesfully ',
            '',
            'success'
          )

          table.destroy();
     this. getCountry();

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
showcountry(kk) {

  this.savemode = false;
  this.updatemode = true;
  this.id = kk.id ;

  (<FormControl> this.userform2.controls['Firstname2']).setValue(kk.firstName);

  (<FormControl> this.userform2.controls['Lastname2']).setValue(kk.lastName);
  (<FormControl> this.userform2.controls['Email2']).setValue(kk.email);
  (<FormControl> this.userform2.controls['Role2']).setValue(kk.rolesId);
  (<FormControl> this.userform2.controls['Occupation2']).setValue(kk.occupation);
  (<FormControl> this.userform2.controls['MobileNumber2']).setValue(kk.mobileNumber);
  $("#createmodel2").modal('show');
  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );
}

showcountry2() {

  this.savemode = true;
  this.updatemode = false;


 // (<FormControl> this.userform.controls['Code']).setValue("");

 // (<FormControl> this.userform.controls['Description']).setValue("");
  $("#createmodel").modal('show');
  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );
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

  getCountry() {

   var userid = localStorage.getItem('UserId');
   this.busy =   this.registerapi
   .GetUser2()
   .then((response: any) => {
     this.spinner.hide();
     console.log("Response")
     this.row3 = response;
     console.log(response)
     this.dtTrigger.next();


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




  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/Staff"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }

    $("#cancel2").hide();
    $("#save2").hide();

var self = this;
    $(".tab-wizard").steps({
      headerTag: "h6",
      bodyTag: "section",
      transitionEffect: "fade",
      enableFinishButton: false,
      onStepChanging: function (event, currentIndex, newIndex)
    {


      if (currentIndex ==0) {
        $("#cancel2").show();
        $("#save2").show();

      }

      else  {
        $("#cancel2").hide();
        $("#save2").hide();
      //  this.vshow = false
      }

    //  $(".tab-wizard").steps("next",{})
    return true
       // return form.valid();
    },

      titleTemplate: '<span class="step">#index#</span> #title#',
      labels: {
          finish: "Submit"
      },

      onFinishing: function (event, currentIndex)
      {
        alert("index =" + currentIndex)
        $(".tab-wizard").validate().settings.ignore = ":disabled";
          return $(".tab-wizard").valid();
      },
      onFinished: function(event, currentIndex) {
        self.submitted= true;


       self.onSubmit()
        //  swal("Form Submitted!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.");

      }
  });
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


    this.Firstname = new FormControl('', [
      Validators.required
    ]);
    this.Firstname2 = new FormControl('', [
      Validators.required
    ]);

    this.Lastname = new FormControl('', [
      Validators.required
    ]);
    this.StaffId = new FormControl('', [
      Validators.required
    ]);

    this.Lastname2 = new FormControl('', [
      Validators.required
    ]);

    this.Role2 = new FormControl('', [
      Validators.required
    ]);



    this.MobileNumber = new FormControl('', [
      Validators.required
    ]);

    this.Gender = new FormControl('', [
      Validators.required
    ]);
    this.Email = new FormControl('', [
      Validators.required
    ]);

    this.Email2 = new FormControl('', [
      Validators.required
    ]);

    this.Unit = new FormControl('', [
      Validators.required
    ]);

    this.Ministry = new FormControl('', [
      Validators.required
    ]);

    this.Department = new FormControl('', [
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

    this.Postal= new FormControl('', [

    ]);

    this.Country= new FormControl('', [
      Validators.required
    ]);

    this.Occupation2= new FormControl('', [

    ]);

    this.MobileNumber2= new FormControl('', [

    ]);






    this.userform = new FormGroup({

      Firstname: this.Firstname,
      Lastname: this.Lastname ,
      StaffId: this.StaffId ,
      MobileNumber: this.MobileNumber ,
      Gender: this.Gender ,
      Email: this.Email ,
      Unit: this.Unit ,
      Ministry: this.Ministry ,
      Department: this.Department ,
      Street: this.Street ,
      City: this.City,
      State: this.State,
      Postal: this.Postal,
      Country: this.Country,




    });

    this.userform2 = new FormGroup({

      Firstname2: this.Firstname2,
      Lastname2: this.Lastname2 ,

      Email2: this.Email2 ,
      Role2:this.Role2 ,
      Occupation2: this.Occupation2,
      MobileNumber2: this.MobileNumber2,


    });

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Security")

    this.registerapi.VChangeEvent("Security");

   var userid = localStorage.getItem('UserId');



   this.busy =   this.registerapi
   .GetAllRoles(userid)
   .then((response: any) => {
     this.spinner.hide();
     console.log("roles Response")
     this.row5 = response.content;
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



 this.busy =   this.registerapi
 .GetUser2()
 .then((response: any) => {
   this.spinner.hide();
   console.log("user Response")
   this.row3 = response;
   console.log(response)
   this.dtTrigger.next();


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
.GetAllTempUser()
.then((response: any) => {
  this.spinner.hide();
  console.log("Response")
  this.row6 = response;
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
.GetAllUnit(userid)
.then((response: any) => {

  this.row7 = response.content;
  console.log("all unitresponse")
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
.GetAllMinistry(userid)
.then((response: any) => {

  this.row8 = response.content;
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
. GetAllDepartment(userid)
.then((response: any) => {
  this.spinner.hide();
  console.log("Response")
  this.row9 = response.content;
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
    .GetCountry("true",userid)
    .then((response: any) => {
      this.spinner.hide();

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
  }

}
