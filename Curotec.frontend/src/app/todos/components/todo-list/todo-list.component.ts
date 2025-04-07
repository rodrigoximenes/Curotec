import { DatePipe, JsonPipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { ConfirmDialogComponent } from '../../../shared/confirm-dialog/confirm-dialog.component';
import { Todo } from '../../models/todo.model';
import { deleteTodo, loadTodos } from '../../store/todo.actions';
import { selectTodos } from '../../store/todo.selectors';
import { LoadingService } from '../../../shared/loading/loading.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  standalone: true,
  styleUrls: ['./todo-list.component.scss'],
  imports: [
    MatTableModule,
    MatButtonModule,
    DatePipe,
    MatCardModule,
    MatTooltipModule,
    MatIconModule
  ]
})
export class TodoListComponent implements OnInit, OnDestroy {
  todos$!: Observable<Todo[]>;
  displayedColumns: string[] = ['title', 'description', 'status', 'priority', 'createdAt', 'assignee', 'actions'];

  private unsubscribe$ = new Subject<void>();

  readonly statusLabels: { [key: number]: string } = {
    0: 'Pending',
    1: 'In Progress',
    2: 'Completed',
    3: 'Canceled'
  };

  readonly priorityLabels: { [key: number]: string } = {
    0: 'Low',
    1: 'Medium',
    2: 'High',
    3: 'Critical'
  };

  constructor(
    private store: Store,
    private router: Router,
    private dialog: MatDialog,
    private loadingService: LoadingService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadingService.show();
    this.todos$ = this.store.select(selectTodos).pipe(takeUntil(this.unsubscribe$));
    this.store.dispatch(loadTodos());
    this.loadingService.hide();
  }

  deleteTodo(id: string): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Confirm Deletion',
        message: 'Are you sure you want to delete this task?'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.store.dispatch(deleteTodo({ id }));
        this.snackBar.open('Task Deleted Successfully!', 'Close', { duration: 3000 });
      }
    });
  }

  goToNewTodo(): void {
    this.router.navigate(['/todos/new']);
  }

  editTodo(id: string): void {
    this.router.navigate(['/todos', id, 'edit']);
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
