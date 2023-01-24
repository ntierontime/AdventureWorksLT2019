using UIKit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MauiX.Services;

public partial class DeviceOrientationService
{
    private static readonly IReadOnlyDictionary<DisplayOrientation, UIInterfaceOrientation> _iosDisplayOrientationMap =
        new Dictionary<DisplayOrientation, UIInterfaceOrientation>
        {
            [DisplayOrientation.Landscape] = UIInterfaceOrientation.LandscapeLeft,
            [DisplayOrientation.Portrait] = UIInterfaceOrientation.Portrait,
        };

    public partial void SetDeviceOrientation(DisplayOrientation displayOrientation)
    {
        if (_iosDisplayOrientationMap.TryGetValue(displayOrientation, out UIInterfaceOrientation iosOrientation))
        {
            UIApplication.SharedApplication.SetStatusBarOrientation(iosOrientation, true);
        }
    }
}
