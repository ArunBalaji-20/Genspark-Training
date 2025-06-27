import { Component, OnInit } from '@angular/core';
import { BugCommentModel } from '../../core/Models/BugCommentModel';
import { bugCommentService } from '../../core/Services/bugCommentService';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { authGuard } from '../../auth-guard';
import { authService } from '../../core/Services/authService';

@Component({
  selector: 'app-chat',
  imports: [CommonModule,FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css'
})
export class Chat implements OnInit {
    comments:BugCommentModel[]=[];
    bugid:number=0;
    newComment:string="";

    constructor(private bugCommentService:bugCommentService,private router:ActivatedRoute,public authService:authService)
    {

    }
  ngOnInit(): void {
    this.router.queryParams.subscribe(params=>{
      this.bugid=params['id'] || null;
      console.log(this.bugid)
    })
   this.loadComments();
  }
  loadComments()
  {
     this.bugCommentService.getAllCommentsAPI(this.bugid).subscribe({
      next:(data)=>{
        console.log(data)
        this.comments=(data.body as any).$values ?? [];
        console.log(this.comments)
      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log('finished getting comments')
      }
    })
  }

  handleSend()
  {
    const data={
            "comment":this.newComment,
            "bugId":this.bugid
        }

      this.bugCommentService.postComment(data).subscribe({
        next:(data)=>{
          console.log(data)
          this.loadComments()
        },
        error:(err)=>{
          console.log(err)
        }
        
      })
  }
}