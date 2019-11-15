using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Validation_sample
{
    public class ValidationRuleViewModel
    {
        public ValidationRuleViewModel()
        {
            Name = "John";
            Age = 26;
            DOB = DateTime.Parse("1995-10-12");
            Email = "john@hotmail.com";
        }

        private string firstName;
        public string Name
        {
            get => firstName;
            set
            {
                firstName = value;
                RaisePropertyChanged("Name");
            }
        }

        private int? age;
        public int? Age
        {
            get => age;
            set
            {
                age = value;
                RaisePropertyChanged("Age");
            }
        }

        private DateTime dob;
        public DateTime DOB
        {
            get => dob;
            set
            {
                dob = value;
                RaisePropertyChanged("DOB");
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    /// <summary>
    /// Rule for email
    /// </summary>
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (string.IsNullOrEmpty(value.ToString()))
            {
                result = new ValidationResult(false, "Email Required");
            }
            else
            {
                string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

                if (!Regex.IsMatch(value.ToString(), MatchEmailPattern))
                {
                    result = new ValidationResult(false, "Invalid Email ID");
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Rule for age
    /// </summary>
    public class AgeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            int age;
            if (string.IsNullOrEmpty(value.ToString()))
            {
                result = new ValidationResult(false, "Age is mandatory");
            }
            else if (int.TryParse(value.ToString(), out age))
            {
                if (age < 1 || age > 100)
                {
                    result = new ValidationResult(false, "Age should greater than 1 and less than 100");
                }
            }
            else
            {
                result = new ValidationResult(false, "Invalid input");
            }
            return result;
        }
    }

    /// <summary>
    /// Rule for name
    /// </summary>
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (string.IsNullOrEmpty(value.ToString()))
            {
                result = new ValidationResult(false, "Name is mandatory");
            }
            else if (!Regex.IsMatch(value.ToString(), @"^[a-zA-Z]+$"))
            {
                result = new ValidationResult(false, "Should enter alphabets only!!!");
            }

            return result;
        }
    }

    public class DateValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            if (value is null)
            {
                result = new ValidationResult(false, "DOB is mandatory");
            }
            return result;
        }
    }
}
