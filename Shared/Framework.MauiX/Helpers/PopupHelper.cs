namespace Framework.MauiX.Helpers;

public class PopupHelper
{
    public static Size GetPopupSize()
    {
        IDeviceDisplay deviceDisplay = Framework.MauiX.Helpers.ServiceHelper.GetService<IDeviceDisplay>();
#if WINDOWS
        return new(0.5 * (deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density), 0.5 * (deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density));
#else
        // Full Screen On Android / IOs ...
        return new(0.975 * (deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density), 0.875 * (deviceDisplay.MainDisplayInfo.Height / deviceDisplay.MainDisplayInfo.Density));
#endif
    }
}

