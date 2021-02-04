import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientListComponent } from './client-list/client-list.component';
import { ClienteAlteraComponent } from './cliente/cliente-altera/cliente-altera.component';
import { ClienteNovoComponent } from './cliente/cliente-novo/cliente-novo.component';
import { ClienteResumoComponent } from './cliente/cliente-resumo/cliente-resumo.component';
import { TurmaListComponent } from './turma/turma-list/turma-list.component';
import { TurmaEditComponent } from './turma/turma-edit/turma-edit.component';
import { TurmaNovaComponent } from './turma/turma-nova/turma-nova.component';
import { CursoListComponent } from './curso/curso-list/curso-list.component';
import { CursoEditComponent } from './curso/curso-edit/curso-edit.component';
import { PagamentoListComponent } from './pagamento/pagamento-list/pagamento-list.component';
import { TesteDownloadComponent } from './download/teste-download/teste-download.component';

const routes: Routes = [
  {path: 'clientes', component: ClientListComponent},
  {path: 'clientes/novo', component: ClienteNovoComponent},
  {path: 'clientes/:id/alterar', component: ClienteAlteraComponent},
  {path: 'clientes/:id/detalhes', component: ClienteResumoComponent},
  {path: 'turmas', component: TurmaListComponent},
  {path: 'turmas/nova', component: TurmaNovaComponent},
  {path: 'turmas/:id', component: TurmaEditComponent},
  {path: 'cursos', component: CursoListComponent},
  {path: 'cursos/:id', component: CursoEditComponent},
  {path: 'pagamentos', component: PagamentoListComponent},
  {path: 'downloads', component: TesteDownloadComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
