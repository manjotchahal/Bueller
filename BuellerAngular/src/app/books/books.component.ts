import { Component, OnInit } from '@angular/core';

import { Book } from '../book';
import { BookService } from '../book.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {
  books: Book[];

  constructor(private bookService: BookService) { }

  ngOnInit() {
    this.getBooks();
  }

  getBooks(): void {
    this.bookService.getBooks().subscribe(books => this.books = books);
  }

  add(Title: string, Description: string, Price: number): void {
    Title = Title.trim();
    Description = Description.trim();
    //number BookId = 0;
    if (!Title || !Price) { return; }
    this.bookService.addBook({ Title, Description, Price } as Book).subscribe(book => { this.books.push(book); });
    location.reload();
  }

  delete(book: Book): void {
    this.books = this.books.filter(b => b !== book);
    this.bookService.deleteBook(book).subscribe();
  }
}
