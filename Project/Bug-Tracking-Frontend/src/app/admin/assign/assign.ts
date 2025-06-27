import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { ActivatedRoute } from '@angular/router';
import { devBugService } from '../../core/Services/devBugService';
import { AvailableDevsResponseModel } from '../../core/Models/AvailableDevsResponseModel';
import { CommonModule } from '@angular/common';
import { bugService } from '../../core/Services/bugService';

@Component({
  selector: 'app-assign',
  imports: [FormsModule,ReactiveFormsModule,CommonModule],
  templateUrl: './assign.html',
  styleUrl: './assign.css'
})
export class Assign implements OnInit {
  bugId:number=0;
  bugAssignForm!: FormGroup;
  bug: BugStatusModel | null = null;
   availableDevs: AvailableDevsResponseModel[] = [];
  domain:string="http://localhost:5258/screenshots/"


  statusMessage = '';
  errorMessage = '';

  constructor(private route:ActivatedRoute,private fb: FormBuilder,private devbugService:devBugService,private bugService:bugService)
  {
  }
    getScreenshotUrl(): string {
    if (!this.bug?.screenshot) return '';
    const parts = this.bug.screenshot.split('/');
    const filename = parts[parts.length - 1];
    return this.domain + filename;
}

   ngOnInit() {
    // Initialize form
   
    this.route.queryParams.subscribe(params => {
      this.bugId = params['id'] || null; 
      console.log('Param 1:', this.bugId);


    });

    // For demo: Load bug
    this.loadBugDetails(this.bugId);
    this.loadAvailableDevs();
    this.bugAssignForm = this.fb.group({
  Status: ["", Validators.required],
  AssignedDevId: ["", Validators.required]
});
  }

  loadBugDetails(id:number) {
    

    this.devbugService.getBugStatusAPI(id).subscribe({
      next:(data)=>{
        console.log(data)
        this.bug=(data.body as any)??[];
        console.log(this.bug)

      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log("completed fetching single bug")
      }
    })

  }

  loadAvailableDevs() {
    this.devbugService.getAvailableDevsAPI().subscribe({
      next: (data: any) => {
        this.availableDevs = data.body?.$values ?? [];
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  handleAssignment()
  {
   const selectedDevId = this.bugAssignForm.value.AssignedDevId;

   const data={
  "bugId": `${this.bugId}`,
  "devId": `${selectedDevId}`
};
    
    this.bugService.assignBugToDevAPI(data).subscribe({
      next:(res)=>{
        console.log(res)
        this.statusMessage="Bug Assigned To Developer Successfully.."
      },
      error:(err)=>{
        console.log(err.error.detail)
        this.errorMessage=err.error.detail
      }
    })
  }

}


 

  



  


  


  // ngOnInit(): void {
  //   this.route.queryParams.subscribe(params => {
  //     this.bugId = params['id'] || null; 
  //     console.log('Param 1:', this.bugId);


  //   });
  //}


  
  



