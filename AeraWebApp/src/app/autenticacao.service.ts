import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { shareReplay, tap } from 'rxjs/operators';
import jwt_decode from 'jwt-decode';
import { DateTime } from 'luxon';

type Autenticacao = {
  access_token: string
}

@Injectable({
  providedIn: 'root',
})
export class AutenticacaoService {

  private url = '/api/autenticacao';

  constructor(private http: HttpClient) { }


  login(usuario: string, senha: string): Observable<any> {
    return this.http.post<Autenticacao>(`${this.url}/login`, {usuario, senha})
    .pipe(
      tap(this.setSession),
      shareReplay()
    );
  }

  private setSession(autenticacao: Autenticacao) {
    const expiraEm = (jwt_decode(autenticacao.access_token) as any).exp;

    localStorage.setItem('id_token', autenticacao.access_token);
    localStorage.setItem('expires_at', JSON.stringify(expiraEm));
  }

  private obterExpiracao(): DateTime {
    const expiracao = localStorage.getItem('expires_at');

    return DateTime.fromSeconds(JSON.parse(expiracao) || 0);

  }

  estaLogado() {
    return this.obterExpiracao() >= DateTime.utc();
  }

  logout() {
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
  }
}
