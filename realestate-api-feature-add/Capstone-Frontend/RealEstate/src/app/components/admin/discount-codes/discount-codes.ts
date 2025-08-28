import { Component } from '@angular/core';
import { PropertyListing, PropertyListingResponseDto } from '../../../models/property-listing.model';
import { BehaviorSubject, debounceTime, distinctUntilChanged, switchMap } from 'rxjs';
import { PagedResult } from '../../../models/paged-result.model';
import { PropertyListingQueryParametersDto } from '../../../models/property-listing-query-parameters.dto';
import { PropertyListingService } from '../../../services/property-listing.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DiscountService } from '../../../services/DiscountService';
import { DiscountCodeModel } from '../../../models/Discount.model';

@Component({
  selector: 'app-discount-codes',
  imports: [CommonModule,RouterLink,FormsModule],
  templateUrl: './discount-codes.html',
  styleUrl: './discount-codes.css'
})
export class DiscountCodes {
listings: DiscountCodeModel[] = [];
  searchTerm: string = '';
  isLoading: boolean = false;
  error: string | null = null;

  paginationInfo = {
    currentPage: 1,
    pageSize: 10,
    totalCount: 0,
    totalPages: 1
  };

  public listingsSubject = new BehaviorSubject<PropertyListingQueryParametersDto>({
    pageNumber: 1,
    pageSize: 10
  });

  math=Math;
  constructor(private discountService:DiscountService){}

  ngOnInit(): void {
    this.listingsSubject.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(params => {
        this.isLoading = true;
        return this.discountService.getCodes(params);
      })
    ).subscribe({
      next: (response) => {
        console.log(response)
        this.listings = response.$values || [];
        console.log('listings',this.listings)
        this.updatePaginationInfo(response);
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Failed to load listings. Please try again later.';
        this.isLoading = false;
      }
    });
  }

  filterListings(): void {
    this.listingsSubject.next({
      ...this.listingsSubject.value,
      keyword: this.searchTerm,
      // ...filters,
      pageNumber: 1
    });
  }
 
  
  

  goToPage(page: number): void {
    if (page >= 1 && page <= this.paginationInfo.totalPages) {
      this.listingsSubject.next({
        ...this.listingsSubject.value,
        pageNumber: page
      });
    }
  }
  deleteListing(id:string):void
  {
    if (confirm('Are you sure you want to delete this listing?')) {
      this.discountService.DeleteCode(id).subscribe({
        next: () => {
          this.listingsSubject.next({
            ...this.listingsSubject.value,
            pageNumber: this.paginationInfo.currentPage
          });
        },
        error: (err) => {
          this.error = 'Failed to delete Code. Please try again.';
        }
      });
    }
  }
  patchExpire(id:string):void
  {
    if(confirm("Do you want to set status to Expired?"))
    {
      this.discountService.PatchExpiryStatus(id).subscribe({
        next: () => {
          this.listingsSubject.next({
            ...this.listingsSubject.value,
            pageNumber: this.paginationInfo.currentPage
          });
        },
        error: (err) => {
          this.error = 'Failed to update status . Please try again.';
        }
      });
    }
  }
 

  private updatePaginationInfo(response: PagedResult<PropertyListing>): void {
    this.paginationInfo = {
      currentPage: response.pageNumber,
      pageSize: response.pageSize,
      totalCount: response.totalCount,
      totalPages: Math.ceil(response.totalCount / response.pageSize)
    };
  }

  get pagesToShow(): number[] {
    const pages = [];
    const maxPagesToShow = 5;
    let startPage = Math.max(1, this.paginationInfo.currentPage - Math.floor(maxPagesToShow / 2));
    const endPage = Math.min(this.paginationInfo.totalPages, startPage + maxPagesToShow - 1);
    
    if (endPage - startPage + 1 < maxPagesToShow) {
      startPage = Math.max(1, endPage - maxPagesToShow + 1);
    }
    
    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }
    return pages;
  }
}
