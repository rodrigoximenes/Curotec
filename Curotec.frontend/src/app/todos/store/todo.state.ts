import { Todo } from "../models/todo.model";

export interface TodoState {
    todos: Todo[];
}

export const initialTodoState: TodoState = {
    todos: []
};
