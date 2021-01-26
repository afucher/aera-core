import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PoDynamicFormField, PoNotificationService } from '@po-ui/ng-components';
import { ClienteService } from 'src/app/cliente.service';
import { Cliente, clienteFields } from 'src/app/models/cliente';

@Component({
  selector: 'app-cliente-novo',
  templateUrl: './cliente-novo.component.html',
  styleUrls: ['./cliente-novo.component.css']
})
export class ClienteNovoComponent implements OnInit {

  cliente: Cliente = {nome: ''};
  fields: Array<PoDynamicFormField> = clienteFields.filter(x => x.property !== 'id');

  @ViewChild('clienteForm', { static: true }) form: NgForm;

  constructor(
    public poNotification: PoNotificationService,
    private clienteService: ClienteService) { }

    formataHorário(horario: string) {
      if (horario && horario.length === 4) { return horario.slice(0, 2) + ':' + horario.slice(2); }
      return horario;
    }

  onSubmit(e) {
    this.clienteService
      .criar({...e.value, hora_nascimento: this.formataHorário(e.value.hora_nascimento)})
      .subscribe(
        (_) => this.poNotification.success('Cliente criado com sucesso!'),
        (err) => this.poNotification.error('Erro ao criar o cliente')
      );
   }

  ngOnInit(): void {}

}
