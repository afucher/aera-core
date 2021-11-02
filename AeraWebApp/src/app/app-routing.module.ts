import { ListaPendentesComponent } from './pagamento/lista-pendentes/lista-pendentes.component';
import { ListaPresencaComponent } from './impressao/lista-presenca/lista-presenca.component';
import { LoginComponent } from './autenticacao/login/login.component';
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
import { CursoNovoComponent } from './curso/curso-novo/curso-novo.component';
import { CursoResumoComponent } from './curso/curso-resumo/curso-resumo.component';
import { PagamentoListComponent } from './pagamento/pagamento-list/pagamento-list.component';
import { AuthGuardService } from './autenticacao/auth-guard.service';
import { PagamentoListFilterComponent } from './pagamento/pagamento-list-filter/pagamento-list-filter.component';

const routes: Routes = [
  {path: '', redirectTo: '/clientes', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'clientes', component: ClientListComponent, canActivate: [AuthGuardService]},
  {path: 'clientes/novo', component: ClienteNovoComponent, canActivate: [AuthGuardService]},
  {path: 'clientes/:id/alterar', component: ClienteAlteraComponent, canActivate: [AuthGuardService]},
  {path: 'clientes/:id/detalhes', component: ClienteResumoComponent, canActivate: [AuthGuardService]},
  {path: 'turmas', component: TurmaListComponent, canActivate: [AuthGuardService]},
  {path: 'turmas/nova', component: TurmaNovaComponent, canActivate: [AuthGuardService]},
  {path: 'turmas/:id', component: TurmaEditComponent, canActivate: [AuthGuardService]},
  {path: 'cursos', component: CursoListComponent, canActivate: [AuthGuardService]},
  {path: 'cursos/novo', component: CursoNovoComponent, canActivate: [AuthGuardService]},
  {path: 'cursos/:id/detalhes', component: CursoResumoComponent, canActivate: [AuthGuardService]},
  {path: 'cursos/:id', component: CursoEditComponent, canActivate: [AuthGuardService]},
  {path: 'pagamentos', component: PagamentoListFilterComponent, canActivate: [AuthGuardService]},
  {path: 'pagamentos/pendentes', component: ListaPendentesComponent, canActivate: [AuthGuardService]},
  {path: 'impressao/lista-presenca/:turmaId', component: ListaPresencaComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
