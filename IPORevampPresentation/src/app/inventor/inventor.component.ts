import { Component, OnInit,ViewChild,OnDestroy } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { Student } from '../Student';
import { Invention } from '../Invention';
import { Priority } from '../Priority';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl , FormArray } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { Subject } from 'rxjs';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import {formatDate} from '@angular/common';


import { map } from 'rxjs/operators';

import 'datatables.net'
import 'datatables.net-dt'




declare var $;

@Component({
  selector: 'app-inventor',
  templateUrl: './inventor.component.html',
  styleUrls: ['./inventor.component.css']
})
export class InventorComponent implements OnDestroy ,OnInit  {
  dtOptions:any = {};
  students: Student[] = [];
 invention:Invention[] = [];
 priority:Priority[] = [];
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = false;
  savemode2:boolean = false;
  savemode3:boolean = false;
  updatemode:boolean = false;
  userform: FormGroup;
  userform2: FormGroup;
  userform3: FormGroup;
  Inventor: FormArray;
  Priority2: FormArray;


  submitted:boolean=false;
  submitted2:boolean=false;
  submitted3:boolean=false;
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
  public pwalletid ="" ;
  vshow :boolean = true

  elementType = 'url';
  value = '';

  transactionid
  paymentreference
  transactionid2

  public rows=[];
  public row2 ;
  public tot ;
  public row3 = [];

  public row5 = [];
  public row6 = [];
  public row7 = [];
  public row8 = [];
  public row9 = [];
  public row10 = [];
  public row11 = [];
  public row100  = [];
  public row101:any[] = [];
  public row102 :any[] = [];
  public row500 :any[] = [];

  public row501 =[];

  public row502  =[];
  public vid ="";

  public rrr ="";

  public row12 :any;
  public fee_description =""
  public trademarktype =""
  public  imageSrc
  public categoryid
  public checkboxFlag =false

  public image1
  public image2
  public image3
  public image4
  public image5
  public image6
  public filepath
  public trademarklogo


  row:any[] =[]
  row22:any
  vshow2:boolean =false
  vshow3:boolean =false
  settingoff:boolean =false
  settingcode
  vfilepath:string =""

  constructor(private registerapi :ApiClientService ,private router: Router ,private route: ActivatedRoute ,private formBuilder: FormBuilder) { }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      dom: 'Bfrtip',
      // Configure the buttons
      buttons: [



      ]

    };



      let email =localStorage.getItem('username');




          const inventionObservable = this.registerapi.getInvention();
          inventionObservable.subscribe((inventionData: Invention[]) => {
              this.invention = inventionData;
              this.dtTrigger.next()
          });
  }

}
