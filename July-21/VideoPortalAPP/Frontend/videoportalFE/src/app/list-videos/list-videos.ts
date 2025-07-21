import { Component, OnInit } from '@angular/core';
import { TrainingVideos } from '../Models/TrainingVideos';
import { UploadService } from '../Services/UploadService';
import { pipe } from 'rxjs';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-list-videos',
  imports: [DatePipe,CommonModule],
  templateUrl: './list-videos.html',
  styleUrl: './list-videos.css'
})
export class ListVideos implements OnInit {
  portalVideos:TrainingVideos[]=[];
  status:string="";
  constructor(private uploadservice:UploadService )
  {

  }

  ngOnInit(): void {

    this.uploadservice.getlist().subscribe({
      next:(res)=>{
        // console.log(res)
        this.portalVideos=res.body as any;
        console.log(this.portalVideos)
      },
      error:(err)=>{
        console.log(err);
        this.status="Error occured"
      },
      complete:()=>{
        console.log("api finished ...")
      }
    })

  }


}
