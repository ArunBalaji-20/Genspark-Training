import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from "./shared/navbar/navbar";
import { NotificationService } from './core/Services/notificationService';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{
  protected title = 'Bug-Tracking-Frontend';
  constructor(private notificationService:NotificationService)
  {}

  ngOnInit(): void {
    this.notificationService.startConnection();
  }
}
