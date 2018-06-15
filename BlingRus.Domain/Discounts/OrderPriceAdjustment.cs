namespace BlingRus.Domain.Discounts
{
    public class OrderPriceAdjustment
    {
        public string Description { get; private set; }
        public decimal DiscountedAmount { get; private set; }
        public decimal AddedAmount { get; private set; }

        public OrderPriceAdjustment(string description, decimal discountedAmount, decimal addedAmount)
        {
            Description = description;
            DiscountedAmount = discountedAmount;
            AddedAmount = addedAmount;
        }
    }
}
