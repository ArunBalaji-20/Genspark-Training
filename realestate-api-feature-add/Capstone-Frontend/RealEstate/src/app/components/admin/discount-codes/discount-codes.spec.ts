import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiscountCodes } from './discount-codes';
import { HttpClientTestingModule, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpClient } from '@microsoft/signalr';
import { ActivatedRoute } from '@angular/router';

describe('DiscountCodes', () => {
  let component: DiscountCodes;
  let fixture: ComponentFixture<DiscountCodes>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DiscountCodes,HttpClientTestingModule],
      providers: [
        { provide: ActivatedRoute, useValue: {} }
      ]
      
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiscountCodes);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

   it('should initialize listings and paginationInfo', () => {
    expect(component.listings).toBeDefined();
    expect(component.paginationInfo).toBeDefined();
    expect(component.paginationInfo.pageSize).toBe(10);
  });

   it('should call deleteListing and set error on failure', () => {
    const spy = spyOn(window, 'confirm').and.returnValue(true);
    const discountServiceSpy = spyOn(component['discountService'], 'DeleteCode').and.returnValue({
      subscribe: (handlers: any) => handlers.error('error')
    } as any);

    component.deleteListing('test-id');
    expect(component.error).toBe('Failed to delete Code. Please try again.');
    expect(discountServiceSpy).toHaveBeenCalledWith('test-id');
    spy.and.callThrough();
  });

  it('should call patchExpire and set error on failure', () => {
    const spy = spyOn(window, 'confirm').and.returnValue(true);
    const discountServiceSpy = spyOn(component['discountService'], 'PatchExpiryStatus').and.returnValue({
      subscribe: (handlers: any) => handlers.error('error')
    } as any);

    component.patchExpire('test-id');
    expect(component.error).toBe('Failed to update status . Please try again.');
    expect(discountServiceSpy).toHaveBeenCalledWith('test-id');
    spy.and.callThrough();
  });

   it('should not call deleteListing if confirm is false', () => {
    const spy = spyOn(window, 'confirm').and.returnValue(false);
    const discountServiceSpy = spyOn(component['discountService'], 'DeleteCode');
    component.deleteListing('test-id');
    expect(discountServiceSpy).not.toHaveBeenCalled();
    spy.and.callThrough();
  });

  it('should not call patchExpire if confirm is false', () => {
    const spy = spyOn(window, 'confirm').and.returnValue(false);
    const discountServiceSpy = spyOn(component['discountService'], 'PatchExpiryStatus');
    component.patchExpire('test-id');
    expect(discountServiceSpy).not.toHaveBeenCalled();
    spy.and.callThrough();
  });
});


