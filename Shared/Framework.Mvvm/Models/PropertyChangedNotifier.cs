using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Framework.Mvvm.Models
{
    /// <summary>
    /// TODO: how to suppress NotifyPropertyChanged and NotifyDataErrorInfo
    /// Provides a method for Notification support.
    /// We use this class for model classes.
    /// It should be serializable.
    /// </summary>
    public class PropertyChangedNotifier : INotifyPropertyChanged, INotifyDataErrorInfo
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PropertyChangedNotifier()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            ErrorsChanged += ValidationBase_ErrorsChanged;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        }

        private void ValidationBase_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(HasErrors));
            RaisePropertyChanged(nameof(Errors));
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        ///// <summary>
        ///// Notifies that <see cref="propertyName" /> has changed.
        ///// </summary>
        ///// <param name="propertyName">Name of the property.</param>
        //protected virtual void RaisePropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(
        //            this,
        //            new PropertyChangedEventArgs(propertyName)
        //            );
        //    }
        //}

        #endregion INotifyPropertyChanged

        /// <summary>
        /// Provides access to the PropertyChanged event handler to derived classes.
        /// </summary>
        protected PropertyChangedEventHandler? PropertyChangedHandler => PropertyChanged;

        /// <summary>
        /// Occurs before a property value changes.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Provides access to the PropertyChanging event handler to derived classes.
        /// </summary>
        protected PropertyChangingEventHandler PropertyChangingHandler
        {
            get
            {
                return PropertyChanging;
            }
        }

        /// <summary>
        /// Verifies that a property name exists in this ViewModel. This method
        /// can be called before the property is used, for instance before
        /// calling RaisePropertyChanged. It avoids errors when a property name
        /// is changed but some places are missed.
        /// </summary>
        /// <remarks>This method is only active in DEBUG mode.</remarks>
        /// <param name="propertyName">The name of the property that will be
        /// checked.</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            var myType = GetType();

            if (!string.IsNullOrEmpty(propertyName) && myType.GetProperty(propertyName) == null)
            {
                if (this is ICustomTypeDescriptor descriptor)
                {
                    if (descriptor.GetProperties()
                        .Cast<PropertyDescriptor>()
                        .Any(property => property.Name == propertyName))
                    {
                        return;
                    }
                }

                throw new ArgumentException("Property not found", propertyName);
            }
        }

        /// <summary>
        /// Raises the PropertyChanging event if needed.
        /// </summary>
        /// <remarks>If the propertyName parameter
        /// does not correspond to an existing property on the current class, an
        /// exception is thrown in DEBUG configuration only.</remarks>
        /// <param name="propertyName">The name of the property that
        /// changed.</param>
        //[SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1030:UseEventsWhereAppropriate",
        //    Justification = "This cannot be an event")]
        public virtual void RaisePropertyChanging(
            string propertyName)
        {
            VerifyPropertyName(propertyName);

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <remarks>If the propertyName parameter
        /// does not correspond to an existing property on the current class, an
        /// exception is thrown in DEBUG configuration only.</remarks>
        /// <param name="propertyName">The name of the property that
        /// changed.</param>
        //[SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1030:UseEventsWhereAppropriate",
        //    Justification = "This cannot be an event")]
        public virtual void RaisePropertyChanged(
            string propertyName)
        {
            VerifyPropertyName(propertyName);

            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));

                VerifyPropertyName(propertyName);
            }
        }

        /// <summary>
        /// Raises the PropertyChanging event if needed.
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changes.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property
        /// that changes.</param>
        //[SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1030:UseEventsWhereAppropriate",
        //    Justification = "This cannot be an event")]
        //[SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1006:GenericMethodsShouldProvideTypeParameter",
        //    Justification = "This syntax is more convenient than other alternatives.")]
        public virtual void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = PropertyChanging;
            if (handler != null)
            {
                var propertyName = GetPropertyName(propertyExpression);
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property
        /// that changed.</param>
        //[SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1030:UseEventsWhereAppropriate",
        //    Justification = "This cannot be an event")]
        //[SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1006:GenericMethodsShouldProvideTypeParameter",
        //    Justification = "This syntax is more convenient than other alternatives.")]
        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                var propertyName = GetPropertyName(propertyExpression);

                if (!string.IsNullOrEmpty(propertyName))
                {
                    // ReSharper disable once ExplicitCallerInfoArgument
                    RaisePropertyChanged(propertyName);
                }
            }
        }

        /// <summary>
        /// Extracts the name of a property from an expression.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression returning the property's name.</param>
        /// <returns>The name of the property returned by the expression.</returns>
        /// <exception cref="ArgumentNullException">If the expression is null.</exception>
        /// <exception cref="ArgumentException">If the expression does not represent a property.</exception>
        //[SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1011:ConsiderPassingBaseTypesAsParameters",
        //    Justification = "This syntax is more convenient than the alternatives."),
        // SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1006:DoNotNestGenericTypesInMemberSignatures",
        //    Justification = "This syntax is more convenient than the alternatives.")]
        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (propertyExpression.Body is not MemberExpression body)
            {
                throw new ArgumentException("Invalid argument", nameof(propertyExpression));
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", nameof(propertyExpression));
            }

            return property.Name;
        }

        /// <summary>
        /// Assigns a new value to the property. Then, raises the
        /// PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property
        /// that changed.</param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change
        /// occurred.</param>
        /// <returns>True if the PropertyChanged event has been raised,
        /// false otherwise. The event is not raised if the old
        /// value is equal to the new value.</returns>
        //[SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1006:DoNotNestGenericTypesInMemberSignatures",
        //    Justification = "This syntax is more convenient than the alternatives."),
        // SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1045:DoNotPassTypesByReference",
        //    MessageId = "1#",
        //    Justification = "This syntax is more convenient than the alternatives.")]
        protected bool Set<T>(
            Expression<Func<T>> propertyExpression,
            ref T field,
            T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            RaisePropertyChanging(propertyExpression);
            field = newValue;
            RaisePropertyChanged(propertyExpression);
            return true;
        }

        /// <summary>
        /// Assigns a new value to the property. Then, raises the
        /// PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="propertyName">The name of the property that
        /// changed.</param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change
        /// occurred.</param>
        /// <returns>True if the PropertyChanged event has been raised,
        /// false otherwise. The event is not raised if the old
        /// value is equal to the new value.</returns>
        //[SuppressMessage(
        //    "Microsoft.Design",
        //    "CA1045:DoNotPassTypesByReference",
        //    MessageId = "1#",
        //    Justification = "This syntax is more convenient than the alternatives.")]
        protected bool Set<T>(
            string propertyName,
            ref T field,
            T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            RaisePropertyChanging(propertyName);
            field = newValue;

            // ReSharper disable ExplicitCallerInfoArgument
            RaisePropertyChanged(propertyName);
            // ReSharper restore ExplicitCallerInfoArgument

            return true;
        }

        // This following code is copied from:
        // https://devblogs.microsoft.com/premier-developer/validate-input-in-xamarin-forms-using-inotifydataerrorinfo-custom-behaviors-effects-and-prism/
        // https://github.com/davidezordan/UsingValidation

        #region INotifyDataErrorInfo Members

        public Dictionary<string, List<string>> Errors { get; private set; } = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (Errors.ContainsKey(propertyName) && Errors[propertyName].Any())
                {
                    return Errors[propertyName].ToList();
                }
                else
                {
                    return new List<string>();
                }
            }
            else
            {
                return Errors.SelectMany(err => err.Value.ToList()).ToList();
            }
        }

        public bool HasErrors
        {
            get
            {
                return Errors.Any(propErrors => propErrors.Value.Any());
            }
        }

        #endregion

        protected virtual void ValidateProperty(object value, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var validationContext = new ValidationContext(this, null)
            {
                MemberName = propertyName
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(value, validationContext, validationResults);

            RemoveErrorsByPropertyName(propertyName);

            HandleValidationResults(validationResults, propertyName);
        }

        private void RemoveErrorsByPropertyName(string propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                Errors.Remove(propertyName);
            }

            RaiseErrorsChanged(propertyName);
        }

        /// <summary>
        /// Changed for Compare Validation: Password and Confirm Password
        /// MemberNames is null when multiple properties validation
        /// </summary>
        /// <param name="validationResults"></param>
        /// <param name="propertyName"></param>
        private void HandleValidationResults(List<ValidationResult> validationResults, string propertyName)
        {
            var errorMessages =
                from results in validationResults
                where results.MemberNames == null || !results.MemberNames.Any() || results.MemberNames.Contains(propertyName)
                select results.ErrorMessage;

            if (errorMessages.Any())
                Errors.Add(propertyName, errorMessages.ToList());
            RaiseErrorsChanged(propertyName);

            //var resultsByPropertyName = from results in validationResults
            //                            from memberNames in results.MemberNames
            //                            group results by memberNames into groups
            //                            select groups;

            //foreach (var property in resultsByPropertyName)
            //{
            //    Errors.Add(property.Key, property.Select(r => r.ErrorMessage).ToList());
            //    RaiseErrorsChanged(property.Key);
            //}
        }

        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    public static class PropertyChangedNotifierHelper
    {
        public static bool IsToRaisePropertyChanged { get; private set; }
        public static void Initialize(bool isToRaisePropertyChanged)
        {
            IsToRaisePropertyChanged = isToRaisePropertyChanged;
        }
    }
}

