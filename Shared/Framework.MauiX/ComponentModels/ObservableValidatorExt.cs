using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Framework.MauiX.ComponentModels;

public class ObservableValidatorExt : ObservableValidator
{
    public ObservableValidatorExt()
    {
        this.ErrorsChanged += ObservableValidatorExt_ErrorsChanged;
    }

    /// <summary>
    /// Raises the PropertyChanged event if needed.
    /// </summary>
    /// <remarks>If the propertyName parameter
    /// does not correspond to an existing property on the current class, an
    /// exception is thrown in DEBUG configuration only.</remarks>
    /// <param name="propertyName">The name of the property that
    /// changed.</param>
    [SuppressMessage(
        "Microsoft.Design",
        "CA1030:UseEventsWhereAppropriate",
        Justification = "This cannot be an event")]
    public virtual void RaisePropertyChanged(
        string propertyName)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

    }

    public IEnumerable<ValidationResult> Errors
    {
        get { return base.GetErrors(); }
    }

    private void ObservableValidatorExt_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
        RaisePropertyChanged(nameof(Errors));
    }
}

