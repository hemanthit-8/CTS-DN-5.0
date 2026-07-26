import { TestBed } from '@angular/core/testing';

import { NotificationService } from './notification';

describe('NotificationService', () => {
  let service: NotificationService;

  beforeEach(() => {
    // NotificationService is intentionally NOT providedIn: 'root' (see Hands-On
    // 6, Task 2) — it must be provided explicitly here, just like it is at the
    // component level in NotificationComponent.
    TestBed.configureTestingModule({
      providers: [NotificationService],
    });
    service = TestBed.inject(NotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
