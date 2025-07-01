// import { Component, OnInit, signal } from '@angular/core';
// import { BugStatusModel } from '../../core/Models/BugStatusModel';
// import { bugService } from '../../core/Services/bugService';
// import { Status } from '../status/status';

// @Component({
//   selector: 'app-all-bugs-status',
//   imports: [Status],
//   templateUrl: './all-bugs-status.html',
//   styleUrl: './all-bugs-status.css'
// })
// export class AllBugsStatus implements OnInit {

//     bugs=signal<BugStatusModel[]>([]);

//     constructor(private bugService:bugService)
//     {}

//     ngOnInit(): void {
//       this.bugService.getAllBugStatusAPI().subscribe({
//         next:(data:any)=>{
//           this.bugs.set(data.body.$values as BugStatusModel[]);
//           console.log('in next')
//           console.log(data)
//         },
//         error:(err)=>{
//           console.log(err);
//         },
//         complete:()=>{
//           console.log("finished get bug status")
//         }

//       })
//     }


// }

import { Component, OnInit, signal, computed } from '@angular/core';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { bugService } from '../../core/Services/bugService';
import { Status } from '../status/status';
import { FormsModule } from '@angular/forms';
import { BugAssignmentModel } from '../../core/Models/BugAssignmentModel';

@Component({
  selector: 'app-all-bugs-status',
  imports: [Status, FormsModule],
  templateUrl: './all-bugs-status.html',
  styleUrl: './all-bugs-status.css'
})
export class AllBugsStatus implements OnInit {

    bugs = signal<BugStatusModel[]>([]);
    assignments=signal<BugAssignmentModel[]>([]);
    searchTerm = signal<string>('');
    statusFilter = signal<string>('');

    filteredBugs = computed(() => {
      let list = this.bugs();
      const search = this.searchTerm().toLowerCase();
      const status = this.statusFilter();

      if (search) {
        list = list.filter(
          bug =>
            bug.bugName?.toLowerCase().includes(search) ||
            bug.description?.toLowerCase().includes(search)
        );
      }
      if (status) {
        list = list.filter(bug => bug.status === status);
      }
      return list;
    });

    constructor(private bugService: bugService) {}

    ngOnInit(): void {
      this.bugService.getAllBugStatusAPI().subscribe({
        next: (data: any) => {
          this.bugs.set(data.body.$values as BugStatusModel[]);
        },
        error: (err) => {
          console.log(err);
        }
      });
     this.bugService.getAllBugsAssignedListAPI().subscribe({
        next:(data:any)=>{
          console.log(data)
          this.assignments.set(data.body.$values as BugAssignmentModel[]);
        },
        error:(err)=>{
          console.log(err)
        },
        complete:()=>{
          console.log("completed fetching assigned list api")
        }
      })
     
    }

    
}