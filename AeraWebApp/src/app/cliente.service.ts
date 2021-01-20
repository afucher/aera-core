import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Cliente } from './models/cliente';

@Injectable({
  providedIn: 'root',
})
export class ClienteService {

  private url = '/api/clientes';

  constructor(private http: HttpClient) { }


  obter(id: number): Observable<Cliente> {
    const url = `${this.url}/${id}`;
    return this.http.get<Cliente>(url).pipe(
      tap(_ => console.log(`fetched cliente id=${id}`)),
      catchError(this.handleError<Cliente>(`obter cliente id=${id}`))
    );
  }

  criar(cliente: Cliente): Observable<Cliente> {
    return this.http.post<Cliente>(this.url, cliente).pipe(
      catchError(this.handleError<Cliente>(`criar cliente`))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
