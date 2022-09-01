
namespace Framework.MauiX.ViewModels
{
    public class ViewModelLocator
    {
        public static void RegisterViewModels()
        {
            DependencyService.Register<Framework.MauiX.ViewModels.ProgressBarVM>();
        }

        public Framework.MauiX.ViewModels.ProgressBarVM ProgressBarVM
        {
            get { return DependencyService.Resolve<Framework.MauiX.ViewModels.ProgressBarVM>(DependencyFetchTarget.GlobalInstance); }
        }
    }
}

