import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLiveEventComponent } from './edit-live-event.component';

describe('EditLiveEventComponent', () => {
  let component: EditLiveEventComponent;
  let fixture: ComponentFixture<EditLiveEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditLiveEventComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditLiveEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
