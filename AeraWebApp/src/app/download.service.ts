import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Curso } from './models/curso';

@Injectable({
  providedIn: 'root',
})
export class DownloadService {

  private url = '/api/download';

  constructor(private http: HttpClient) { }

  listaDePresenca(turmaId: number): Observable<Blob> {
    const headerOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/pdf'
    });
    const requestOptions = {headers : headerOptions, responseType: 'blob' as 'blob'};
    return this.http.get(`${this.url}/listaDePresenca/${turmaId}`, requestOptions);
  }

  atestado(alunoId: number): Observable<Blob> {
    const headerOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/pdf'
    });
    const requestOptions = {headers : headerOptions, responseType: 'blob' as 'blob'};
    return this.http.get(`${this.url}/atestado/${alunoId}`, requestOptions);
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
