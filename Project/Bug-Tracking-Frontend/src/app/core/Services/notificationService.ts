import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class NotificationService {
  private hubConnection!: signalR.HubConnection;
  private notificationsSubject = new BehaviorSubject<string[]>([]);
  public notifications$ = this.notificationsSubject.asObservable();
  private notifications: string[] = [];

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5258/notificationHub', {
        withCredentials: true
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR Connected'))
      .catch(err => console.error('SignalR Error:', err));

    this.hubConnection.on('ReceiveNotification', (message: string) => {
      const time = new Date().toLocaleTimeString();
      this.notifications.unshift(`${message} (at ${time})`);
      this.notificationsSubject.next([...this.notifications]);
    });
  }

  getNotifications(): string[] {
    return this.notifications;
  }

  updateNotifications(notifications: string[]): void {
  this.notifications = notifications;
  this.notificationsSubject.next([...this.notifications]);
}

}
