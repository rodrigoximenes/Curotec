import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TodoService } from '../services/todo.service';
import * as TodoActions from './todo.actions';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable()
export class TodoEffects {
    private actions$ = inject(Actions);
    private todoService = inject(TodoService);

    loadTodos$ = createEffect(() =>
        this.actions$.pipe(
            ofType(TodoActions.loadTodos),
            mergeMap(() =>
                this.todoService.getTodos().pipe(
                    map(todos => TodoActions.loadTodosSuccess({ todos })),
                    catchError(error => of(TodoActions.loadTodosFailure({ error })))
                )
            )
        )
    );

    createTodo$ = createEffect(() =>
        this.actions$.pipe(
            ofType(TodoActions.createTodo),
            mergeMap(({ todo }) =>
                this.todoService.create(todo).pipe(
                    map(created => TodoActions.createTodoSuccess({ todo: created })),
                    catchError(error => of(TodoActions.createTodoFailure({ error })))
                )
            )
        )
    );

    updateTodo$ = createEffect(() =>
        this.actions$.pipe(
            ofType(TodoActions.updateTodo),
            mergeMap(({ id, changes }) =>
                this.todoService.update(id, changes).pipe(
                    map(updated => TodoActions.updateTodoSuccess({ todo: updated })),
                    catchError(error => of(TodoActions.updateTodoFailure({ error })))
                )
            )
        )
    );

    deleteTodo$ = createEffect(() =>
        this.actions$.pipe(
            ofType(TodoActions.deleteTodo),
            mergeMap(({ id }) =>
                this.todoService.delete(id).pipe(
                    map(() => TodoActions.deleteTodoSuccess({ id })),
                    catchError(error => of(TodoActions.deleteTodoFailure({ error })))
                )
            )
        )
    );
}
