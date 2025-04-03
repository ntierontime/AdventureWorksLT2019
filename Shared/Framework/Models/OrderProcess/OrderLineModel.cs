namespace Framework.Models
{
    public interface IOrderLineModel
    {
        double ItemListingPrice { get; set; }
        double ItemPriceAfterDiscount { get; set; }
        double ItemsCount{ get; set; }
        double SubTotal { get; set; }
        double TaxAmount { get; set; }
        double LineTotal { get; }
    }

    public class OrderLineModel: IOrderLineModel, IEquatable<OrderLineModel>
    {
        public string UniqueName { get; set; } = null!;
        public double ItemListingPrice { get; set; }
        public double ItemPriceAfterDiscount { get; set; }
        public double ItemsCount { get; set; }
        public double SubTotal { get; set; }
        public string? TaxCode { get; set; }
        public double TaxAmount { get; set; }
        public double LineTotal { get { return SubTotal + TaxAmount; } }

        public bool Equals(OrderLineModel? other)
        {
            return other != null && this.SubTotal == other.SubTotal && this.TaxAmount == other.TaxAmount;
        }

        public override string ToString()
        {
            return $"{SubTotal}+{TaxAmount}={LineTotal};Tax{TaxCode}={TaxAmount};SubTotal={ItemPriceAfterDiscount}x{ItemsCount};ItemPrice={ItemPriceAfterDiscount}({ItemListingPrice})";
        }
    }
}

