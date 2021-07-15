import { Router, ActivatedRoute } from '@angular/router';
import { AutenticacaoService } from './autenticacao/autenticacao.service';
import { Component } from '@angular/core';

import { PoMenuItem } from '@po-ui/ng-components';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private autenticacao: AutenticacaoService, private router: Router, private route: ActivatedRoute) {}

  readonly menus: Array<PoMenuItem> = [
    {label: 'AERA', link: '/' },
    {label: 'Clientes', link: '/clientes',
      subItems: [{label: 'Todos', link: '/clientes'},
                 {label: 'Novo', link: '/clientes/novo'}]},
    {label: 'Cursos', link: '/cursos',
      subItems: [{label: 'Todos', link: '/cursos'},
                 {label: 'Novo', link: '/cursos/novo'}]},
    {label: 'Turmas', link: '/turmas',
      subItems: [{label: 'Todas', link: '/turmas'},
                 {label: 'Nova', link: '/turmas/nova'}]},
    {label: 'Pagamentos', link: '/pagamentos'},
    {label: 'Logout', action: () => {
      this.autenticacao.logout();
      this.router.navigate(['/login']);
    }}
  ];

  mostraMenu() {
    return this.autenticacao.estaLogado() && !this.router.url.includes('/impressao');
  }
}
