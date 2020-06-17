import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-template',
  templateUrl: './template.component.html',
  styleUrls: ['./template.component.scss']
})
export class TemplateComponent implements OnInit {
    @Input() item: any;
    @Input() chkpre: any;
  constructor() { }

  ngOnInit(): void {
  }

}
