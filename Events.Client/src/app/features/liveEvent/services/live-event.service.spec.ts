import { TestBed } from '@angular/core/testing';

import { LiveEventService } from './live-event.service';

describe('LiveEventService', () => {
  let service: LiveEventService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LiveEventService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
