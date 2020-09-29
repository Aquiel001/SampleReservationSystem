import {Component, Input, OnInit} from '@angular/core';
import {HeaderInfo} from '../../models/HeaderInfo';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {
  isExpanded: boolean;
  @Input() header: HeaderInfo;
  // @Input() headerLink: string;


  constructor() {
  }

  ngOnInit(): void {
    console.log(this.header)
    this.isExpanded = false;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
