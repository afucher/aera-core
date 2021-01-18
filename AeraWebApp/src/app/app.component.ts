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
    {label: 'Cursos', link: '/cursos'},
    {label: 'Turmas', link: '/turmas',
      subItems: [{label: 'Todas', link: '/turmas'},
                 {label: 'Nova', link: '/turmas/nova'}]},
    {label: 'Pagamentos', link: '/pagamentos'},
  ];
}
