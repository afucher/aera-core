import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  PoPageDynamicTableActions,
  PoPageDynamicTableCustomTableAction
} from '@po-ui/ng-templates';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
})
export class ClientListComponent implements OnInit {
  readonly actions: PoPageDynamicTableActions = {};
  constructor(private router: Router) { }
  tableCustomActions: Array<PoPageDynamicTableCustomTableAction> = [
    { label: 'Detalhes', action: ({id}) => this.router.navigate([`/clientes/${id}/detalhes`]) },
    { label: 'Alterar', action: ({id}) => this.router.navigate([`/clientes/${id}/alterar`]) }
  ];

  public readonly fields: Array<any> = [
    { property: 'id', key: true },
    { property: 'nome', label: 'Nome', filter: true},
    { property: 'cpf', label: 'CPF', filter: true},
    { property: 'email', label: 'Email', filter: true},
    { property: 'celular', label: 'Celular'}
  ];

  ngOnInit(): void {
  }

}
