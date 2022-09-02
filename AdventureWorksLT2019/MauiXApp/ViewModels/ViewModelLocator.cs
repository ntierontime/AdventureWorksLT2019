
namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    /// <summary>
    /// 1. Page Level View Models will be resolved in Page Constructor
    /// 2. Some of Control Level View Models will be in Locator
    /// </summary>
    public class ViewModelLocator
    {
        public AdventureWorksLT2019.MauiXApp.ViewModels.ProgressBarVM ProgressBarVM
        {
            get { return Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.ProgressBarVM>(); }
        }

    }
}
