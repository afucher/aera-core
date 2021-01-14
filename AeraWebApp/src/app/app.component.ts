import { Component } from '@angular/core';

import { PoMenuItem } from '@po-ui/ng-components';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  readonly menus: Array<PoMenuItem> = [
    { label: 'AERA', link: '/' },
    {label: 'Clientes', link: '/clientes'},
    {label: 'Turmas', link: '/turmas'}
  ];
}
