import { PagamentoService } from 'src/app/pagamento.service';
import { Pagamento } from './../../models/pagamento';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { PoModalComponent, PoTableAction } from '@po-ui/ng-components';
import { PoPageDynamicTableActions, PoPageDynamicTableComponent, PoPageDynamicTableCustomTableAction, PoPageDynamicTableFilters } from '@po-ui/ng-templates';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-pagamento-list',
  templateUrl: './pagamento-list.component.html',
  styleUrls: ['./pagamento-list.component.css']
})
export class PagamentoListComponent implements OnInit {

  readonly actions: PoPageDynamicTableActions = {};
  constructor(private router: Router, private _pagamentos: PagamentoService) { }
  tableCustomActions: Array<PoPageDynamicTableCustomTableAction> = [
    { label: 'Alterar', action: ({idMatricula}) => this.abreModal(idMatricula)}
  ];
  url: string;

  @Input() somentePendentes = false;

  public readonly fields: Array<PoPageDynamicTableFilters> = [
    { property: 'nomeAluno', label: 'Aluno'},
    { property: 'valor', label: 'Valor', type: 'currency', format: 'BRL'},
    { property: 'parcela', label: 'Parcela' },
    { property: 'totalDeParcelas', label: 'Total de parcelas' },
    { property: 'dataDeVencimento', label: 'Vencimento', type: 'date' },
    { property: 'pago', label: 'Pago', type: 'boolean', booleanFalse: 'Em aberto', booleanTrue: 'Pago' },
  ];

  pagamentoColumns: Array<any> = [
    {property: 'dataDeVencimento', label: 'Vencimento', type: 'date', readonly: true},
    {property: 'valor', label: 'Valor', type: 'currency', format: 'BRL', readonly: true},
    {property: 'parcela', label: 'Parcela', readonly: true},
    {property: 'totalDeParcelas', label: 'Parcelas', readonly: true},
    {property: 'pago', label: 'Pago?', type: 'boolean', boolean: {trueLabel: 'Pago', falseLabel: 'Em aberto'}, readonly: true},
  ];
  pagamentoActions: Array<PoTableAction> = [
    { action: () => {}, label: 'Atualizar', disabled: pagamento => pagamento.pago }
  ];

  pagamentos: Pagamento[] = [];
  numeroDeParcelas: number;
  valor: number;
  dataInicial: string;

  @ViewChild('modalMatricula', { static: true }) poModal: PoModalComponent;
  @ViewChild(PoPageDynamicTableComponent, { static: true }) table: PoPageDynamicTableComponent;


  ngOnInit(): void {
    if(this.somentePendentes) {
      this.url = '/api/pagamentos/pendentes';
    } else {
      this.url = '/api/pagamentos';
    }
  }

  gerarPagamentos() {
    this.pagamentos = Array(this.numeroDeParcelas).fill(null).map((_, index) => {
      return {
        dataDeVencimento: DateTime.fromFormat(this.dataInicial, 'yyyy-MM-dd').plus({month: 1 * index}).toFormat('yyyy-MM-dd'),
        idMatricula: 1,
        pago: false,
        parcela: index + 1,
        totalDeParcelas: this.numeroDeParcelas,
        valor: this.valor
      };
    });
  }

  abreModal(matriculaId: number) {
    this._pagamentos.obterPorMatricula(matriculaId).subscribe(
      p => {
        this.pagamentos = p;
        this.numeroDeParcelas = this.pagamentos[0].totalDeParcelas;
        this.valor = this.pagamentos[0].valor;
        this.dataInicial = this.pagamentos[0].dataDeVencimento;
        this.poModal.open();
      }
    );
  }

}
