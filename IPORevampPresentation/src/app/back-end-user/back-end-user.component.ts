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
  Role: FormControl;
  Lastname: FormControl;
  Lastname2: FormControl;
  MobileNumber: FormControl;
  Gender: FormControl;
  Gender2: FormControl;

  Email: FormControl;
  Email2: FormControl;
  Unit: FormControl;
  Unit2: FormControl;
  Street: FormControl;
  Street2: FormControl;
  City: FormControl;
  City2: FormControl;
  State: FormControl;
  State2: FormControl;
  Postal: FormControl;
  Postal2: FormControl;
  Country: FormControl;
  Country2: FormControl;
  Occupation2: FormControl;
  MobileNumber2: FormControl;
  id:string;
  Description: FormControl;
  Ministry: FormControl;
  Ministry2: FormControl;
  StaffId: FormControl;
  StaffId2: FormControl;
  Department: FormControl;
  Department2: FormControl;
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
      formData.append("StaffId",this.userform2.value.StaffId2);
      formData.append("Gender",this.userform2.value.Gender2);
      formData.append("Street",this.userform2.value.Street2);
      formData.append("City",this.userform2.value.City2);
      formData.append("Postal",this.userform2.value.Postal2);
      formData.append("Country",this.userform2.value.Country2);
      formData.append("State",this.userform2.value.State2);
      formData.append("Ministry",this.userform2.value.Ministry2);
      formData.append("Department",this.userform2.value.Department2);
      formData.append("Unit",this.userform2.value.Unit2);



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

  else {



   // alert("Form Not Valid")
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

  (<FormControl> this.userform2.controls['Role2']).setValue(kk.rolesId);
  (<FormControl> this.userform2.controls['Occupation2']).setValue(kk.occupation);
  (<FormControl> this.userform2.controls['MobileNumber2']).setValue(kk.mobileNumber);
  (<FormControl> this.userform2.controls['StaffId2']).setValue(kk.staffid);
  (<FormControl> this.userform2.controls['Gender2']).setValue(kk.gender);
  (<FormControl> this.userform2.controls['Street2']).setValue(kk.street);
  (<FormControl> this.userform2.controls['City2']).setValue(kk.city);
  (<FormControl> this.userform2.controls['Postal2']).setValue(kk.postalCode);
  (<FormControl> this.userform2.controls['Country2']).setValue(kk.countryCode);
  (<FormControl> this.userform2.controls['State2']).setValue(kk.state);
  (<FormControl> this.userform2.controls['Ministry2']).setValue(kk.ministry);
  (<FormControl> this.userform2.controls['Department2']).setValue(kk.department);
  (<FormControl> this.userform2.controls['Unit2']).setValue(kk.unit);
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
    this.StaffId2 = new FormControl('', [

    ]);

    this.Street2 = new FormControl('', [
      Validators.required
    ]);

    this.Gender2 = new FormControl('', [
      Validators.required
    ]);
    this.City2= new FormControl('', [
      Validators.required
    ]);

    this.Postal2= new FormControl('', [
      Validators.required
    ]);

    this.Country2= new FormControl('', [
      Validators.required
    ]);

    this.State2= new FormControl('', [
      Validators.required
    ]);

    this.Ministry2= new FormControl('', [

    ]);

    this.Department2= new FormControl('', [

    ]);

    this.Unit2= new FormControl('', [

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


      Role2:this.Role2 ,
      Occupation2: this.Occupation2,
      MobileNumber2: this.MobileNumber2,
      StaffId2: this.StaffId2,
      Street2: this.Street2,
      Gender2: this.Gender2,
      City2: this.City2,
      Postal2: this.Postal2,
      Country2: this.Country2,
      State2: this.State2,
      Ministry2: this.Ministry2,
      Department2: this.Department2,
      Unit2: this.Unit2,


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
//
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
