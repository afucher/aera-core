import { AutenticacaoService } from './autenticacao.service';
import { tap } from 'rxjs/operators';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class AutenticacaoInterceptor implements HttpInterceptor {

    constructor(private router: Router, private autenticacao: AutenticacaoService) {}

    intercept(req: HttpRequest<any>,
              next: HttpHandler): Observable<HttpEvent<any>> {

        const idToken = localStorage.getItem('id_token');

        if (idToken) {
            req = req.clone({
                headers: req.headers.set('Authorization',
                    'Bearer ' + idToken)
            });

        }
        return next.handle(req).pipe(tap(() => {}, err => {
          if (err instanceof HttpErrorResponse) {
            if (err.status !== 401) { return; }

            this.autenticacao.logout();
            this.router.navigate(['login']);
          }
        }));
    }
}
