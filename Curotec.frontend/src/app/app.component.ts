import { Component } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterOutlet } from '@angular/router';
import { LoadingOverlayComponent } from './shared/loading/loading.component';
import { LoadingService } from './shared/loading/loading.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoadingOverlayComponent, NgIf, MatProgressSpinnerModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  isLoading = false;
  title = 'curotec.frontend';

  constructor(private loadingService: LoadingService) {
    this.loadingService.loading$.subscribe(isLoading => {
      this.isLoading = isLoading;
    });
  }
}
