import { createFeatureSelector, createSelector } from '@ngrx/store';
import { TodoState } from './todo.reducer';

export const selectTodoState = createFeatureSelector<TodoState>('todos');

export const selectTodos = createSelector(
    selectTodoState,
    (state) => state.todos
);


export const selectTodoById = (id: string) =>
    createSelector(
        selectTodos,
        (todos) => todos.find(todo => todo.id === id)
    );