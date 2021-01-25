import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PoModalComponent, PoTableAction, PoTableColumn } from '@po-ui/ng-components';
import { ClienteService } from 'src/app/cliente.service';
import { Cliente } from 'src/app/models/cliente';
import { Turma } from 'src/app/models/turma';

@Component({
  selector: 'app-cliente-resumo',
  templateUrl: './cliente-resumo.component.html',
  styleUrls: ['./cliente-resumo.component.css']
})
export class ClienteResumoComponent implements OnInit {
  cliente: Cliente;
  turma: Turma;
  pagamentos: any[];
  actions: Array<PoTableAction> = [
    { action: this.mostrarPagamentos.bind(this), icon: 'po-icon-finance', label: 'Pagamentos' }
  ];
  columns: Array<PoTableColumn> = [
    {property: 'curso', label: 'Curso'},
    {property: 'emAndamento', label: 'Status', type: 'boolean', boolean: {trueLabel: 'Em andamento', falseLabel: 'Finalizado'}}
  ];
  pagamentoColumns: Array<PoTableColumn> = [
    {property: 'dataDeVencimento', label: 'Vencimento', type: 'date'},
    {property: 'valor', label: 'Valor', type: 'currency', format: 'BRL'},
    {property: 'parcela', label: 'Parcela'},
    {property: 'totalDeParcelas', label: 'Parcelas'},
    {property: 'pago', label: 'Pago?', type: 'boolean', boolean: {trueLabel: 'Pago', falseLabel: 'Em aberto'}},
  ];
  pagamentoActions: Array<PoTableAction> = [
    { action: () => {}, label: 'Pagar', disabled: pagamento => pagamento.pago }
  ];

  @ViewChild(PoModalComponent, { static: true }) poModal: PoModalComponent;

  constructor(
    private route: ActivatedRoute,
    private clienteService: ClienteService
  ) { }

  ngOnInit(): void {
     // tslint:disable-next-line: radix
     const id = parseInt(this.route.snapshot.paramMap.get('id'));
     this.clienteService.obter(id)
       .subscribe(c => this.cliente = c);
  }

  mostrarPagamentos(turma: Turma) {
    this.clienteService.teste(turma.id, this.cliente.id).subscribe(p => this.pagamentos = p);
    this.turma = turma;
    this.poModal.open();
  }

}
