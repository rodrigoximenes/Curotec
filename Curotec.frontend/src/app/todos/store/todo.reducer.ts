import { createReducer, on } from '@ngrx/store';
import { Todo } from '../models/todo.model';
import * as TodoActions from './todo.actions';

export interface TodoState {
    todos: Todo[];
    loading: boolean;
    error: any;
}

export const initialState: TodoState = {
    todos: [],
    loading: false,
    error: null
};

export const todoReducer = createReducer(
    initialState,

    // Load Todos
    on(TodoActions.loadTodos, (state) => ({
        ...state,
        loading: true,
        error: null
    })),
    on(TodoActions.loadTodosSuccess, (state, { todos }) => ({
        ...state,
        todos,
        loading: false
    })),
    on(TodoActions.loadTodosFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error
    })),

    // Create Todo
    on(TodoActions.createTodoSuccess, (state, { todo }) => {
        if (!todo || !todo.id) {
            console.warn('Invalid todo on createTodoSuccess:', todo);
            return state;
        }

        return {
            ...state,
            todos: [...state.todos, todo]
        };
    }),
    on(TodoActions.createTodoFailure, (state, { error }) => ({
        ...state,
        error
    })),

    // Update Todo
    on(TodoActions.updateTodoSuccess, (state, { todo }) => {
        if (!todo || !todo.id) {
            console.warn('Invalid todo on updateTodoSuccess:', todo);
            return state;
        }

        return {
            ...state,
            todos: state.todos.map(t => (t.id === todo.id ? todo : t))
        };
    }),
    on(TodoActions.updateTodoFailure, (state, { error }) => ({
        ...state,
        error
    })),

    // Delete Todo
    on(TodoActions.deleteTodoSuccess, (state, { id }) => ({
        ...state,
        todos: state.todos.filter(t => t.id !== id)
    })),
    on(TodoActions.deleteTodoFailure, (state, { error }) => ({
        ...state,
        error
    }))
);
