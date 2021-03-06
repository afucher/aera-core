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

  teste(id: number, clienteId: number): Observable<any> {
    const url = `/api/pagamentos/turma/${id}/aluno/${clienteId}`;
    return this.http.get<any>(url).pipe(
      tap(_ => console.log(`fetched cliente id=${id}`)),
      catchError(this.handleError<any>(`obter cliente id=${id}`))
    );
  }

  criar(cliente: Cliente): Observable<Cliente> {
    return this.http.post<Cliente>(this.url, cliente).pipe(
      catchError(this.handleError<Cliente>(`criar cliente`))
    );
  }

  salvar(cliente: Cliente): Observable<Cliente> {
    return this.http.put<Cliente>(`${this.url}/${cliente.id}`, cliente).pipe(
      catchError(this.handleError<Cliente>(`alterar cliente`))
    );
  }

  obterDetalhePagamentos(id: number) {
    const url = `${this.url}/${id}/pagamentos`;
    return this.http.get<Cliente>(url).pipe(
      tap(_ => console.log(`fetched clientePagamentos id=${id}`)),
      catchError(this.handleError<Cliente>(`obter clientePagamentos id=${id}`))
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
