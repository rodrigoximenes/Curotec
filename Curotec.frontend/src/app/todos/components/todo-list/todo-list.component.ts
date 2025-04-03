import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { AsyncPipe, NgFor, NgIf } from '@angular/common';

import { Todo } from '../../models/todo.model';
import { loadTodos, deleteTodo } from '../../store/todo.actions';
import { selectTodos } from '../../store/todo.selectors';


@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  standalone: true,
  styleUrls: ['./todo-list.component.scss'],
  imports: [MatButtonModule, MatListModule, NgIf, NgFor, AsyncPipe]
})
export class TodoListComponent implements OnInit {
  todos$!: Observable<Todo[]>;

  constructor(private store: Store) { }

  ngOnInit(): void {
    this.todos$ = this.store.select(selectTodos);
    this.store.dispatch(loadTodos());
  }

  deleteTodo(id: string): void {
    this.store.dispatch(deleteTodo({ id }));
  }
}
