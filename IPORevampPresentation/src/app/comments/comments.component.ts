import { Component, OnInit, Input  } from '@angular/core';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {
  @Input()
public   vrow2: any[] = [];
  constructor() { }

  ngOnInit() {

    console.log("User Comments")
    console.log(this.vrow2)

  }

}
