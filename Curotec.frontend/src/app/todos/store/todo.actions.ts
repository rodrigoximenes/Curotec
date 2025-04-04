import { createAction, props } from '@ngrx/store';
import { Todo } from '../models/todo.model';

// Load
export const loadTodos = createAction('[Todo] Load Todos');
export const loadTodosSuccess = createAction('[Todo] Load Todos Success', props<{ todos: Todo[] }>());
export const loadTodosFailure = createAction('[Todo] Load Todos Failure', props<{ error: any }>());

// Create
export const createTodo = createAction('[Todo] Create Todo', props<{ todo: Omit<Todo, 'id'> }>());
export const createTodoSuccess = createAction('[Todo] Create Todo Success', props<{ todo: Todo }>());
export const createTodoFailure = createAction('[Todo] Create Todo Failure', props<{ error: any }>());

// Update
export const updateTodo = createAction('[Todo] Update Todo', props<{ id: string; changes: Partial<Todo> }>());
export const updateTodoSuccess = createAction('[Todo] Update Todo Success', props<{ todo: Todo }>());
export const updateTodoFailure = createAction('[Todo] Update Todo Failure', props<{ error: any }>());

// Delete
export const deleteTodo = createAction('[Todo] Delete Todo', props<{ id: string }>());
export const deleteTodoSuccess = createAction('[Todo] Delete Todo Success', props<{ id: string }>());
export const deleteTodoFailure = createAction('[Todo] Delete Todo Failure', props<{ error: any }>());
