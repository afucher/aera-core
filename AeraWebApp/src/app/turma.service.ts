import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Turma } from './models/turma';

@Injectable({
  providedIn: 'root',
})
export class TurmaService {

  private url = '/api/turmas';

  constructor(private http: HttpClient) { }


  obter(id: number): Observable<Turma> {
    const url = `${this.url}/${id}`;
    return this.http.get<Turma>(url).pipe(
      tap(_ => console.log(`fetched turma id=${id}`)),
      catchError(this.handleError<Turma>(`obter turma id=${id}`))
    );
  }

  salvar(turma: Turma): Observable<Turma> {
    return this.http.put<Turma>(`${this.url}/${turma.id}`, turma)
      .pipe(
        catchError(this.handleError('salvarTurma', turma))
      );
  }

  criar(turma: Turma): Observable<Turma> {
    return this.http.post<Turma>(this.url, turma)
      .pipe(
        catchError(this.handleError('criarTurma', turma))
      );
  }

  matricular(turma: Turma, alunoId: number): Observable<Turma> {
    return this.http.post<Turma>(`${this.url}/${turma.id}/matricular`, alunoId)
      .pipe(
        catchError(this.handleError('matricularAluno', turma))
      );
  }

  gerarPagamentos(turma: Turma, dataVencimento: Date, valor: number, parcelas: number): Observable<Turma> {
    const url = `${this.url}/${turma.id}/pagamentos?dataVencimento=${dataVencimento}&valor=${valor}&parcelas=${parcelas}`;
    return this.http.post<Turma>(url, {})
      .pipe(
        catchError(this.handleError('matricularAluno', turma))
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
