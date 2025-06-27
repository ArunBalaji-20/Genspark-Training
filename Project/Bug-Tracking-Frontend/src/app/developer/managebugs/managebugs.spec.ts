import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Managebugs } from './managebugs';

describe('Managebugs', () => {
  let component: Managebugs;
  let fixture: ComponentFixture<Managebugs>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Managebugs]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Managebugs);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
