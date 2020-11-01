import { ClienteService } from './../cliente.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Cliente } from '../models/cliente';
import { PoDynamicFormField } from '@po-ui/ng-components';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {

  cliente: Cliente;
  fields: Array<PoDynamicFormField> = [
    {
      property: 'id',
      label: 'Id',
      required: true,
      gridColumns: 2,
      gridSmColumns: 2,
      order: 1,
      disabled: true
    },
    {
      property: 'nome',
      label: 'Nome',
      required: true,
      minLength: 4,
      maxLength: 50,
      gridColumns: 6,
      gridSmColumns: 12,
      order: 1
    },
    { property: 'cpf', label: 'CPF', mask: '999.999.999-99', gridColumns: 6, gridSmColumns: 12}]

  constructor(
    private route: ActivatedRoute,
    private clienteService: ClienteService) { }

  ngOnInit(): void {
    // tslint:disable-next-line: radix
    const id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.clienteService.obter(id)
      .subscribe(cliente => this.cliente = cliente);
  }

}
