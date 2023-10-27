import { Pokemon } from '../models/pokemon.model';

export class PaginatedList {
  items: Pokemon[] = [];
  pageNumber: number = 0;
  totalPages: number = 0;
  totalCount: number = 0;
  previousPage: string = '';
  nextPage: string = '';
}
