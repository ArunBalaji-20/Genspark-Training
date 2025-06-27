import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllBugsStatus } from './all-bugs-status';

describe('AllBugsStatus', () => {
  let component: AllBugsStatus;
  let fixture: ComponentFixture<AllBugsStatus>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AllBugsStatus]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllBugsStatus);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
