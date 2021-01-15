import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientListComponent } from './client-list/client-list.component';
import { ClienteComponent } from './cliente/cliente.component';
import { TurmaListComponent } from './turma/turma-list/turma-list.component';
import { TurmaEditComponent } from './turma/turma-edit/turma-edit.component';


const routes: Routes = [
  {path: 'clientes', component: ClientListComponent},
  {path: 'clientes/:id', component: ClienteComponent},
  {path: 'turmas', component: TurmaListComponent},
  {path: 'turmas/:id', component: TurmaEditComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
