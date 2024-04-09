using System;

namespace GovUkDesignSystem.Attributes.DataBinding
{
    public class GovUkDataBindingOptionalIntLocalisableErrorTextAttribute : GovUkDataBindingOptionalIntErrorTextAttribute
    {
        public GovUkDataBindingOptionalIntLocalisableErrorTextAttribute(string nameAtStartOfSentence = "", string mustBeNumberErrorMessage = "", string resourceName = "", string isWholeNumberErrorMessage = "", Type resourceType = null): base(nameAtStartOfSentence)
        {
            
            if(string.IsNullOrEmpty(nameAtStartOfSentence) && (string.IsNullOrEmpty(isWholeNumberErrorMessage) || string.IsNullOrEmpty(mustBeNumberErrorMessage)))
            {
                throw new ArgumentNullException("nameAtStartOfSentence cannot be null or empty unless all error messages are overwritten");
            }
            
            if (resourceType == null ^ string.IsNullOrEmpty(resourceName))
            {
                throw new ArgumentNullException("resourceName or resourceType cannot be null or empty while the other is not null or empty");
            }
            
            _mustBeNumberErrorMessage = mustBeNumberErrorMessage;
            _isWholeNumberErrorMessage = isWholeNumberErrorMessage;
            ResourceType = resourceType;
            ResourceName = resourceName;
        }
        
        /// <summary>
        /// An override for the error message that is displayed if the value entered is not a whole number.
        /// A complete sentence of the form: ‘[Whatever it is] must be a whole number’
        /// <br/>e.g. "Median age must be a whole number"
        /// </summary>
        private readonly string _isWholeNumberErrorMessage;
        public override string IsWholeNumberErrorMessage => GetResourceValue(_isWholeNumberErrorMessage);
        
        /// <summary>
        /// An override for the error message that is displayed if the value entered is not a number.
        /// A complete sentence of the form: ‘[Whatever it is] must be a number’
        /// <br/>e.g. "Median age must be a number"
        /// </summary>
        private readonly string _mustBeNumberErrorMessage;
        public override string MustBeNumberErrorMessage => GetResourceValue(_mustBeNumberErrorMessage);
    }
}