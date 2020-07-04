import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PoModule } from '@po-ui/ng-components';
import { RouterModule } from '@angular/router';
import { ClientListComponent } from './client-list/client-list.component';
import { PoPageDynamicTableModule } from '@po-ui/ng-templates';

@NgModule({

  declarations: [
    AppComponent,
    ClientListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    PoModule,
    PoPageDynamicTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
