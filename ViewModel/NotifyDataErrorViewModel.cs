using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Validation_sample
{
    public class NotifyDataErrorViewModel : NotificationObject, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> propErrors = new Dictionary<string, List<string>>();

        private string firstName = "John";
        public string Name
        {
            get
            {
                return firstName;
            }
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
            get
            {
                return age;
            }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        private DateTime dob = DateTime.Parse("1995-10-12");
        public DateTime DOB
        {
            get
            {
                return dob;
            }
            set
            {
                dob = value;
                OnPropertyChanged("DOB");
            }
        }

        private string email = "john@hotmail.com";
        public string Email
        {
            get
            {
                return email;
            }
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
                AddError(nameof(Name), "Name should not be empty!!!");
            else if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"))
                AddError(nameof(Name), "Should enter alphabets only!!!");
        }

        private void ValidateAge()
        {
            ClearErrors(nameof(Age));
            if ((this.Age < 1 || this.Age > 100) || this.Age == null)
            {
                AddError(nameof(Age), "Age should greater than 1 and less than 100");
            }
        }

        private void ValidateEmail()
        {
            ClearErrors(nameof(Email));
            if (string.IsNullOrEmpty(this.Email))
            {
                AddError(nameof(Email), "Email Required");
            }
            else
            {
                string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

                if (!Regex.IsMatch(this.Email, MatchEmailPattern))
                    AddError(nameof(Email), "Invalid Email ID");
            }
        }
        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnPropertyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
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
                return null;
        }

        public bool HasErrors
        {
            get
            {
                try
                {
                    var propErrorsCount = propErrors.Values.FirstOrDefault(r => r.Count > 0);
                    if (propErrorsCount != null)
                        return true;
                    else
                        return false;
                }
                catch { }
                return true;
            }
        }

        private void AddError(string propertyName, string error)
        {
            if (!propErrors.ContainsKey(propertyName))
                propErrors[propertyName] = new List<string>();

            if (!propErrors[propertyName].Contains(error))
            {
                propErrors[propertyName].Add(error);
                OnPropertyErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (propErrors.ContainsKey(propertyName))
            {
                propErrors.Remove(propertyName);
                OnPropertyErrorsChanged(propertyName);
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

        private ICommand _command;

        public ICommand SaveCommand
        {
            get
            {
                return _command ?? (_command = new RelayCommand<object>(SaveChanges, IsEnable));
            }
        }

        public void SaveChanges(object param)
        {
            //code
        }

        bool IsEnable(object obj)
        {
            return !HasErrors;
        }

        #endregion
    }
}
