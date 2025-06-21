import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Resolve } from './resolve';

describe('Resolve', () => {
  let component: Resolve;
  let fixture: ComponentFixture<Resolve>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Resolve]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Resolve);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
