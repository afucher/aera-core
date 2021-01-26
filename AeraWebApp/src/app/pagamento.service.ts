import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Curso } from './models/curso';

@Injectable({
  providedIn: 'root',
})
export class PagamentoService {

  private url = '/api/pagamentos';

  constructor(private http: HttpClient) { }

  pagar(pagamento: any): Observable<Curso> {
    return this.http.post<any>(this.url, pagamento)
      .pipe(
        catchError(this.handleError('pagamento', pagamento))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      throw(error);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
