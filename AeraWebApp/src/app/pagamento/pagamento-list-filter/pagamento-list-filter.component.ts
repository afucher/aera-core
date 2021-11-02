import { PagamentoService } from 'src/app/pagamento.service';
import { Component, OnInit } from '@angular/core';
import { PoDisclaimer, PoDisclaimerGroupRemoveAction, PoNotificationService, PoTableAction } from '@po-ui/ng-components';
import { PoPageDynamicTableActions } from '@po-ui/ng-templates';

export interface Pagamento {
  valor: number;
  parcela: number;
  totalDeParcelas: number;
  pago: boolean;
  idMatricula: number;
  dataDeVencimento: string;
  turma: string;
  nomeCurso: string;
  idTurma: number;
}

@Component({
  selector: 'app-pagamento-list-filter',
  templateUrl: './pagamento-list-filter.component.html',
  styleUrls: ['./pagamento-list-filter.component.css']
})
export class PagamentoListFilterComponent implements OnInit {
  disclaimers: PoDisclaimer[] = [];
  private idTurmaFiltrada: number | undefined;
  periodo;
  pendentes = true;
  readonly actions: PoPageDynamicTableActions = {};

  constructor(
    private pagamentoService: PagamentoService,
    private poNotification: PoNotificationService) { }

  pagamentoColumns: Array<any> = [
    {property: 'aluno', label: 'Aluno', readonly: true},
    {property: 'dataDeVencimento', label: 'Vencimento', type: 'date', readonly: true},
    {property: 'valor', label: 'Valor', type: 'currency', format: 'BRL', readonly: true},
    {property: 'parcela', label: 'Parcela', readonly: true},
    {property: 'totalDeParcelas', label: 'Parcelas', readonly: true},
    {property: 'pago', label: 'Pago?', type: 'boolean', boolean: {trueLabel: 'Pago', falseLabel: 'Em aberto'}, readonly: true},
    {
      property: 'nomeDoCurso',
      label: 'Turma',
      type: 'link',
      action: (valor, row) => {
        if (this.idTurmaFiltrada === row.idTurma) { return; }
        this.idTurmaFiltrada = row.idTurma;
        this.populateDisclaimers(valor);
        this.filter();
      }
    },
    {property: 'idTurma', visible: false}
  ];
  pagamentoActions: Array<PoTableAction> = [
    { action: this.pagar.bind(this), label: 'Pagar', disabled: pagamento => pagamento.pago }
  ];

  pagamentos: Pagamento[] = [];
  pagamentosFiltrados: Pagamento[] = [];

  ngOnInit(): void {
    const date = new Date();
    const firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    const lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    this.periodo = {start: firstDay.toISOString().slice(0, 10), end: lastDay.toISOString().slice(0, 10)};
    this.obterPagamentos();
  }

  populateDisclaimers(disclaimer: string) {
    this.disclaimers = [{value: disclaimer, property: 'search'}];
  }

  clearDisclaimers(){
    this.disclaimers = [];
  }

  filter() {
    if (this.idTurmaFiltrada) {
      this.pagamentosFiltrados = this.pagamentos.filter(p => p.idTurma === this.idTurmaFiltrada);
    } else {
      this.pagamentosFiltrados = this.pagamentos;
    }
  }

  obterPagamentos() {
    this.pagamentoService
      .obterPorTurmas(this.periodo.start, this.periodo.end)
      .subscribe(turmas => {
        this.pagamentos = turmas
        .flatMap(t => t.matriculas
          .flatMap(m => m.pagamentos
            .map(p => {
              return {
                ...p,
                idTurma: t.id,
                idMatricula: m.id,
                aluno: m.aluno.nome,
                nomeDoCurso: this.montaNomeTurma(t)};
            })));
        this.pagamentosFiltrados = [...this.pagamentos];
      });
  }

  private montaNomeTurma(turma) {
    const periodo = parseInt(turma.horárioInicial.split(':')[0], 10) < 18 ? 'T' : 'N';
    return `${turma.diaDaSemana + 1}${periodo}_${turma.nomeCurso}`;
  }

  remove(_: PoDisclaimerGroupRemoveAction) {
    this.clearDisclaimers();
    this.idTurmaFiltrada = undefined;
    this.filter();
  }

  pagar(pagamento: Pagamento) {
    const pagamentoParaAtualizar = this.pagamentos.find(p =>
      p.idMatricula === pagamento.idMatricula &&
      p.parcela === pagamento.parcela);
    pagamentoParaAtualizar.pago = true;
    this.pagamentoService.pagar(pagamento)
      .subscribe(() => {
        this.poNotification.success(`Parcela ${pagamento.parcela} de ${pagamento.totalDeParcelas} paga!`);
      }, () => {
        pagamentoParaAtualizar.pago = false;
        this.poNotification.error('Erro ao pagar a parcela, atualize e tente novamente.');
      });
  }
}
