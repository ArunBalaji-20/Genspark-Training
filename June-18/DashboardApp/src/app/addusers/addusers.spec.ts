import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Addusers } from './addusers';

describe('Addusers', () => {
  let component: Addusers;
  let fixture: ComponentFixture<Addusers>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Addusers]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Addusers);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
