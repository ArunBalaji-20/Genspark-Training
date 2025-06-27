import { Component, Input } from '@angular/core';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-status',
  imports: [CommonModule,FormsModule],
  templateUrl: './status.html',
  styleUrl: './status.css'
})
export class Status {
  @Input() bug:BugStatusModel|null =new BugStatusModel();
  domain:string="http://localhost:5258/screenshots/"
  constructor()
  {

  }

  getScreenshotUrl(): string {
    if (!this.bug?.screenshot) return '';
    const parts = this.bug.screenshot.split('/');
    const filename = parts[parts.length - 1];
    return this.domain + filename;
}

getStatusClass(): string {
    switch (this.bug?.status) {
        case 'Submitted':
            return 'bg-danger text-white'; // red card-submitted alert
        case 'InProgress':
            return 'bg-warning text-dark'; // yellow card -inprogress
        case 'Assigned':
            return 'bg-primary text-white'; // blue assigned

        case 'Resolved':
            return 'bg-success text-white'; // green card - fixed
        default:
            return 'bg-light'; // default light grey
    }
}


}
