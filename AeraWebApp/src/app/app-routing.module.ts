import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientListComponent } from './client-list/client-list.component';
import { ClienteComponent } from './cliente/cliente.component';
import { ClienteNovoComponent } from './cliente/cliente-novo/cliente-novo.component';
import { TurmaListComponent } from './turma/turma-list/turma-list.component';
import { TurmaEditComponent } from './turma/turma-edit/turma-edit.component';
import { TurmaNovaComponent } from './turma/turma-nova/turma-nova.component';
import { CursoListComponent } from './curso/curso-list/curso-list.component';
import { CursoEditComponent } from './curso/curso-edit/curso-edit.component';
import { PagamentoListComponent } from './pagamento/pagamento-list/pagamento-list.component';

const routes: Routes = [
  {path: 'clientes', component: ClientListComponent},
  {path: 'clientes/novo', component: ClienteNovoComponent},
  {path: 'clientes/:id', component: ClienteComponent},
  {path: 'turmas', component: TurmaListComponent},
  {path: 'turmas/nova', component: TurmaNovaComponent},
  {path: 'turmas/:id', component: TurmaEditComponent},
  {path: 'cursos', component: CursoListComponent},
  {path: 'cursos/:id', component: CursoEditComponent},
  {path: 'pagamentos', component: PagamentoListComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
