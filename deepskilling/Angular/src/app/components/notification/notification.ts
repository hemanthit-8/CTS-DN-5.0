import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationService } from '../../services/notification';

/**
 * NotificationComponent — Hands-On 6, Task 2: provides NotificationService at
 * the component level (`providers: [NotificationService]`) instead of relying
 * on the app-wide 'root' singleton. That gives this component (and its children)
 * their own isolated instance, so multiple NotificationComponents on a page
 * each keep their own message queue instead of sharing one.
 */
@Component({
  selector: 'app-notification',
  imports: [CommonModule],
  providers: [NotificationService],
  templateUrl: './notification.html',
  styleUrl: './notification.css',
})
export class Notification {
  constructor(public notificationService: NotificationService) {}
}
