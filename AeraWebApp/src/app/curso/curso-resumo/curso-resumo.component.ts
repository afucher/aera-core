import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PoPageAction } from '@po-ui/ng-components';
import { CursoService } from 'src/app/curso.service';
import { Curso } from 'src/app/models/curso';

@Component({
  selector: 'app-curso-resumo',
  templateUrl: './curso-resumo.component.html',
  styleUrls: ['./curso-resumo.component.css']
})
export class CursoResumoComponent implements OnInit {
  public readonly pageActions: Array<PoPageAction> = [{
    label: 'Alterar',
    action: () => this.router.navigate([`/cursos/${this.curso.id}`])
  }];
  curso: Curso;
  constructor(
    private cursoService: CursoService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.cursoService.obter(id)
      .subscribe(c => this.curso = c);
  }

}
