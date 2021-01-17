import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PoDynamicFormField, PoNotificationService, PoTableColumn } from '@po-ui/ng-components';
import { Turma } from 'src/app/models/turma';
import { TurmaService } from 'src/app/turma.service';

@Component({
  selector: 'app-turma-edit',
  templateUrl: './turma-edit.component.html',
  styleUrls: ['./turma-edit.component.css']
})
export class TurmaEditComponent implements OnInit {

  turma: Turma;
  columns: Array<PoTableColumn> = [{
    property: 'nome'
  }];
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
    { property: 'dataInicial', label: 'Início', gridColumns: 2, gridSmColumns: 12, type: 'date', format: 'yyyy-MM-dd', required: true},
    { property: 'dataFinal', label: 'Término', gridColumns: 2, gridSmColumns: 12, type: 'date', format: 'yyyy-MM-dd', required: true},
    { property: 'horárioInicial', label: 'Horário Inicial', gridColumns: 2, gridSmColumns: 12, type: 'time',
      required: true},
    { property: 'horárioFinal', label: 'Horário Final', gridColumns: 2, gridSmColumns: 12, type: 'time', required: true},
    { property: 'quantidadeDeAulas', label: 'Aulas', gridColumns: 2, gridSmColumns: 12, type: 'number', required: true},
    { property: 'professorId', label: 'Professor', gridColumns: 3, gridSmColumns: 12, required: true,
    optionsService: '/api/professores', fieldLabel: 'nome', fieldValue: 'id'}];

  constructor(
    public poNotification: PoNotificationService,
    private route: ActivatedRoute,
    private turmaService: TurmaService) { }

  formataHorário(horario: string) {
    if (horario.length === 4) { return horario.slice(0, 2) + ':' + horario.slice(2); }
    return horario;
  }

  onSubmit() {
    this.turmaService
      .salvar({...this.turma,
        horárioInicial: this.formataHorário(this.turma.horárioInicial),
        horárioFinal: this.formataHorário(this.turma.horárioFinal)})
      .subscribe((_) => this.poNotification.success('Turma salva com sucesso!'));
   }

  ngOnInit(): void {
    // tslint:disable-next-line: radix
    const id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.turmaService.obter(id)
      .subscribe(turma => this.turma = turma);
  }

}
