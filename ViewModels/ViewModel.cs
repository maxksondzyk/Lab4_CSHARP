using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using KsondzykLab2.Models;
using KsondzykLab2.Tools.Exceptions;
using KsondzykLab2.Tools.Managers;
using KsondzykLab2.Tools.MVVM;

namespace KsondzykLab2.ViewModels
{

    internal class ViewModel : INotifyPropertyChanged
    {
        #region Fields
        private RelayCommand<object> _startCommand;
        private Person _person = new Person();
        private string _name;
        private string _lastName;
        private string _mail;
        private string _isAdult;
        private string _sunSign;
        private string _chineseSign;
        private string _isBirthday;
        private DateTime? _birthday;
        private string _birth;
        #endregion

        #region Properties
        public DateTime? Birthday
        {
            private get => _birthday;
            set
            {
                _birthday = value;
                if (value != null) _birth = value.Value.ToShortDateString();

                OnPropertyChanged();
            }
        }
        public string Name
        {
            private get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            private get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string Mail
        {
            private get => _mail;
            set
            {
                _mail = value;
                OnPropertyChanged();
            }
        }

        public string IsAdult
        { 
            get => _isAdult;
            private set
            {
                _isAdult = value;
                OnPropertyChanged();
            }
        }
        public string IsBirthday
        {
            get => _isBirthday;
            private set
            {
                _isBirthday = value;
                OnPropertyChanged();
            }
        }
        public string SunSign
        {
            get => _sunSign;
            private set
            {
                _sunSign = value;
                OnPropertyChanged();
            }
        }
        public string ChineseSign
        {
            get => _chineseSign;
           private set
            {
                _chineseSign = value;
                OnPropertyChanged();
            }
        }
        public string UserMail
        {
            get => _person.Mail;
           private set
            {
                _person.Mail = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get => _person.Name;
            private set
            {
                _person.Name = value;
                OnPropertyChanged();
            }
        }
        public string UserLastName
        {
            get => _person.LastName;
            private set
            {
                _person.LastName = value;
                OnPropertyChanged();
            }
        }

        public string Birth
        {
            get => _birth;
            private set
            {
                _birth = value;
                OnPropertyChanged();
            }
        }
        #endregion
        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(Mail) && Birthday.HasValue && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Name);
        }
        public RelayCommand<object> StartCommand
        {
            get
            {
                return _startCommand ??= new RelayCommand<object>(Calculate, o => CanExecute());
            }

        }
        
        private async void Calculate(object obj)
        {
          try
          {
                LoaderManager.Instance.ShowLoader();
                await Task.Run(() => _person = new Person(Name,LastName,Mail,Birthday));

                IsAdult = $"Adult: {_person.IsAdult}";
                SunSign = $"Your sun sign: {_person.SunSign}";
                ChineseSign = $"Your chinese sign: {_person.ChineseSign}";
                IsBirthday = $"It's your birthday: {_person.isBirthday}";
                UserName = $"Your name is {_person.Name}";
                UserLastName = $"Your last name is {_person.LastName}";
                Birth = $"Your birthday is: {_person.Birthday.Value.ToShortDateString()}";
                UserMail = $"Your mail is: {_person.Mail}";
                LoaderManager.Instance.HideLoader();
                if (_person.Birthday.Value.Day.Equals(DateTime.Today.Day)&& _person.Birthday.Value.Month.Equals(DateTime.Today.Month))
                {
                    MessageBox.Show("Happy Birthday!");
                }
                   
          }
          catch (InvalidFutureDateException e)
          {
              clear();
              MessageBox.Show(e.Message);
          }
          catch (InvalidPastDateException e)
          {
              clear();
              MessageBox.Show(e.Message);
          }
          catch (InvalidMailException e)
          {
              clear();
              MessageBox.Show(e.Message);
          }
        }

        private void clear()
        {
            IsAdult = "";
            SunSign = "";
            ChineseSign = "";
            IsBirthday = "";
            UserName = "";
            UserLastName = "";
            Birth = "";
            UserMail = "";
            LoaderManager.Instance.HideLoader();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
