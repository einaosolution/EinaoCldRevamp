import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-sub-menu',
  templateUrl: './sub-menu.component.html',
  styleUrls: ['./sub-menu.component.css']
})
export class SubMenuComponent implements OnInit {
  @Input() parentid: string;
  @Input() vrow: any[];
  constructor() { }




  ngOnInit() {




    console.log("parent id =" + this.parentid )
    console.log("vrow =" )
    console.log(this.vrow )




  }

}
