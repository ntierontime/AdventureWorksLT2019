using System.Linq.Expressions;

namespace AdventureWorksLT2019.MauiXApp.SQLite
{
    public class CustomerRepository : Framework.MauiX.SQLite.SQLiteTableRepositoryBase<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery, AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier>
    {
        public CustomerRepository(Framework.MauiX.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>();
        }

        protected override Expression<Func<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, bool>> GetItemExpression(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier identifier)
        {
            return t => t.CustomerID == identifier.CustomerID;
        }

        public override async Task<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> Get(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier identifier)
        {
            return await GetItemFromTableAsync(GetItemExpression(identifier));
        }

        public override async Task Save(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier identifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel item)
        {
            await InsertUpdateItemInTableAsync(GetItemExpression(identifier), item);
        }

        protected override Expression<Func<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, bool>> GetSQLiteTableQueryPredicate_Common(AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery criteria)
        {
            return
                    t => true;
            /*
            return t =>
                        (
                            (
                            !criteria.Common.Application.IsToCompare
                            ||
                            criteria.Common.Application.IsToCompare && t.Application.Contains(criteria.Common.Application.ValueToBeContained)
                            )
                        );
            */
        }
    }
}

