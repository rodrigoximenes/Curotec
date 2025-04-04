import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { filter, take } from 'rxjs/operators';
import { TaskPriorityEnum, TaskStatusEnum, Todo } from '../../models/todo.model';
import { createTodo, updateTodo } from '../../store/todo.actions';
import { selectTodoById } from '../../store/todo.selectors';

@Component({
  selector: 'app-todo-form',
  templateUrl: './todo-form.component.html',
  styleUrls: ['./todo-form.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
})
export class TodoFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private store = inject(Store);
  private route = inject(ActivatedRoute);
  public router = inject(Router);
  private snackBar = inject(MatSnackBar);

  form!: FormGroup;
  isEdit = false;
  id!: string;

  priorities = Object.keys(TaskPriorityEnum)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({ key: Number(key), value: TaskPriorityEnum[key as any] }));

  statuses = Object.keys(TaskStatusEnum)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({ key: Number(key), value: TaskStatusEnum[key as any] }));

  errorMessages: { [key: string]: string } = {
    title: 'Title is required.',
    status: 'Status is required.',
    priority: 'Priority is required.',
    assignee: 'Assignee is required.',
  };

  ngOnInit(): void {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      status: [TaskStatusEnum.Pending, Validators.required],
      priority: [TaskPriorityEnum.Medium, Validators.required],
      creationDate: [new Date().toISOString().substring(0, 10), Validators.required],
      completionDate: [null],
      assignee: ['', Validators.required]
    });

    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEdit = true;
        this.id = id;

        this.store.select(selectTodoById(id)).pipe(
          filter(todo => !!todo),
          take(1)
        ).subscribe(todo => {
          if (todo) {
            this.form.patchValue(todo);
          }
        });
      }
    });
  }

  getErrorMessage(controlName: string): string | null {
    const control = this.form.get(controlName);
    if (control?.hasError('required')) {
      return this.errorMessages[controlName] || 'This field is required.';
    }
    return null;
  }

  save(): void {
    if (this.form.invalid) return;

    const formValue = this.form.value;

    const todo: Todo = {
      title: formValue.title,
      description: formValue.description,
      priority: formValue.priority,
      status: formValue.status,
      assignee: formValue.assignee,
      creationDate: new Date(),
      id: this.isEdit ? this.id : crypto.randomUUID()
    };

    if (this.isEdit) {
      this.store.dispatch(updateTodo({ id: todo.id, changes: todo }));
    } else {
      this.store.dispatch(createTodo({ todo }));
    }

    this.snackBar.open('Task Saved Successfully!', 'Close', { duration: 3000 });
    this.router.navigate(['/todos']);
  }
}