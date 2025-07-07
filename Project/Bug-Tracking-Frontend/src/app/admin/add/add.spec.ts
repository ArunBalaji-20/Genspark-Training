import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Add } from './add';
import { empManageService } from '../../core/Services/empManageService';

describe('Add', () => {
  let component: Add;
  let fixture: ComponentFixture<Add>;
  let empManageMock:any;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Add],
      providers:[{ provide: empManageService , useValue: empManageMock }]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Add);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
