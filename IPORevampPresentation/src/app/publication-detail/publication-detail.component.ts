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




declare var $;

import * as jspdf from 'jspdf';

import html2canvas from 'html2canvas';
import 'jspdf-autotable';







@Component({
  selector: 'app-publication-detail',
  templateUrl: './publication-detail.component.html',
  styleUrls: ['./publication-detail.component.css']
})
export class PublicationDetailComponent implements OnInit {

  @ViewChild('dataTable') table;
  @ViewChild("fileInput") fileInput;
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  model: any = {};
  Code: FormControl;
  userid=""
  id:string;
  pwalletid:string ;
  appdescription:string ;
 public  HTML_Width
 public HTML_Height
 public PDF_Width
 public  PDF_Height
 public top_left_margin

 public margins = {
  top: 70,
  bottom: 40,
  left: 30,
  width: 550
};

  Description: FormControl;
  public rows = [];
  public row2
  public row3 = [];
  public row4
  public appcomment =""
  appcomment3="";
  appcomment2="" ;
  appcomment4 ="" ;

  public row10 = [];
  vbatch=1;

  vshow :boolean = false;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }


  generatepdf3() {
    var doc4 = new jspdf();
    alert("inside")

    doc4.fromHTML(document.getElementById('html-2-pdfwrapper'),
    this.margins.left, // x coord
    this.margins.top,
    {
      // y coord
      width: this.margins.width// max width of content on PDF
    },
   this. margins);

   doc4.save ('output2.pdf')


   var iframe = document.createElement('iframe');
  iframe.setAttribute('style','position:absolute;right:0; top:0; bottom:0; height:100%; width:650px; padding:20px;');
  document.body.appendChild(iframe);

  iframe.src = doc4.output('datauristring');


    //const doc = new jspdf();
  //  doc.autoTable({html: '#my-table'});
   // doc4.save('table.pdf');

  }
  generatepdf2() {
    var data = document.getElementById('html-2-pdfwrapper');
var date = new Date();
html2canvas(data).then(canvas => {
var imgWidth = 210;
var pageHeight = 1000;
var imgHeight = canvas.height * imgWidth / canvas.width;
var heightLeft = imgHeight;

  //enter code here
  const imgData = canvas.toDataURL('image/png')

  var doc = new jspdf('p', 'mm');
  var position = 0;

  doc.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight+15);
  heightLeft -= pageHeight;

  while (heightLeft >= 0) {
    position = heightLeft - imgHeight;
    doc.addPage();
    doc.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight + 15);
    heightLeft -= pageHeight;
  }
doc.save ('Visiometria_'+this.id+ '_'+date.getTime()+'.pdf')


});

  }

  calculatePDF_height_width(selector,index){
   let  page_section = $(selector).eq(index);
    this.HTML_Width = page_section.width();
   this.HTML_Height = page_section.height();
   this.top_left_margin = 15;
  this.PDF_Width = this.HTML_Width + (this.top_left_margin * 2);
   this.PDF_Height = (this.PDF_Width * 1.2) + (this.top_left_margin * 2);
   // canvas_image_width = HTML_Width;
   // canvas_image_height = HTML_Height;
    }



  generatePdf() {
   var doc = new jspdf('p', 'mm', "a4");
  // var doc = new jspdf();
  // var doc = new jspdf('p', 'pt', [this.PDF_Width, this.PDF_Height]);
  var  self = this;
    html2canvas(document.getElementById('report')).then(function(canvas) {

      var img = canvas.toDataURL("image/png");
     // doc.addPage(self.PDF_Width, self.PDF_Height);

   doc.setFont("courier");




   var width = doc.internal.pageSize.getWidth();
var height = doc.internal.pageSize.getHeight();


   // var width = doc.internal.pageSize.width;
   // var height = doc.internal.pageSize.height;

  //doc.addImage(img, 'JPEG',5,20);
    //doc.addImage(img, 'JPEG', 0, 0);

    doc.addImage(img, 'JPEG', 0, 0, width, height);




    doc.save('Result.pdf');

  });


  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }


  onSubmit(){


    var doc = new jspdf('p', 'mm', "a4");






    html2canvas(document.getElementById('report')).then(function(canvas) {

      var img = canvas.toDataURL("image/png");

      alert("about to save")
   doc.setFont("courier");



    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    doc.addImage(img, 'JPEG', 0, 0, width, height);



    doc.save('Output.pdf');

    alert("finish  save")





  });



  }






  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/PublicationNew"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }





    this.registerapi.setPage("TrademarkPublication")

    this.registerapi.VChangeEvent("TrademarkPublication");




  const firstParam: string = this.route.snapshot.queryParamMap.get('bno');

  if (firstParam) {
    var userid = localStorage.getItem('UserId');



   this.busy =   this.registerapi
   .GetPublicationById(userid ,firstParam)
   .then((response: any) => {

     console.log("Batch Response")
     this.rows = response.content;


     console.log(response)


this.vshow =true;



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

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");







  }

}
