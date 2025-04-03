namespace Framework.Models.OrderProcess
{
    /// <summary>
    /// 1. this request can be used for initial order, modify order, cancel order
    /// 2. only for one BoughtFor
    /// 3. only for one type of items, e.g. only one Service/Product/...
    /// 4.1. ideally, all items are coming from one Seller/ServiceProvider
    /// 4.2. all items can come from multiple Sellers/ServiceProviders
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TPerson"></typeparam>
    /// <typeparam name="TPaymentGateWay"></typeparam>
    /// <typeparam name="TShipMethod"></typeparam>
    /// <typeparam name="TAddressDataModel"></typeparam>
    public class SimpleOrderRequest<TIdentifier, TItem, TPerson, TPaymentGateWay, TShipMethod, TAddressDataModel> : MultiItemsCUDRequest<TIdentifier, TItem>
        where TItem : class
        where TPerson : class
        where TPaymentGateWay: Enum
        where TShipMethod : Enum
    {
        public TPerson? Buyer { get; set; }
        public TPerson? BoughtFor { get; set; }
        public Dictionary<TPaymentGateWay, double>? Payments { get; set; }
        public TShipMethod ShippingMethod { get; set; } = default(TShipMethod)!;
        public TAddressDataModel? BillToAddress { get; set; }
        public TAddressDataModel? ShipToAddress { get; set; }
    }
}
