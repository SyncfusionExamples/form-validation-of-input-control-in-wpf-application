using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Diagnostics;
using Syncfusion.Windows.Shared;

namespace Validation_sample
{
    public class DataErrorViewModel : NotificationObject , IDataErrorInfo
    {
        public DataErrorViewModel()
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

        private DateTime? dob;
        public DateTime? DOB
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

        private string m_result;
        public string CurrentErrorInfo
        {
            get
            {
                string errors = "";
                foreach (var error in errorList)
                {
                    errors += error + "\n";
                }
                return errors;
            }
            set
            {
                m_result = value;
                this.RaisePropertyChanged("CurrentErrorInfo");
            }
        }

        /// <summary>
        /// Gets or sets the error list.
        /// </summary>
        List<string> errorList = new List<string>();

        #region IDataErrorInfo Members

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = OnValidate(columnName);
                CurrentErrorInfo = result;

                if (string.IsNullOrEmpty(result))
                {
                    if (errorList.Count > 0)
                    {
                        CurrentErrorInfo = errorList[errorList.Count - 1];
                    }
                }

                return result;
            }
        }

        private string OnValidate(string columnName)
        {
            string result = String.Empty;
            if (columnName == "Name")
            {
                if (string.IsNullOrEmpty(this.Name))
                {
                    result = "Name is mandatory";
                    if (!errorList.Contains("Name is mandatory"))
                    {
                        errorList.Add(result);
                    }
                }
                else if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"))
                {
                    result = "Should enter alphabets only!!!";
                    if (!errorList.Contains("Should enter alphabets only!!!"))
                    {
                        errorList.Add(result);
                    }
                }
                else
                {
                    if (errorList.Contains("Should enter alphabets only!!!"))
                    {
                        errorList.Remove("Should enter alphabets only!!!");
                    }

                    if (errorList.Contains("Name is mandatory"))
                    {
                        errorList.Remove("Name is mandatory");
                    }
                }
            }
            if(columnName == "DOB")
            {
                result = "DOB is mandatory";
                if(DOB == null)
                {
                    if (!errorList.Contains(result))
                    {
                        errorList.Add(result);
                    }
                }
                else
                {
                    errorList.Remove(result);
                    result = null;
                }
            }
            if (columnName == "Age")
            {
                if ((this.Age < 1 || this.Age > 100) || this.Age == null)
                {
                    result = "Age should greater than 1 and less than 100";
                    if (!errorList.Contains("Age should greater than 1 and less than 100"))
                    {
                        errorList.Add(result);
                    }
                }
                else
                {
                    if (errorList.Contains("Age should greater than 1 and less than 100"))
                    {
                        errorList.Remove("Age should greater than 1 and less than 100");
                    }
                }
            }

            if (columnName == "Email")
            {
                string MatchEmailPattern = "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

                if (string.IsNullOrEmpty(this.Email))
                {
                    result = "Email Required";
                    if (!errorList.Contains("Email Required"))
                    {
                        errorList.Add(result);
                    }
                }
                else if (!Regex.IsMatch(this.Email, MatchEmailPattern))
                {
                    result = "Invalid Email ID";
                    if (!errorList.Contains("Invalid Email ID"))
                    {
                        errorList.Add(result);
                    }
                }
                else
                {
                    if (errorList.Contains("Email Required"))
                    {
                        errorList.Remove("Email Required");
                    }

                    if (errorList.Contains("Invalid Email ID"))
                    {
                        errorList.Remove("Invalid Email ID");
                    }
                }
            }

            return result;
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
            return string.IsNullOrEmpty(this["Name"]) && string.IsNullOrEmpty(this["Age"]) && string.IsNullOrEmpty(this["Email"]);
        }

        #endregion

    }
}
