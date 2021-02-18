import { CursoService } from './../../curso.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PoDynamicFormField, PoNotificationService } from '@po-ui/ng-components';
import { campos, Curso } from 'src/app/models/curso';

@Component({
  selector: 'app-curso-edit',
  templateUrl: './curso-edit.component.html',
  styleUrls: ['./curso-edit.component.css']
})
export class CursoEditComponent implements OnInit {

  curso: Curso;
  fields: Array<PoDynamicFormField> = campos.map(campo => {
    if(campo.property === 'nome') return {...campo, disabled: true}
    return campo
  });

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
