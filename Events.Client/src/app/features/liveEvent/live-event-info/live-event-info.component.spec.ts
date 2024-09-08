import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveEventInfoComponent } from './live-event-info.component';

describe('LiveEventInfoComponent', () => {
  let component: LiveEventInfoComponent;
  let fixture: ComponentFixture<LiveEventInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiveEventInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiveEventInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
