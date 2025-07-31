namespace ChienShopMigrationProject.Dtos
{
    public class OrderCreateDto
    {
        public string OrderName { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public List<OrderDetailCreateDto> OrderDetails { get; set; } = new();
    }
}