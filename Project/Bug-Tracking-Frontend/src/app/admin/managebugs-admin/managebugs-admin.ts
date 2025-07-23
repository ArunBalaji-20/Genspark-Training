import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { devBugService } from '../../core/Services/devBugService';
import { NotificationService } from '../../core/Services/notificationService';

@Component({
  selector: 'app-managebugs',
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './managebugs-admin.html',
  styleUrl: './managebugs-admin.css'
})
export class ManagebugsAdmin {
  bugId:number=0;

  bugUpdateForm!: FormGroup;

  bug: BugStatusModel | null = null;

  domain:string="http://localhost:5258/screenshots/"


  statusMessage = '';
  errorMessage = '';

  constructor(private route:ActivatedRoute,private fb: FormBuilder,private devbugService:devBugService,private notificationService:NotificationService)
  {
  }


  // ngOnInit(): void {
  //   this.route.queryParams.subscribe(params => {
  //     this.bugId = params['id'] || null; 
  //     console.log('Param 1:', this.bugId);


  //   });
  //}

   ngOnInit() {
    // Initialize form
    this.bugUpdateForm = this.fb.group({
      Status: ["", Validators.required]
    });
    this.route.queryParams.subscribe(params => {
      this.bugId = params['id'] || null; 
      console.log('Param 1:', this.bugId);


    });

    // For demo: Load bug
    this.loadBugDetails(this.bugId);
  }

  loadBugDetails(id:number) {
    

    this.devbugService.getBugStatusAPI(id).subscribe({
      next:(data)=>{
        console.log(data)
        this.bug=(data.body as any)??[];
        console.log(this.bug)

        this.bugUpdateForm.patchValue({
  Status: this.bug && (this.bug.status === 'Resolved' || this.bug.status === 'InProgress')
    ? this.bug.status
    : ''
});
      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log("completed fetching single bug")
      }
    })

    
    
  }
  

  getScreenshotUrl(): string {
    if (!this.bug?.screenshot) return '';
    const parts = this.bug.screenshot.split('/');
    const filename = parts[parts.length - 1];
    return this.domain + filename;
}

  handleStatusUpdate() {
    const newStatus = this.bugUpdateForm.value.Status;

    console.log('Updating bug to status:', newStatus);

    if(newStatus=="Resolved")
    {
      this.devbugService.patchBugStatusResolveAdminAPI(this.bugId).subscribe({
        next:(data)=>{
          
          console.log(data)
          console.log("patched")
          this.statusMessage = 'Status updated successfully!';
          window.alert(this.statusMessage)
          this.notificationService.updateNotifications([`${this.bug?.bugName} is Resolved.`]);
        },
        error:(err)=>{
          console.log(err.error.detail)
          // this.statusMessage=err.error.detail
          this.errorMessage = err.error.detail;
        },
        complete:()=>{
          console.log("patch completed")
        }
      })
    }

    if(newStatus=="InProgress")
    {
      console.log("innnnnn")
      this.devbugService.patchBugStatusInProgressAPI(this.bugId).subscribe({
        next:(data)=>{
          console.log(data);
           console.log("patched")
          this.statusMessage = 'Status updated successfully!';
          window.alert(this.statusMessage)

        },
        error:(err)=>{
          console.log(err);
          console.log(err.error.detail)
          this.errorMessage = err.error.detail;
        },
        complete:()=>{
          console.log("completed patch inprogress")
        }
      })

    }
   
   

    // this.statusMessage = '';
    // this.errorMessage = 'Error occurred while updating status!';
  }
}


 