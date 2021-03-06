import { ListaPresencaComponent } from './impressao/lista-presenca/lista-presenca.component';
import { LoginComponent } from './autenticacao/login/login.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { PoModule } from '@po-ui/ng-components';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ClientListComponent } from './client-list/client-list.component';
import { PoPageLoginModule, PoTemplatesModule } from '@po-ui/ng-templates';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { DownloadListaComponent } from './download/download-list/download-lista.component';
import { DownloadAtestadoComponent } from './download/download-atestado/download-atestado.component';
import { CursoNovoComponent } from './curso/curso-novo/curso-novo.component';
import { CursoResumoComponent } from './curso/curso-resumo/curso-resumo.component';
import { AutenticacaoInterceptor } from './autenticacao/AutenticacaoInterceptor';

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
    ClienteResumoComponent,
    DownloadListaComponent,
    DownloadAtestadoComponent,
    CursoNovoComponent,
    CursoResumoComponent,
    LoginComponent,
    ListaPresencaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    PoModule,
    PoTemplatesModule,
    PoPageLoginModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AutenticacaoInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
