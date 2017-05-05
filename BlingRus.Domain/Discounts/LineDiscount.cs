namespace BlingRus.Domain.Discounts
{
    public class LineDiscount
    {
        public string Description { get; private set; }
        public decimal DiscountedAmount { get; private set; }

        public LineDiscount(string description, decimal discountedAmount)
        {
            Description = description;
            DiscountedAmount = discountedAmount;
        }
    }
}
