using System.ComponentModel.DataAnnotations;

namespace FirstAPI.Misc
{
    public class NameValidation : ValidationAttribute
    {
        public NameValidation()
        {
            ErrorMessage = "Name can only contain letters and spaces.";
        }

        public NameValidation(string errorMessage) : base(errorMessage)
        {
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false; 
            }

            if (!(value is string name))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            foreach (char c in name)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c)) 
                {
                    return false; 
                }
            }

            return true; 
        }
    }
}