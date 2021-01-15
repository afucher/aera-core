import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PoDynamicFormField } from '@po-ui/ng-components';
import { Turma } from 'src/app/models/turma';
import { TurmaService } from 'src/app/turma.service';

@Component({
  selector: 'app-turma-edit',
  templateUrl: './turma-edit.component.html',
  styleUrls: ['./turma-edit.component.css']
})
export class TurmaEditComponent implements OnInit {

  turma: Turma;
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
      property: 'curso',
      label: 'Nome',
      required: true,
      minLength: 4,
      maxLength: 50,
      gridColumns: 6,
      gridSmColumns: 12,
      order: 2,
      disabled: true
    },
    { property: 'dataInicial', label: 'Início', gridColumns: 6, gridSmColumns: 12, type: 'date', format: 'yyyy-MM-dd'},
    { property: 'dataFinal', label: 'Término', gridColumns: 6, gridSmColumns: 12, type: 'date', format: 'yyyy-MM-dd'}];

  constructor(
    private route: ActivatedRoute,
    private turmaService: TurmaService) { }

  ngOnInit(): void {
    // tslint:disable-next-line: radix
    const id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.turmaService.obter(id)
      .subscribe(turma => this.turma = turma);
  }

}
