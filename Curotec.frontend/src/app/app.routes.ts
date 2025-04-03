import { Routes } from '@angular/router';
import { TodoListComponent } from './todos/components/todo-list/todo-list.component';

export const routes: Routes = [
    { path: '', component: TodoListComponent },
    { path: 'todos', component: TodoListComponent },
];
