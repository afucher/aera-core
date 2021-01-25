import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { PoModule } from '@po-ui/ng-components';
import { FormsModule } from '@angular/forms';
import { ClientListComponent } from './client-list/client-list.component';
import { PoPageDynamicTableModule } from '@po-ui/ng-templates';
import { HttpClientModule } from '@angular/common/http';
import { TurmaListComponent } from './turma/turma-list/turma-list.component';
import { TurmaEditComponent } from './turma/turma-edit/turma-edit.component';
import { TurmaNovaComponent } from './turma/turma-nova/turma-nova.component';
import { CursoListComponent } from './curso/curso-list/curso-list.component';
import { CursoEditComponent } from './curso/curso-edit/curso-edit.component';
import { PagamentoListComponent } from './pagamento/pagamento-list/pagamento-list.component';
import { ClienteNovoComponent } from './cliente/cliente-novo/cliente-novo.component';
import { ClienteAlteraComponent } from './cliente/cliente-altera/cliente-altera.component';
import { PagamentoDetalhesComponent } from './cliente/pagamento-detalhes/pagamento-detalhes.component';
import { ClienteResumoComponent } from './cliente/cliente-resumo/cliente-resumo.component';

@NgModule({

  declarations: [
    AppComponent,
    ClientListComponent,
    TurmaListComponent,
    TurmaEditComponent,
    TurmaNovaComponent,
    CursoListComponent,
    CursoEditComponent,
    PagamentoListComponent,
    ClienteNovoComponent,
    ClienteAlteraComponent,
    PagamentoDetalhesComponent,
    ClienteResumoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    PoModule,
    PoPageDynamicTableModule,
    HttpClientModule,
    CommonModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
