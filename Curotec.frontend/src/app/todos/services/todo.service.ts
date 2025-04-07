import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Todo } from '../models/todo.model';

@Injectable({ providedIn: 'root' })
export class TodoService {
  private apiUrl = 'http://localhost:5000/api/todo';

  constructor(private http: HttpClient) { }

  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.apiUrl);
  }

  create(todo: Omit<Todo, 'id'>): Observable<Todo> {
    return this.http.post<Todo>(this.apiUrl, todo);
  }

  update(id: string, changes: Partial<Todo>): Observable<Todo> {
    return this.http.put<Todo>(`${this.apiUrl}/${id}`, changes);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
