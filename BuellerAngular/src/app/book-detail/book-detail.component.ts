import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Book } from '../book';
import { Class } from '../class';
import { BookService } from '../book.service';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent implements OnInit {
  @Input() book: Book;
  classes: Class[];

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getBook();
    //this.getClasses();
  }

  getBook(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.bookService.getBook(id).subscribe(book => this.book = book);
  }

  // getClasses(): void {
  //   const id = +this.route.snapshot.paramMap.get('id');
  //   this.bookService.getClasses(id).subscribe(classes => this.classes = classes);
  // }

  save(Title: string, Description: string, Price: number): void {
    if (Title == this.book.Title && Description == this.book.Description && Price == this.book.Price) {return;}
    var BookId = this.book.BookId;
    this.bookService.updateBook({ BookId,Title,Price,Description } as Book)
      .subscribe(() => this.goBack());
  }

  goBack(): void {
    this.location.back();
  }
}
