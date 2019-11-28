import { Component, OnInit , Input  } from '@angular/core';

@Component({
  selector: 'app-patent-preview',
  templateUrl: './patent-preview.component.html',
  styleUrls: ['./patent-preview.component.css']
})
export class PatentPreviewComponent implements OnInit {
  @Input()
  public   row2: any[] = [];
  @Input()
  public   row4: any;
  @Input()
  public vshow :boolean = false;
  @Input()
  public  savemode :boolean = false;

  @Input()
  public   row3: any[] = [];

  @Input()
  public   row5: any[] = [];

  @Input()
  public   row6: any[] = [];

  constructor() { }

  ngOnInit() {
  //  this.savemode = true;
  }

}
