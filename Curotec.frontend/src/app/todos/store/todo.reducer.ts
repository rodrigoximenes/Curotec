import { createReducer, on } from '@ngrx/store';
import { loadTodosSuccess, addTodo, updateTodo, deleteTodo } from './todo.actions';
import { Todo } from '../models/todo.model';

export interface TodoState {
    todos: Todo[];
}

export const initialState: TodoState = {
    todos: []
};

export const todoReducer = createReducer(
    initialState,
    on(loadTodosSuccess, (state, { todos }) => ({ ...state, todos })),
    on(addTodo, (state, { todo }) => ({ ...state, todos: [...state.todos, todo] })),
    on(updateTodo, (state, { todo }) => ({
        ...state,
        todos: state.todos.map(t => t.id === todo.id ? todo : t)
    })),
    on(deleteTodo, (state, { id }) => ({
        ...state,
        todos: state.todos.filter(t => t.id !== id)
    }))
);
