import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PoDynamicFormComponent, PoDynamicFormField, PoNotificationService } from '@po-ui/ng-components';
import { CursoService } from 'src/app/curso.service';
import { Curso, campos } from 'src/app/models/curso';

@Component({
  selector: 'app-curso-novo',
  templateUrl: './curso-novo.component.html',
  styleUrls: ['./curso-novo.component.css']
})
export class CursoNovoComponent implements OnInit {
  curso: Curso = {nome: ''}
  fields: Array<PoDynamicFormField> = campos.filter(c => c.property !== 'id');
  constructor(
    private cursoService: CursoService,
    private router: Router,
    private notification: PoNotificationService) { }

  ngOnInit(): void {
  }

  salvar(form: PoDynamicFormComponent) {
    this.cursoService.criar(form.value)
      .subscribe(c => {
        this.router.navigate([`/cursos/${c.id}`]);
        this.notification.success(`Curso ${c.nome} criado com sucesso!`);
      },() => this.notification.error('Erro ao criar o curso'))
  }

}
