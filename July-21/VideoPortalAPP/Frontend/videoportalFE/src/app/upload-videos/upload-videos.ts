import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UploadService } from '../Services/UploadService';

@Component({
  selector: 'app-upload-videos',
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './upload-videos.html',
  styleUrl: './upload-videos.css'
})
export class UploadVideos {
  FileUpload:FormGroup ;
  selectedFile: File | null = null;
  statusMessage:string="";

  constructor(private uploadservice:UploadService)
  {
    this.FileUpload=new FormGroup({
      Title: new FormControl(null, [Validators.required]),
      Description: new FormControl(null, [Validators.required]),
      file: new FormControl(null, [Validators.required]),
    })
  }

   onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  handleFormSubmisson()
  {
    console.log("form submitted")
    if (this.FileUpload.invalid ) {
      console.log("Invalid form or no file selected");
      return;
    }

    const formData = new FormData();
    formData.append('Title', this.FileUpload.get('Title')?.value);
    formData.append('Description', this.FileUpload.get('Description')?.value);
    if (this.selectedFile) {
      formData.append('File', this.selectedFile);
    }

    this.statusMessage="Uploading..."

    this.uploadservice.uploadVideo(formData).subscribe({
      next:(res)=>{
        console.log(res);
      },
      error:(err)=>{
        console.log(err);
        this.statusMessage="Error Occured.."
      },
      complete:()=>{
        console.log("uploaded")
        this.statusMessage = " Upload completed successfully.";
        this.FileUpload.reset();
        this.selectedFile = null;
      }
    })

  }
}
