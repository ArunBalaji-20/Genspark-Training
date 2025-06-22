import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { BugReportModel } from '../../core/Models/BugReportModel';
import { bugService } from '../../core/Services/bugService';

@Component({
  selector: 'app-report',
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './report.html',
  styleUrl: './report.css'
})
export class Report {
bugReportForm: FormGroup;
bug:BugReportModel = new BugReportModel();
selectedFile: File | null = null;
statusMessage:Boolean=false;
errorMessage:Boolean=false;

constructor(private bugService:bugService) {
  this.bugReportForm = new FormGroup({
    BugName: new FormControl(null, [Validators.required]),
    Description: new FormControl(null, [Validators.required]),
    Screenshot: new FormControl(null, []),
    CvssScore: new FormControl(null, [Validators.required])
  });

  
}
  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }
  updateCvssScoreDisplay(event: any) {
  const value = event.target.value;
  this.bugReportForm.get('CvssScore')?.setValue(value);
}



  handleBugReportSubmission()
  {
   

   if (this.bugReportForm.invalid ) {
      console.log("Invalid form or no file selected");
      return;
    }

    const formData = new FormData();
    formData.append('BugName', this.bugReportForm.get('BugName')?.value);
    formData.append('Description', this.bugReportForm.get('Description')?.value);
    formData.append('CvssScore', this.bugReportForm.get('CvssScore')?.value);
    if (this.selectedFile) {
      formData.append('Screenshot', this.selectedFile);
    }

    this.bugService.submitBugReportAPI(formData).subscribe({
      next: (res) => {
        console.log('Bug sent', res);
        if(res.status==201)
        {
          this.statusMessage=true;
        }
      },
      error: (err) => {
        console.error('Error', err);
        this.errorMessage=true;
      },
      complete: () => {
        console.log('Completed');
      }
    });

    // Reset form
    this.bugReportForm.reset();
    this.selectedFile = null;
    
    
    
  }
}
