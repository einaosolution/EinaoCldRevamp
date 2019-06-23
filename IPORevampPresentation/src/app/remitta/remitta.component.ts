import { Component, OnInit } from '@angular/core';


declare var RmPaymentEngine:any;




@Component({
  selector: 'app-remitta',
  templateUrl: './remitta.component.html',
  styleUrls: ['./remitta.component.css']
})
export class RemittaComponent implements OnInit {
amount =10000;
  constructor() { }



  ngOnInit() {
  }

}
