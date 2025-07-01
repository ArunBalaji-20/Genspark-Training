import { Component, Input, OnInit, signal } from '@angular/core';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {  Router } from '@angular/router';
import { BugAssignmentModel } from '../../core/Models/BugAssignmentModel';

@Component({
  selector: 'app-status',
  imports: [CommonModule,FormsModule],
  templateUrl: './status.html',
  styleUrl: './status.css'
})
export class Status  {
  @Input() bug:BugStatusModel|null =new BugStatusModel();
  @Input() assignmentList:BugAssignmentModel[]=[];
    // @Input() assignmentList: BugAssignmentModel[] = [];
  
  domain:string="http://localhost:5258/screenshots/"
  constructor(private router:Router)
  {
     
  }
  

  getScreenshotUrl(): string {
    if (!this.bug?.screenshot) return '';
    const parts = this.bug.screenshot.split('/');
    const filename = parts[parts.length - 1];
    return this.domain + filename;
}
getAssignedDevName(): string | null {
  if ( this.assignmentList) {
    const assignment = this.assignmentList.find(a => a.bugId === this.bug?.bugId);
    return assignment ? assignment.devName : null;
  }
  // this.bug?.status === 'Assigned' &&
  return null;
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
handleChatRedirect(id:number)
{
    this.router.navigate(['/chat'],{queryParams:{id:`${id}`}})
    console.log("redirected to chat")
}

}
