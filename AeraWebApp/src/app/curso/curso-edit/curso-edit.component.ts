import { CursoService } from './../../curso.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PoDynamicFormField, PoNotificationService } from '@po-ui/ng-components';
import { Curso } from 'src/app/models/curso';

@Component({
  selector: 'app-curso-edit',
  templateUrl: './curso-edit.component.html',
  styleUrls: ['./curso-edit.component.css']
})
export class CursoEditComponent implements OnInit {

  curso: Curso;
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
      order: 2,
      disabled: true
    },
  {property: 'cargaHorária', label: 'Carga Horária', gridColumns: 2, required: true, type: 'number'},
  {property: 'descrição', label: 'Descrição', rows: 3, maxLength: 255}];

  constructor(
    public poNotification: PoNotificationService,
    private route: ActivatedRoute,
    private cursoService: CursoService) { }

  onSubmit() {
    this.cursoService
      .salvar(this.curso)
      .subscribe((_) => this.poNotification.success('Curso salvo com sucesso!'));
   }

  ngOnInit(): void {
    // tslint:disable-next-line: radix
    const id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.cursoService.obter(id)
      .subscribe(curso => this.curso = curso);
  }
}
