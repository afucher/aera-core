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
    { label: 'Alterar', action: ({id}) => this.router.navigate([`/clientes/${id}`]) }
  ];

  public readonly fields: Array<any> = [
    { property: 'id', key: true },
    { property: 'nome', label: 'Nome', filter: true, gridColumns: 6 }
  ];

  ngOnInit(): void {
  }

}
