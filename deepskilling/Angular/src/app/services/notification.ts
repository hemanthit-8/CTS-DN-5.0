import { Injectable } from '@angular/core';

/**
 * NotificationService — Hands-On 6, Task 2: intentionally provided at the
 * COMPONENT level (see NotificationComponent) rather than 'root'. Providing it
 * at the component level creates a brand-new instance scoped to that component
 * and its children, instead of the app-wide singleton you'd get from providedIn: 'root'.
 * That's useful when each usage needs isolated state (e.g. per-widget notification queues).
 */
@Injectable()
export class NotificationService {
  private messages: string[] = [];

  push(message: string): void {
    this.messages.push(message);
  }

  getMessages(): string[] {
    return this.messages;
  }

  clear(): void {
    this.messages = [];
  }
}
