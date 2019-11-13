using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Validation_sample
{
    public class ValidationRuleViewModel
    {
        public ValidationRuleViewModel()
        {
            this.Name = "John";
            this.Age = 26;
            this.DOB = DateTime.Parse("1995-10-12");
            this.Email = "john@hotmail.com";
        }

        private string firstName;
        public string Name
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                this.RaisePropertyChanged("Name");
            }
        }

        [Range(1, 100)]
        private int? age;
        public int? Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                this.RaisePropertyChanged("Age");
            }
        }

        private DateTime dob;
        public DateTime DOB
        {
            get
            {
                return dob;
            }
            set
            {
                dob = value;
                this.RaisePropertyChanged("DOB");
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                this.RaisePropertyChanged("Email");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// Rule for email
    /// </summary>
    public class EmailValidationRule : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = new System.Windows.Controls.ValidationResult(true, null);

            if (string.IsNullOrEmpty(value.ToString()))
            {
                result = new System.Windows.Controls.ValidationResult(false, "Email Required");
            }
            else
            {
                string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

                if (!Regex.IsMatch(value.ToString(), MatchEmailPattern))
                    result = new System.Windows.Controls.ValidationResult(false, "Invalid Email ID");
            }

            return result;
        }
    }

    /// <summary>
    /// Rule for age
    /// </summary>
    public class AgeValidationRule : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = new System.Windows.Controls.ValidationResult(true, null);
            int age = 0;
            if(string.IsNullOrEmpty(value.ToString()))
            {
                result = new System.Windows.Controls.ValidationResult(false, "Age is mandatory");
            }
           else if(int.TryParse(value.ToString(), out age))
            {
                if(age < 1 || age > 100)
                {
                    result = new System.Windows.Controls.ValidationResult(false, "Age should greater than 1 and less than 100");
                }
            }
            else
            {
                result = new System.Windows.Controls.ValidationResult(false, "Invalid input");
            }
            return result;
        }
    }

    /// <summary>
    /// Rule for name
    /// </summary>
    public class NameValidationRule : ValidationRule
    {
       public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
       {
            var result = new System.Windows.Controls.ValidationResult(true, null);

            if (string.IsNullOrEmpty(value.ToString()))
            {
                result = new System.Windows.Controls.ValidationResult(false, "Name is mandatory");
            }
            else if (!Regex.IsMatch(value.ToString(), @"^[a-zA-Z]+$"))
                result = new System.Windows.Controls.ValidationResult(false, "Should enter alphabets only!!!");

            return result;
        }
    }

    public class DateValidation : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = new System.Windows.Controls.ValidationResult(true, null);
            if(value is null)
            {
                result = new System.Windows.Controls.ValidationResult(false, "DOB is mandatory");
            }
            return result;
        }
    }
}
