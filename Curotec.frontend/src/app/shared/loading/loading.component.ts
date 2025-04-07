import { Component } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
    selector: 'app-loading-overlay',
    standalone: true,
    template: `
    <div class="overlay">
      <mat-spinner></mat-spinner>
    </div>
  `,
    styles: [`
    .overlay {
      position: fixed;
      top: 0; left: 0;
      width: 100vw;
      height: 100vh;
      background: rgba(0,0,0,0.5);
      display: flex;
      align-items: center;
      justify-content: center;
      z-index: 9999;
    }
  `],
    imports: [MatProgressSpinnerModule],
})
export class LoadingOverlayComponent { }
