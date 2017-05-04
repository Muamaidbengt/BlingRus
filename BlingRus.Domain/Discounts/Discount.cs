namespace BlingRus.Domain.Discounts
{
    public class Discount
    {
        public string Description { get; }
        public decimal DiscountedAmount { get; }

        public Discount(string description, decimal discountedAmount)
        {
            Description = description;
            DiscountedAmount = discountedAmount;
        }
    }
}
