import { Component } from '@angular/core';

@Component({
  selector: 'app-customer-details',
  imports: [],
  templateUrl: './customer-details.html',
  styleUrl: './customer-details.css'
})
export class CustomerDetails {
  name:string;
  LikeThumbsclassName:string="bi bi-hand-thumbs-up";
  DislikeThumbsClassName:string="bi bi-hand-thumbs-down";
  like:boolean=false;
  dislike:boolean=false;

  likes:number;
  dislikes:number;
  constructor()
  {
    this.name="";
    this.likes=0;
    this.dislikes=0;
  }

  OnClick(uname:string)
  {
    this.name=uname;
  }


  toggleLikeThumbs()
  {
    this.like = !this.like;

    if(this.like)
    {
      this.LikeThumbsclassName="bi bi-hand-thumbs-up-fill";
       this.DislikeThumbsClassName="bi bi-hand-thumbs-down";
       this.likes+=1;
    }
    else{
      this.LikeThumbsclassName="bi bi-hand-thumbs-up";
    }
  }

  toggleDislikeThumbs()
  {
    this.dislike = !this.dislike;

    if(this.dislike)
    {
      this.DislikeThumbsClassName="bi bi-hand-thumbs-down-fill";
      this.LikeThumbsclassName="bi bi-hand-thumbs-up";
      this.dislikes+=1;
    }
    else{
      this.DislikeThumbsClassName="bi bi-hand-thumbs-down";
    }
  }
  

}
