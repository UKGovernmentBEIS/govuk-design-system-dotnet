using System;

namespace GovUkDesignSystem.Attributes.DataBinding
{
    public class GovUkDataBindingOptionalDecimalLocalisableErrorTextAttribute : GovUkDataBindingOptionalDecimalErrorTextAttribute
    {
        public GovUkDataBindingOptionalDecimalLocalisableErrorTextAttribute(string nameAtStartOfSentence = "", string resourceName = "", string mustBeNumberErrorMessage = "", Type resourceType = null): base(nameAtStartOfSentence)
        {
            if (resourceType == null ^ string.IsNullOrEmpty(resourceName))
            {
                throw new ArgumentNullException("resourceName or resourceType cannot be null or empty while the other is not null or empty");
            }
            
            if (string.IsNullOrEmpty(nameAtStartOfSentence))
            {
                throw new ArgumentNullException("nameAtStartOfSentence cannot be null or empty");
            }
            _mustBeNumberErrorMessage = mustBeNumberErrorMessage;
            ResourceType = resourceType;
            ResourceName = resourceName;
        }
                
        /// <summary>
        /// An override for the error message that is displayed if the value entered is not a number.
        /// A complete sentence of the form: ‘[Whatever it is] must be a number’
        /// <br/>e.g. "Median age must be a number"
        /// </summary>
        private readonly string _mustBeNumberErrorMessage;
        public override string MustBeNumberErrorMessage => GetResourceValue(_mustBeNumberErrorMessage);
    }
}