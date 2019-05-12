import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-corporate',
  templateUrl: './corporate.component.html',
  styleUrls: ['./corporate.component.css']
})
export class CorporateComponent implements OnInit {

  varray4 = [{ YearName: 'Argentina', YearCode: 'AR' }, {  YearName: 'Austria', YearCode: 'AT' }  ,{  YearName: 'Cameroon', YearCode: 'CM' },{  YearName: 'China', YearCode: 'CN' } ,{  YearName: 'Nigeria', YearCode: 'NG' } ]
  constructor() { }

  ngOnInit() {
  }

}
