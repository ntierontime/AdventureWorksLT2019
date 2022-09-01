
namespace AdventureWorksLT2019.MauiX.ViewModels
{
    public class ViewModelLocator
    {
        public static void RegisterViewModels()
        {
            DependencyService.Register<AdventureWorksLT2019.MauiX.ViewModels.AppVM>();
            DependencyService.Register<AdventureWorksLT2019.MauiX.ViewModels.AppLoadingVM>();
        }

        //public AdventureWorksLT2019.MauiX.ViewModels.AppVM AppVM
        //{
        //    get { return DependencyService.Resolve<AdventureWorksLT2019.MauiX.ViewModels.AppVM>(DependencyFetchTarget.GlobalInstance); }
        //}

        public AdventureWorksLT2019.MauiX.ViewModels.AppLoadingVM AppLoadingVM
        {
            get { return DependencyService.Resolve<AdventureWorksLT2019.MauiX.ViewModels.AppLoadingVM>(DependencyFetchTarget.GlobalInstance); }
        }
    }
}

