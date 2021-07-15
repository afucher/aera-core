import { Route } from '@angular/compiler/src/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PoDynamicFormField, PoLookupColumn, PoModalAction, PoModalComponent, PoNotificationService, PoTableAction, PoTableColumn } from '@po-ui/ng-components';
import { Cliente } from 'src/app/models/cliente';
import { Turma } from 'src/app/models/turma';
import { TurmaService } from 'src/app/turma.service';

@Component({
  selector: 'app-turma-edit',
  templateUrl: './turma-edit.component.html',
  styleUrls: ['./turma-edit.component.css']
})
export class TurmaEditComponent implements OnInit {
  frequenciaForm: FormGroup;
  alunoParaMatricular: number = undefined;
  alunoParaAtualizarFrequencia: Cliente = undefined;
  actions: Array<PoTableAction> = [
    {label: 'Frequencia', action: (aluno) => {
      this.frequenciaForm = this.fb.group({
        frequencia: [aluno.frequencia, Validators.compose([Validators.required, Validators.min(0), Validators.max(this.turma.quantidadeDeAulas)])]
      });
      this.alunoParaAtualizarFrequencia = aluno;
      this.modalFrequencia.open();
    }
  }];
  colunasLookup: Array<PoLookupColumn> = [{property: 'nome'}];
  close: PoModalAction = {
    action: () => {
      this.closeModal();
    },
    label: 'Cancelar'
  };

  confirm: PoModalAction = {
    action: () => {
      if (this.alunoParaMatricular !== undefined) {
        this.turmaService
          .matricular(this.turma, this.alunoParaMatricular)
          .subscribe(t => {
            this.turma = t;
            this.closeModal();
            this.poNotification.success('Aluno matriculado');
          });
     }
    },
    label: 'Matricular'
  };

  fechaPagamentos: PoModalAction = {
    action: () => {
      this.modalPagamentos.close();
    },
    label: 'Cancelar'
  };

  fechaFrequencia: PoModalAction = {
    action: () => {
      this.modalFrequencia.close();
    },
    label: 'Cancelar'
  };

  atualizaFrequencia: PoModalAction = {
    action: () => {
      this.modalFrequencia.close();
    },
    label: 'Atualizar'
  };

  geraPagamentos: PoModalAction = {
    action: () => {
      this.turmaService
        .gerarPagamentos(this.turma,
          this.geraPagamentoModel.dataVencimento,
          this.geraPagamentoModel.valor,
          this.geraPagamentoModel.parcelas)
        .subscribe((_) => this.modalPagamentos.close());
    },
    label: 'Gerar'
  };

  turma: Turma;
  columns: Array<PoTableColumn> = [{
    property: 'nome'
  }];
  turmaColumns: Array<PoTableColumn> = [
    { property: 'id' },
    { property: 'nome' },
    { property: 'turmas', type: 'detail', detail: { columns: [
      {property: 'curso'},
      {property: 'dataFinal'}
    ]}}
  ];
  fieldsPagamentos: Array<PoDynamicFormField> = [
    { property: 'dataVencimento', label: 'Data Vencimento 1a parcela', gridColumns: 4, gridSmColumns: 12, type: 'date', required: true},
    { property: 'valor', label: 'Valor parcela', gridColumns: 4, gridSmColumns: 12, type: 'currency', format: 'BRL', required: true},
    { property: 'parcelas', label: 'Número de parcelas', gridColumns: 4, gridSmColumns: 12, type: 'number', required: true},
  ];
  geraPagamentoModel: {parcelas: number, valor: number, dataVencimento: Date} = {parcelas: 1, valor: undefined, dataVencimento: undefined};
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

  @ViewChild('optionsForm', { static: true }) form: NgForm;
  @ViewChild('modalMatricula', { static: true }) poModal: PoModalComponent;
  @ViewChild('modalPagamentos', { static: true }) modalPagamentos: PoModalComponent;
  @ViewChild('modalFrequencia', { static: true }) modalFrequencia: PoModalComponent;

  constructor(
    public poNotification: PoNotificationService,
    private route: ActivatedRoute,
    private router: Router,
    private turmaService: TurmaService,
    private fb: FormBuilder) {
      this.frequenciaForm = this.fb.group({frequencia: ['']});
    }

  closeModal() {
    this.alunoParaMatricular = undefined;
    this.poModal.close();
  }

  abreMatricular() {
    this.poModal.open();
  }

  abrePagamentos() {
    this.modalPagamentos.open();
  }

  listaPresenca() {
    window.open(`impressao/lista-presenca/${this.turma.id}`, '_blank');
  }

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
    const id = parseInt(this.route.snapshot.paramMap.get('id'), 10);
    this.turmaService.obter(id)
      .subscribe(turma => this.turma = turma);
  }

}
