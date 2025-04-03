import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { TodoService } from '../services/todo.service';
import { loadTodos, loadTodosSuccess, loadTodosFailure } from './todo.actions';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable()
export class TodoEffects {
    private actions$ = inject(Actions);
    private todoService = inject(TodoService);
    private store = inject(Store);

    loadTodos$ = createEffect(() =>
        this.actions$.pipe(
            ofType(loadTodos),
            mergeMap(() =>
                this.todoService.getTodos().pipe(
                    map((todos) => loadTodosSuccess({ todos })),
                    catchError((error) => of(loadTodosFailure({ error })))
                )
            )
        )
    );
}
