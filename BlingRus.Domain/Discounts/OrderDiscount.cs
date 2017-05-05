namespace BlingRus.Domain.Discounts
{
    public class OrderDiscount
    {
        public string Description { get; private set; }
        public decimal DiscountedAmount { get; private set; }

        public OrderDiscount(string description, decimal discountedAmount)
        {
            Description = description;
            DiscountedAmount = discountedAmount;
        }
    }
}
