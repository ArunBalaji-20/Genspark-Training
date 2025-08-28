using System.Security.Claims;
using RealEstateApi.Exceptions;
using RealEstateApi.Interfaces;
using RealEstateApi.Misc;
using RealEstateApi.Models;
using RealEstateApi.Models.DTOs;

namespace RealEstateApi.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<Guid, Purchase> _purchaseRepository;
        private readonly IRepository<Guid, PropertyListing> _listingRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDiscountCodeService _discountCodeService;
        private readonly IRepository<Guid, DiscountCodes> _discountCodeRepository;

        public PurchaseService(IRepository<Guid, Purchase> purchaseRepository,
                            IRepository<Guid, PropertyListing> listingRepository,
                            IDiscountCodeService discountCodeService,
                            IRepository<Guid, DiscountCodes> discountCodeRepository,
                            IHttpContextAccessor httpContextAccessor)
        {
            _purchaseRepository = purchaseRepository;
            _listingRepository = listingRepository;
            _httpContextAccessor = httpContextAccessor;
            _discountCodeService = discountCodeService;
            _discountCodeRepository = discountCodeRepository;
        }

        public async Task<Purchase> CreatePurchaseAsync(CreatePurchaseDto purchaseDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var role = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Buyer" || !userId.HasValue)
                throw new UnauthorizedAccessAppException("Only buyers can purchase properties.");

            var listing = await _listingRepository.GetByIdAsync(purchaseDto.ListingId);
            if (listing == null || listing.IsDeleted)
                throw new NotFoundException("Listing not found.");

            if (listing.Status == "Sold")
                throw new FailedOperationException("This property has already been sold.");

            // Mark listing as sold
            listing.Status = "Sold";
            listing.UpdatedAt = DateTime.UtcNow;
            await _listingRepository.UpdateAsync(listing.Id, listing);

            var purchase = new Purchase
            {
                BuyerId = userId.Value,
                ListingId = listing.Id,
                PriceAtPurchase = listing.Price,
                OrginalPrice = listing.Price //added this here
            };

            return await _purchaseRepository.AddAsync(purchase);

        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByBuyerAsync(Guid buyerId)
        {
            var purchases = await _purchaseRepository.GetAllAsync();
            return purchases.Where(p => p.BuyerId == buyerId);
        }



        public async Task<Purchase> CreatePurchaseWithDiscountAsync(CreatePurchaseWithDiscountDto purchaseDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var role = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Buyer" || !userId.HasValue)
                throw new UnauthorizedAccessAppException("Only buyers can purchase properties.");

            var listing = await _listingRepository.GetByIdAsync(purchaseDto.ListingId);
            if (listing == null || listing.IsDeleted)
                throw new NotFoundException("Listing not found.");

            if (listing.Status == "Sold")
                throw new FailedOperationException("This property has already been sold.");

            if (purchaseDto.code != null)
            {
                var CheckCode = await _discountCodeService.CheckCodeAvail(purchaseDto.code);
                if (CheckCode != null)
                {
                    // Mark listing as sold
                    double DiscountAmount = listing.Price * (CheckCode.DiscountPercentage / 100.0);

                    listing.Status = "Sold";
                    listing.UpdatedAt = DateTime.UtcNow;
                    await _listingRepository.UpdateAsync(listing.Id, listing);

                    var purchase = new Purchase
                    {
                        BuyerId = userId.Value,
                        ListingId = listing.Id,
                        PriceAtPurchase = listing.Price - DiscountAmount,
                        OrginalPrice = listing.Price,
                        DiscountCode = purchaseDto.code


                    };
                    CheckCode.Remaining = CheckCode.Remaining - 1;

                    await _discountCodeRepository.UpdateAsync(CheckCode.Id, CheckCode);

                    return await _purchaseRepository.AddAsync(purchase);
                }
                else
                {
                    throw new NotFoundException("Invalid Code");
                }
            }
            else
            {
                // Mark listing as sold
                listing.Status = "Sold";
                listing.UpdatedAt = DateTime.UtcNow;
                await _listingRepository.UpdateAsync(listing.Id, listing);

                var purchase = new Purchase
                {
                    BuyerId = userId.Value,
                    ListingId = listing.Id,
                    PriceAtPurchase = listing.Price,
                    OrginalPrice = listing.Price,//added this here


                };

                return await _purchaseRepository.AddAsync(purchase);
            }


        }

        public async Task<double> GetFinalPriceAfterDiscount(CreatePurchaseWithDiscountDto purchaseDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var role = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Buyer" || !userId.HasValue)
                throw new UnauthorizedAccessAppException("Only buyers can purchase properties.");

            var listing = await _listingRepository.GetByIdAsync(purchaseDto.ListingId);
            if (listing == null || listing.IsDeleted)
                throw new NotFoundException("Listing not found.");

            if (listing.Status == "Sold")
                throw new FailedOperationException("This property has already been sold.");

            if (purchaseDto.code != null)
            {
                var CheckCode = await _discountCodeService.CheckCodeAvail(purchaseDto.code);
                if (CheckCode != null)
                {
                    // Mark listing as sold
                    double DiscountAmount = listing.Price * (CheckCode.DiscountPercentage / 100.0);

                    double FinalPrice = listing.Price - DiscountAmount;
                    return FinalPrice;

                }
                else
                {
                    throw new NotFoundException("Invalid Code");
                }
            }
            else
            {
                // without code
                double FinalPrice = listing.Price;
                return FinalPrice;
            }
        }

    }
}