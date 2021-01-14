import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PoPageDynamicTableActions, PoPageDynamicTableCustomTableAction } from '@po-ui/ng-templates';

@Component({
  selector: 'app-turma-list',
  templateUrl: './turma-list.component.html',
  styleUrls: ['./turma-list.component.css']
})
export class TurmaListComponent implements OnInit {
  readonly actions: PoPageDynamicTableActions = {};
  constructor(private router: Router) { }
  tableCustomActions: Array<PoPageDynamicTableCustomTableAction> = [
    { label: 'Alterar', action: ({id}) => this.router.navigate([`/turmas/${id}`]) }
  ];

  public readonly fields: Array<any> = [
    { property: 'id', key: true },
    { property: 'curso', label: 'Nome', filter: true},
    { property: 'dataInicial', label: 'Início', filter: true, type: 'date'},
    { property: 'dataFinal', label: 'Término', filter: true, type: 'date'}
  ];

  ngOnInit(): void {
  }

}
