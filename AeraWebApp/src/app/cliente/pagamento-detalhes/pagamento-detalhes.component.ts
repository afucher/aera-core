import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PoTableAction, PoTableColumn } from '@po-ui/ng-components';
import { ClienteService } from 'src/app/cliente.service';
import { Cliente } from 'src/app/models/cliente';

@Component({
  selector: 'app-pagamento-detalhes',
  templateUrl: './pagamento-detalhes.component.html',
  styleUrls: ['./pagamento-detalhes.component.css']
})
export class PagamentoDetalhesComponent implements OnInit {
  cliente: Cliente;
  actions: Array<PoTableAction> = [];
  columns: Array<PoTableColumn> = [
    { property: 'idMatricula', label: 'Matricula' },
    { property: 'detail', label: 'Details', type: 'detail', detail: {columns: [
      { property: 'idMatricula', label: 'Matricula' },
    ]} }
  ];
  constructor(
    private route: ActivatedRoute,
    private clienteService: ClienteService) { }

  ngOnInit(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id'), 10);
    this.clienteService.obterDetalhePagamentos(id)
      .subscribe(c => this.cliente = c);
  }

}
