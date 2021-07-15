import { TurmaService } from './../../turma.service';
import { ActivatedRoute } from '@angular/router';
import { Turma } from './../../models/turma';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-lista-presenca',
  templateUrl: './lista-presenca.component.html',
  styleUrls: ['./lista-presenca.component.css']
})
export class ListaPresencaComponent implements OnInit {

  turma: Turma = {
    curso : 'Princípios e Fundamentos',
    professor: 'Professor do curso',
    dataInicial : '21/03/2001',
    dataFinal: '22/02/2090',
    alunos : [{
      nome: 'Aluno 1 com sobrenome'
    },
    {
      nome: 'Aluno 2 sem sobrenome'
    }]
  };

  constructor(private route: ActivatedRoute, private turmaService: TurmaService) { }

  ngOnInit(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('turmaId'), 10);
    this.turmaService.obter(id).subscribe(t => this.turma = t);
  }

}
