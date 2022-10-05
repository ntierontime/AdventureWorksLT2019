using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public partial class SalesOrderHeaderCompositeModel : CompositeModel<SalesOrderHeaderDataModel, SalesOrderHeaderCompositeModel.__DataOptions__>
{
        // 4. ListTable = 4,
        public SalesOrderDetailDataModel[] SalesOrderDetails_Via_SalesOrderID { get; set; }

    public enum __DataOptions__
    {
        __Master__,
        // 4. ListTable
        SalesOrderDetails_Via_SalesOrderID,

    }
}

