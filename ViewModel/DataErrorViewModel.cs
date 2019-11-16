using Syncfusion.Windows.Shared;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Validation_sample
{
    public class DataErrorViewModel : NotificationObject, IDataErrorInfo
    {
        public DataErrorViewModel()
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

        private DateTime? dob;
        public DateTime? DOB
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

        #region IDataErrorInfo Members

        private string _error;

        public string Error
        {
            get => _error;
            set
            {
                if (_error != value)
                {
                    _error = value;
                    RaisePropertyChanged("Error");
                }
            }
        }

        public string this[string columnName] => OnValidate(columnName);

        private string OnValidate(string columnName)
        {
            string result = string.Empty;
            if (columnName == "Name")
            {
                if (string.IsNullOrEmpty(Name))
                {
                    result = "Name is mandatory";
                }
                else if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"))
                {
                    result = "Should enter alphabets only!!!";
                }
            }
            if (columnName == "DOB")
            {
                if (DOB == null)
                {
                    result = "DOB is mandatory";
                }
                else
                {
                    result = null;
                }
            }
            if (columnName == "Age")
            {
                if ((Age < 1 || Age > 100) || Age == null)
                {
                    result = "Age should greater than 1 and less than 100";
                }
            }

            if (columnName == "Email")
            {
                string MatchEmailPattern = "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                [0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

                if (string.IsNullOrEmpty(Email))
                {
                    result = "Email Required";
                }
                else if (!Regex.IsMatch(Email, MatchEmailPattern))
                {
                    result = "Invalid Email ID";
                }
            }
            if (result == null)
            {
                Error = null;
            }
            else
            {
                Error = "Error";
            }
            return result;
        }

        #endregion

    }
}
