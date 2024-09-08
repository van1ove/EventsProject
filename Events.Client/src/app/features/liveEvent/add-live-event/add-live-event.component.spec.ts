import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddLiveEventComponent } from './AddLiveEventComponent';

describe('AddLiveEventComponent', () => {
  let component: AddLiveEventComponent;
  let fixture: ComponentFixture<AddLiveEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddLiveEventComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddLiveEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
