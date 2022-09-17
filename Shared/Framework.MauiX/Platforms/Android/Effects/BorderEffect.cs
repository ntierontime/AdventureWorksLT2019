using Framework.MauiX.Effects;
using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

[assembly: ResolutionGroupName("Framework.MauiX.Effects")]
[assembly: ExportEffect(typeof(BorderEffect), "BorderEffect")]
namespace Framework.MauiX.Platforms.Android.Effects
{
    public class BorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Element is Entry)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Entry)Element).BackgroundColor.ToAndroid());
                    //gd.SetCornerRadius(((EntryExt)Element).CornerRadius);
                    //gd.SetStroke(5, Xamarin.Forms.Color.Red.ToAndroid());
                    Control.SetBackground(gd);
                }
                //else if (Element is Framework.Xamariner.Controls.EntryExt)
                //{
                //    GradientDrawable gd = new GradientDrawable();
                //    gd.SetColor(((Framework.Xamariner.Controls.EntryExt)Element).BackgroundColor.ToAndroid());
                //    //gd.SetCornerRadius(((Framework.Xamariner.Controls.EntryExt)Element).CornerRadius);
                //    //gd.SetStroke(5, Xamarin.Forms.Color.Red.ToAndroid());
                //    Control.SetBackground(gd);
                //}
                else if (Element is Picker)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Picker)Element).BackgroundColor.ToAndroid());
                    //gd.SetCornerRadius(((EntryExt)Element).CornerRadius);
                    //gd.SetStroke(5, Xamarin.Forms.Color.Red.ToAndroid());
                    Control.SetBackground(gd);
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnDetached()
        {
            try
            {
                if (Element is Entry)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Entry)Element).BackgroundColor.ToAndroid());
                    Control.SetBackground(gd);
                }
                //else if (Element is Framework.Xamariner.Controls.EntryExt)
                //{
                //    GradientDrawable gd = new GradientDrawable();
                //    gd.SetColor(((Framework.Xamariner.Controls.EntryExt)Element).BackgroundColor.ToAndroid());
                //    gd.SetCornerRadius(((Framework.Xamariner.Controls.EntryExt)Element).CornerRadius);
                //    gd.SetStroke(((Framework.Xamariner.Controls.EntryExt)Element).BorderWidth, ((Framework.Xamariner.Controls.EntryExt)Element).BorderColor.ToAndroid());
                //    Control.SetBackground(gd);
                //}
                else if (Element is Picker)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(((Picker)Element).BackgroundColor.ToAndroid());
                    gd.SetCornerRadius(10);
                    gd.SetStroke(2, Colors.Black.ToAndroid());
                    Control.SetBackground(gd);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

