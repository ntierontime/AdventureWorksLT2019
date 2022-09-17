using Framework.MauiX.Effects;
namespace Framework.MauiX.Behaviors;

/// <summary>
/// This file/Code is copied from:
/// https://devblogs.microsoft.com/premier-developer/validate-input-in-xamarin-forms-using-inotifydataerrorinfo-custom-behaviors-effects-and-prism/
/// https://github.com/davidezordan/UsingValidation
/// </summary>
public class EntryValidationBehavior : Behavior<Entry>
{
    private Entry _associatedObject;

    protected override void OnAttachedTo(Entry bindable)
    {
        base.OnAttachedTo(bindable);
        // Perform setup

        _associatedObject = bindable;

        _associatedObject.TextChanged += _associatedObject_TextChanged;
    }

    void _associatedObject_TextChanged(object sender, TextChangedEventArgs e)
    {
        var source = _associatedObject.BindingContext as CommunityToolkit.Mvvm.ComponentModel.ObservableValidator;
        if (source != null && !string.IsNullOrEmpty(PropertyName))
        {
            var errors = source.GetErrors(PropertyName);
            if (errors != null && errors.Any())
            {
                var borderEffect = _associatedObject.Effects.FirstOrDefault(eff => eff is BorderEffect);
                if (borderEffect == null)
                {
                    var borderEffect1 = Effect.Resolve("BorderEffect");
                    _associatedObject.Effects.Add(borderEffect1);
                }
            }
            else
            {
                var borderEffect1 = Effect.Resolve("BorderEffect");
                var borderEffect = _associatedObject.Effects.FirstOrDefault(eff => eff.GetType() == borderEffect1.GetType());
                if (borderEffect != null)
                {
                    _associatedObject.Effects.Remove(borderEffect);
                }
            }
        }
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        base.OnDetachingFrom(bindable);
        // Perform clean up

        _associatedObject.TextChanged -= _associatedObject_TextChanged;

        _associatedObject = null;
    }

    public string PropertyName { get; set; }
}

