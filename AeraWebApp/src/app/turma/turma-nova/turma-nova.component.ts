import { Component, OnInit } from '@angular/core';
import { PoDynamicFormField, PoNotificationService } from '@po-ui/ng-components';
import { Turma } from 'src/app/models/turma';
import { TurmaService } from 'src/app/turma.service';

@Component({
  selector: 'app-turma-nova',
  templateUrl: './turma-nova.component.html',
  styleUrls: ['./turma-nova.component.css']
})
export class TurmaNovaComponent implements OnInit {

  turma: Turma = {};
  fields: Array<PoDynamicFormField> = [
    {
      property: 'cursoId',
      label: 'Curso',
      required: true,
      gridColumns: 6,
      gridSmColumns: 12,
      searchService: '/api/cursos',
      columns: [
        { property: 'nome', label: 'Nome' }
      ],
      fieldLabel: 'nome',
      fieldValue: 'id'
    },
    { property: 'dataInicial', label: 'Início', gridColumns: 2, gridSmColumns: 12, type: 'date', format: 'yyyy-MM-dd', required: true},
    { property: 'dataFinal', label: 'Término', gridColumns: 2, gridSmColumns: 12, type: 'date', format: 'yyyy-MM-dd', required: true},
    { property: 'horárioInicial', label: 'Horário Inicial', gridColumns: 2, gridSmColumns: 12, type: 'time', format: 'HH:mm',
      required: true},
    { property: 'horárioFinal', label: 'Horário Final', gridColumns: 2, gridSmColumns: 12, type: 'time', format: 'HH:mm', required: true},
    { property: 'quantidadeDeAulas', label: 'Aulas', gridColumns: 2, gridSmColumns: 12, type: 'number', required: true},
    { property: 'professorId', label: 'Professor', gridColumns: 2, gridSmColumns: 12, required: true,
    optionsService: '/api/professores', fieldLabel: 'nome', fieldValue: 'id'}];

  constructor(
    public poNotification: PoNotificationService,
    private turmaService: TurmaService) { }

    formataHorário(horario: string) {
      if (horario.length === 4) { return horario.slice(0, 2) + ':' + horario.slice(2); }
      return horario;
    }

  onSubmit(e) {
    this.turmaService
      .criar({...e.value,
        horárioInicial: this.formataHorário(e.value.horárioInicial),
        horárioFinal: this.formataHorário(e.value.horárioFinal)})
      .subscribe((_) => this.poNotification.success('Turma criada com sucesso!'));
   }

  ngOnInit(): void {}
}
