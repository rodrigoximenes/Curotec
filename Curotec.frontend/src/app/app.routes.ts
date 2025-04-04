import { Routes } from '@angular/router';
import { TodoListComponent } from './todos/components/todo-list/todo-list.component';
import { TodoFormComponent } from './todos/components/todo-form/todo-form.component';

export const routes: Routes = [
    { path: '', component: TodoListComponent },
    { path: 'todos', component: TodoListComponent },
    {
        path: 'todos/new',
        component: TodoFormComponent
    },
    {
        path: 'todos/:id/edit',
        component: TodoFormComponent
    }
];
