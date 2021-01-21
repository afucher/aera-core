import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PoDynamicFormField, PoNotificationService, PoPageAction } from '@po-ui/ng-components';
import { ClienteService } from 'src/app/cliente.service';
import { Cliente, clienteFields } from 'src/app/models/cliente';

@Component({
  selector: 'app-cliente-altera',
  templateUrl: './cliente-altera.component.html',
  styleUrls: ['./cliente-altera.component.css']
})
export class ClienteAlteraComponent implements OnInit {
  cliente: Cliente;
  fields: Array<PoDynamicFormField> = clienteFields;

  constructor(
    private route: ActivatedRoute,
    public poNotification: PoNotificationService,
    private clienteService: ClienteService) { }

  ngOnInit(): void {
    // tslint:disable-next-line: radix
    const id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.clienteService.obter(id)
      .subscribe(cliente => this.cliente = cliente);
  }

  formataHorário(horario: string) {
    if (horario && horario.length === 4) { return horario.slice(0, 2) + ':' + horario.slice(2); }
    return horario;
  }

  onSubmit(e) {
    this.clienteService
      .salvar({...e.value, hora_nascimento: this.formataHorário(e.value.hora_nascimento)})
      .subscribe((_) => this.poNotification.success('Cliente atualizado com sucesso!'));
  }


}
