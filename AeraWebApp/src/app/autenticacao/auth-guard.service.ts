import { AutenticacaoService } from './autenticacao.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService implements CanActivate {
  constructor(public autenticacao: AutenticacaoService, public router: Router) {}

  canActivate(): boolean {
    if (!this.autenticacao.estaLogado()) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
}}
