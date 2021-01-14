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
