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

@Component({
  selector: 'app-certificate',
  templateUrl: './certificate.component.html',
  styleUrls: ['./certificate.component.css']
})
export class CertificateComponent implements OnInit {
  row2:any
  todaydate:any
  vshow=false
  elementType = 'url';
  value = 'Federal Ministry Of Trade Nigeria';
  constructor(private registerapi :ApiClientService) { }


  print(): void {
    let printContents, popupWin;
    printContents = document.getElementById('print-section').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title>Print tab</title>
          <style>
          //........Customized style.......
          </style>
        </head>
    <body onload="window.print();window.close()">${printContents}</body>
      </html>`
    );
    popupWin.document.close();
}

  onSubmit3 () {
    window.print();
  }
  ngOnInit() {
    var userid  = localStorage.getItem('UserId');
    var pwalletid  = localStorage.getItem('Pwallet');
     this.todaydate= new Date();

    this.registerapi
    .GetCertificateApplicationById(userid,pwalletid)
    .then((response: any) => {
      console.log("response")

      this.row2 =response.content;

      this.vshow = true;

      console.log(response)



    })
             .catch((response: any) => {


 })
  }

}
