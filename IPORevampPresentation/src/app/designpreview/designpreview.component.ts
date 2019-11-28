import { Component, OnInit , Input } from '@angular/core';

@Component({
  selector: 'app-designpreview',
  templateUrl: './designpreview.component.html',
  styleUrls: ['./designpreview.component.css']
})
export class DesignpreviewComponent implements OnInit {
  @Input()
  public   row2: any[] = [];
  @Input()
  public   row4: any;



  @Input()
  public   row3: any[] = [];

  @Input()
  public   row5: any[] = [];

  @Input()
  public   row6: any[] = [];

  @Input()
  public   coapplicant: any[] = [];


  constructor() { }

  ngOnInit() {
  }

}
