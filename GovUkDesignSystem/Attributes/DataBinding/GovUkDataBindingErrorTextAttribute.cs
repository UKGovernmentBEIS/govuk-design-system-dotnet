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
        private ResourceManager _resourceManager;

        protected Type ResourceType { get; set; }

        protected string ResourceName { get; set; }

        protected string GetResourceValue(string resourceKey)
        {
            var resourceAssembly = ResourceType?.Assembly;
            if (resourceAssembly != null && _resourceManager == null)
            {
                _resourceManager = new ResourceManager(ResourceName, resourceAssembly);
            }

            return _resourceManager?.GetString(resourceKey, CultureInfo.CurrentCulture) ?? resourceKey;
        }
    }
}
