namespace BlingRus.Domain.Discounts
{
    public class PriceLineAdjustment
    {
        public string Description { get; private set; }
        public decimal DiscountedAmount { get; private set; }
        public decimal AddedAmount { get; private set; }

        public PriceLineAdjustment(string description, decimal discountedAmount, decimal addedAmount)
        {
            Description = description;
            DiscountedAmount = discountedAmount;
            AddedAmount = addedAmount;
        }
    }
}