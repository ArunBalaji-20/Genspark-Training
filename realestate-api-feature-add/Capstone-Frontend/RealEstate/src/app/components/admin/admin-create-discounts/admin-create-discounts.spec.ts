import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCreateDiscounts } from './admin-create-discounts';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ActivatedRoute } from '@angular/router';

describe('AdminCreateDiscounts', () => {
  let component: AdminCreateDiscounts;
  let fixture: ComponentFixture<AdminCreateDiscounts>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminCreateDiscounts,HttpClientTestingModule],
      providers: [
        { provide: ActivatedRoute, useValue: {} }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminCreateDiscounts);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
