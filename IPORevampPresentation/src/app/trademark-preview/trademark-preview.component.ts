import { Component, OnInit , Input } from '@angular/core';
import {ApiClientService} from '../api-client.service';

@Component({
  selector: 'app-trademark-preview',
  templateUrl: './trademark-preview.component.html',
  styleUrls: ['./trademark-preview.component.css']
})
export class TrademarkPreviewComponent implements OnInit {
  @Input()
  public   row2: any;
  @Input()
  public   row4: any[] = [];
  @Input()
  public vshow :boolean = false;
  filepath:any ;
  constructor(private registerapi :ApiClientService) { }

  ngOnInit() {

    this.filepath = this.registerapi.GetFilepath2();
    console.log("input received")
    console.log(this.row4)
  }

}
