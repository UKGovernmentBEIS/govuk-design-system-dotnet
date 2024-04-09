using System;
using System.Resources;
using System.Globalization;

namespace GovUkDesignSystem.Attributes.DataBinding
{
    /// <summary>
    /// Abstract base class for all attributes that hold data binding error text
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class GovUkDataBindingErrorTextAttribute : Attribute
    {
    }
}
