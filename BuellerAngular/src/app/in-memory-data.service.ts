import { InMemoryDbService } from 'angular-in-memory-web-api';

export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    const books = [
      { id: 1, price: 19.99, title: 'Introduction to Calculus', description: 'Calculus I & II book' },
      { id: 2, price: 9.99, title: 'Multivariate Calculus', description: 'Calculus III book' },
      { id: 3, price: 19.99, title: 'Accounting', description: 'Accounting I & II book' },
      { id: 4, price: 14.99, title: 'Chemistry', description: 'Chemistry book' },
      { id: 5, price: 14.99, title: 'Computer Architecture', description: 'Computer architecture book' },
    ];
    return {books};
  }
}