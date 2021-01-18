import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { PoModule } from '@po-ui/ng-components';
import { FormsModule } from '@angular/forms';
import { ClientListComponent } from './client-list/client-list.component';
import { PoPageDynamicTableModule } from '@po-ui/ng-templates';
import { ClienteComponent } from './cliente/cliente.component';
import { HttpClientModule } from '@angular/common/http';
import { TurmaListComponent } from './turma/turma-list/turma-list.component';
import { TurmaEditComponent } from './turma/turma-edit/turma-edit.component';
import { TurmaNovaComponent } from './turma/turma-nova/turma-nova.component';
import { CursoListComponent } from './curso/curso-list/curso-list.component';
import { CursoEditComponent } from './curso/curso-edit/curso-edit.component';

@NgModule({

  declarations: [
    AppComponent,
    ClientListComponent,
    ClienteComponent,
    TurmaListComponent,
    TurmaEditComponent,
    TurmaNovaComponent,
    CursoListComponent,
    CursoEditComponent
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
