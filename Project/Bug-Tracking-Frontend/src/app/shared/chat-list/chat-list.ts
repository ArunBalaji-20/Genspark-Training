import { Component, OnInit } from '@angular/core';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { bugService } from '../../core/Services/bugService';

@Component({
  selector: 'app-chat-list',
  imports: [CommonModule],
  templateUrl: './chat-list.html',
  styleUrl: './chat-list.css'
})
export class ChatList implements OnInit {

  isFetching:Boolean=false;
  mybugs:BugStatusModel[]=[];
  constructor(private bugService:bugService,private router:Router)
  {}
  ngOnInit(): void {
    this.bugService.getAllBugStatusAPI().subscribe({
      next:(data)=>{
        console.log(data)
        this.mybugs=(data.body as any)?.$values ??[];
        
        // this.employees = (res.body as any)?.$values ?? [];
        console.log("from ngonint chatlist bugs")
      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log('completed api chatlist mybugs')
      }
    })

  }

  handleChat(id:number)
  {
    console.log(id);
     this.router.navigate(['/chat'],{queryParams:{id:`${id}`}})
    console.log("redirected to chat")
  }
 
}
