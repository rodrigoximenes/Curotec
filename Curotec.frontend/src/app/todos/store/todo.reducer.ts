import { createReducer, on } from '@ngrx/store';
import { Todo } from '../models/todo.model';
import * as TodoActions from './todo.actions';

export interface TodoState {
    todos: Todo[];
    error: any;
}

export const initialState: TodoState = {
    todos: [],
    error: null
};

export const todoReducer = createReducer(
    initialState,

    // Load
    on(TodoActions.loadTodosSuccess, (state, { todos }) => ({
        ...state,
        todos
    })),
    on(TodoActions.loadTodosFailure, (state, { error }) => ({
        ...state,
        error
    })),

    // Create
    on(TodoActions.createTodoSuccess, (state, { todo }) => ({
        ...state,
        todos: [...state.todos, todo]
    })),
    on(TodoActions.createTodoFailure, (state, { error }) => ({
        ...state,
        error
    })),

    // Update
    on(TodoActions.updateTodoSuccess, (state, { todo }) => ({
        ...state,
        todos: state.todos.map(t => (t.id === todo.id ? todo : t))
    })),
    on(TodoActions.updateTodoFailure, (state, { error }) => ({
        ...state,
        error
    })),

    // Delete
    on(TodoActions.deleteTodoSuccess, (state, { id }) => ({
        ...state,
        todos: state.todos.filter(t => t.id !== id)
    })),
    on(TodoActions.deleteTodoFailure, (state, { error }) => ({
        ...state,
        error
    }))
);
