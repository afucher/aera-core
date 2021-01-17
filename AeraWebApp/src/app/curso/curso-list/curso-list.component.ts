import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PoPageDynamicTableActions, PoPageDynamicTableCustomTableAction } from '@po-ui/ng-templates';

@Component({
  selector: 'app-curso-list',
  templateUrl: './curso-list.component.html',
  styleUrls: ['./curso-list.component.css']
})
export class CursoListComponent implements OnInit {

  readonly actions: PoPageDynamicTableActions = {};
  constructor(private router: Router) { }
  tableCustomActions: Array<PoPageDynamicTableCustomTableAction> = [
    { label: 'Alterar', action: ({id}) => this.router.navigate([`/cursos/${id}`]) }
  ];

  public readonly fields: Array<any> = [
    { property: 'id', key: true },
    { property: 'nome', label: 'Nome', filter: true}
  ];

  ngOnInit(): void {
  }

}
