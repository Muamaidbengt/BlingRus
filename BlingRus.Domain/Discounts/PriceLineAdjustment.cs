namespace BlingRus.Domain.Discounts
{
    public class PriceLineAdjustment
    {
        public string Description { get; private set; }
        public decimal DiscountedAmount { get; private set; }
        public decimal AddedAmount { get; private set; }
        public int? Quantity { get; private set; }

        public PriceLineAdjustment(int? quantity, string description, decimal discountedAmount, decimal addedAmount)
        {
            Quantity = quantity;
            Description = description;
            DiscountedAmount = discountedAmount;
            AddedAmount = addedAmount;
        }
    }
}