import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveEventListComponent } from './live-event-list.component';

describe('LiveEventListComponent', () => {
  let component: LiveEventListComponent;
  let fixture: ComponentFixture<LiveEventListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiveEventListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiveEventListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
