import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Curso } from './models/curso';

@Injectable({
  providedIn: 'root',
})
export class CursoService {

  private url = '/api/cursos';

  constructor(private http: HttpClient) { }


  obter(id: number): Observable<Curso> {
    const url = `${this.url}/${id}`;
    return this.http.get<Curso>(url).pipe(
      tap(_ => console.log(`fetched curso id=${id}`)),
      catchError(this.handleError<Curso>(`obter curso id=${id}`))
    );
  }

  criar(curso: Curso): Observable<Curso> {
    return this.http.post<Curso>(this.url, curso)
      .pipe(
        catchError(this.handleError('criarCurso', curso))
      );
  }

  salvar(curso: Curso): Observable<Curso> {
    return this.http.put<Curso>(`${this.url}/${curso.id}`, curso)
      .pipe(
        catchError(this.handleError('salvarCurso', curso))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      throw(error);
    };
  }
}
