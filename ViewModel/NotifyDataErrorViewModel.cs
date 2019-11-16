using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Validation_sample
{
    public class NotifyDataErrorViewModel : NotificationObject, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> propErrors = new Dictionary<string, List<string>>();

        private string firstName = "John";
        public string Name
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged("Name");
            }
        }

        [Range(1, 100)]
        private int? age = 26;
        public int? Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        private DateTime dob = DateTime.Parse("1995-10-12");
        public DateTime DOB
        {
            get => dob;
            set
            {
                dob = value;
                OnPropertyChanged("DOB");
            }
        }

        private string email = "john@hotmail.com";
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private void ValidateName()
        {
            ClearErrors(nameof(Name));
            if (string.IsNullOrEmpty(Name))
            {
                AddError(nameof(Name), "Name should not be empty!!!");
            }
            else if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"))
            {
                AddError(nameof(Name), "Should enter alphabets only!!!");
            }
        }

        private void ValidateAge()
        {
            ClearErrors(nameof(Age));
            if ((Age < 1 || Age > 100) || Age == null)
            {
                AddError(nameof(Age), "Age should greater than 1 and less than 100");
            }
        }

        private void ValidateEmail()
        {
            ClearErrors(nameof(Email));
            if (string.IsNullOrEmpty(Email))
            {
                AddError(nameof(Email), "Email Required");
            }
            else
            {
                string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

                if (!Regex.IsMatch(Email, MatchEmailPattern))
                {
                    AddError(nameof(Email), "Invalid Email ID");
                }
            }
        }
        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            RaisePropertyChanged("HasErrors");
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            List<string> errors = new List<string>();
            if (propertyName != null)
            {
                propErrors.TryGetValue(propertyName, out errors);
                return errors;
            }

            else
            {
                return null;
            }
        }

        public bool HasErrors
        {
            get
            {
                try
                {
                    List<string> propErrorsCount = propErrors.Values.FirstOrDefault(r => r.Count > 0);
                    if (propErrorsCount != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { }
                return true;
            }
        }

        private void AddError(string propertyName, string error)
        {
            if (!propErrors.ContainsKey(propertyName))
            {
                propErrors[propertyName] = new List<string>();
            }

            if (!propErrors[propertyName].Contains(error))
            {
                propErrors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (propErrors.ContainsKey(propertyName))
            {
                propErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        # endregion

        #region INotifyPropertyChanged

        public void OnPropertyChanged(string name)
        {
            RaisePropertyChanged(name);
            if (name == "Name")
            {
                ValidateName();
            }
            else if (name == "Age")
            {
                ValidateAge();
            }
            else if (name == "Email")
            {
                ValidateEmail();
            }
        }

        #endregion
    }
}
