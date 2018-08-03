import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

import { Book } from './book';
import { Class } from './class';
import { MessageService } from './message.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class BookService {
  //private booksUrl = 'api/books'; // URL to web api
  private booksUrl = 'http://localhost:58782/api/Book'; // URL to web api

  constructor(private http: HttpClient, private messageService: MessageService) { }

  /** GET books from the server */
  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.booksUrl+'/GetAll').pipe(tap(books => this.log('fetched books')), catchError(this.handleError('getBooks', [])));
  }

  /** GET book by id. Will 404 if id not found */
  getBook(id: number): Observable<Book> {
    const url = `${this.booksUrl}/GetById/${id}`;
    return this.http.get<Book>(url).pipe(tap(_ => this.log(`fetched book id=${id}`)), catchError(this.handleError<Book>(`getBook id=${id}`)));
  } 

  /** GET classes by book id. Will 404 if id not found */
  getClasses(id: number): Observable<Class[]> {
    const url = `${this.booksUrl}/GetClassesByBookId/${id}`;
    return this.http.get<Class[]>(url).pipe(tap(classes => this.log(`fetched classes by book id=${id}`)), catchError(this.handleError(`getClasses id=${id}`,[])));
  } 

  /** PUT: update the book on the server */
  updateBook (book: Book): Observable<any> {
    return this.http.put(this.booksUrl+`/Edit/${book.BookId}`, book, httpOptions).pipe(tap(_ => this.log(`updated book id=${book.id}`)), catchError(this.handleError<any>('updateBook')));
  }

  /** POST: add a new book to the server */
  addBook (book: Book): Observable<Book> {
    return this.http.post(this.booksUrl+'/Add', book, httpOptions).pipe(tap((book: Book) => this.log(`added book w/ id=${book.id}`)), catchError(this.handleError<Book>('addBook')));
  }

  /** DELETE: delete the hero from the server */
  deleteBook (book: Book | number): Observable<Book> {
    const id = typeof book === 'number' ? book : book.BookId;
    const url = `${this.booksUrl}/Delete/${id}`;
    return this.http.delete<Book>(url, httpOptions).pipe(tap(_ => this.log(`deleted book id=${id}`)), catchError(this.handleError<Book>('deleteBook')));
  }

  /* GET books whose name contains search term */
  searchBooks(term: string): Observable<Book[]> {
    if (!term.trim()) {
      // if not search term, return empty hero array.
      return of([]);
    }
    return this.http.get<Book[]>(`${this.booksUrl}/?title=${term}`).pipe(
      tap(_ => this.log(`found books matching "${term}"`)),
      catchError(this.handleError<Book[]>('searchBooks', []))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
  */
  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a BookService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`BookService: ${message}`);
  }
}
