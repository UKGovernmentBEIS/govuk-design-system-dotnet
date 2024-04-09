using System;

namespace GovUkDesignSystem.Attributes.DataBinding
{
    public class GovUkDataBindingMandatoryDecimalLocalisableErrorTextAttribute : GovUkDataBindingMandatoryDecimalErrorTextAttribute
    {
        public GovUkDataBindingMandatoryDecimalLocalisableErrorTextAttribute(string errorMessageIfMissing, string nameAtStartOfSentence = "", string mustBeNumberErrorMessage = "", string resourceName = "", Type resourceType = null): base(errorMessageIfMissing, nameAtStartOfSentence)
        {
            if(string.IsNullOrEmpty(nameAtStartOfSentence) && string.IsNullOrEmpty(mustBeNumberErrorMessage))
            {
                throw new ArgumentNullException("nameAtStartOfSentence cannot be null or empty unless all error messages are overwritten");
            }
            
            if (resourceType == null ^ string.IsNullOrEmpty(resourceName))
            {
                throw new ArgumentNullException("resourceName or resourceType cannot be null or empty while the other is not null or empty");
            }
            
            if(string.IsNullOrEmpty(errorMessageIfMissing))
            {
                throw new ArgumentNullException("errorMessageIfMissing cannot be null or empty");
            }
            
            _mustBeNumberErrorMessage = mustBeNumberErrorMessage;
            ResourceType = resourceType;
            ResourceName = resourceName;
        }

        /// <summary>
        /// A complete sentence of the form: ‘Enter [whatever it is]’.
        /// <br/>For example, ‘Enter your first name’.
        /// </summary>
        public override string ErrorMessageIfMissing => GetResourceValue(base.ErrorMessageIfMissing);

        /// <summary>
        /// An override for the error message that is displayed if the value entered is not a number.
        /// A complete sentence of the form: ‘[Whatever it is] must be a number’
        /// <br/>e.g. "Median age must be a number"
        /// </summary>
        private readonly string _mustBeNumberErrorMessage;
        public override string MustBeNumberErrorMessage => GetResourceValue(_mustBeNumberErrorMessage);
    }
}