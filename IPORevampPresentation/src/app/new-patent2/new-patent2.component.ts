import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl, FormArray } from '@angular/forms';

@Component({
  selector: 'app-new-patent2',
  templateUrl: './new-patent2.component.html',
  styleUrls: ['./new-patent2.component.css']
})
export class NewPatent2Component implements OnInit {
  orderForm: FormGroup;
  items: FormArray;
  constructor(private formBuilder: FormBuilder) { }


  addItem(): void {
    this.items = this.orderForm.get('items') as FormArray;
    this.items.push(this.createItem());
  }


  createItem(): FormGroup {
    return this.formBuilder.group({
      name: '',
      description: '',
      price: ''
    });
  }

  ngOnInit() {

    this.orderForm = this.formBuilder.group({
      customerName: '',
      email: '',
      items: this.formBuilder.array([ this.createItem() ])
    });
  }

}
