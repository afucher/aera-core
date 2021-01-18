import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PoPageDynamicTableActions, PoPageDynamicTableCustomTableAction } from '@po-ui/ng-templates';

@Component({
  selector: 'app-pagamento-list',
  templateUrl: './pagamento-list.component.html',
  styleUrls: ['./pagamento-list.component.css']
})
export class PagamentoListComponent implements OnInit {

  readonly actions: PoPageDynamicTableActions = {};
  constructor(private router: Router) { }
  tableCustomActions: Array<PoPageDynamicTableCustomTableAction> = [
    { label: 'Alterar', action: ({id}) => this.router.navigate([`/pagamentos/${id}`]) }
  ];

  public readonly fields: Array<any> = [
    { property: 'value', label: 'Valor', type: 'currency', format: 'BRL'},
    { property: 'installment', label: 'Parcela' },
    { property: 'numberInstallments', label: 'Total de parcelas' },
  ];

  ngOnInit(): void {
  }

}
