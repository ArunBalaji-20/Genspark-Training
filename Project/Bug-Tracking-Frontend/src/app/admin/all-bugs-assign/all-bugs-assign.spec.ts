import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllBugsAssign } from './all-bugs-assign';

describe('AllBugsAssign', () => {
  let component: AllBugsAssign;
  let fixture: ComponentFixture<AllBugsAssign>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AllBugsAssign]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllBugsAssign);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
