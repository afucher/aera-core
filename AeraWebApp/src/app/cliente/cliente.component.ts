import { clienteFields } from 'src/app/models/cliente';
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
  fields: Array<PoDynamicFormField> = clienteFields;

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
