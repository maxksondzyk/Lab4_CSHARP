using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Lab4_CSHARP.Models;
using Lab4_CSHARP.Tools;
using Lab4_CSHARP.Tools.Exceptions;
using Lab4_CSHARP.Tools.Managers;
using Lab4_CSHARP.Tools.MVVM;
using Lab4_CSHARP.Tools.Navigation;

namespace Lab4_CSHARP.ViewModels
{

    internal class ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields
        private static ViewModel _instance;
        private static DateTime? _birthday;
        private static string _name;
        private static string _lastName;
        private static string _mail;
        private bool _edit;
        private ObservableCollection<Person> _people;
        #region Commands
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _copyCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _proceedCommand;
        private RelayCommand<object> _cancelCommand;

        private RelayCommand<object> _sortByName;
        private RelayCommand<object> _sortByLastName;
        private RelayCommand<object> _sortByMail;
        private RelayCommand<object> _sortByBirthday;
        private RelayCommand<object> _sortBySunSign;
        private RelayCommand<object> _sortByChineseSign;
        private RelayCommand<object> _sortByAdultness;
        private RelayCommand<object> _sortByBirthdayness;

        #endregion
        
        #endregion

        #region Properties

        public static ViewModel Instance => _instance ??= new ViewModel();
        internal ViewModel()
        {
        
            _people = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }
        public ObservableCollection<Person> People
        {
            get => _people;
            set
            {
               _people = value;
                OnPropertyChanged();
            }
        }
        public Person SelectedPerson { get; set; }
        public DateTime? Birthday
        {
            private get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            private get
            {
                return _name;
            }
            set
            {

                _name = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            private get
            {
                return _lastName;
            }
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

        #region Commands
        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ??= new RelayCommand<object>(
                    AddMethod);
            }
        }
     
        public RelayCommand<object> EditCommand
        {
            get
            {
                return _editCommand ??= new RelayCommand<object>(
                   EditMethod, o =>UserSelected());
            }
        }
        public RelayCommand<object> CopyCommand
        {
            get
            {
                return _copyCommand ??= new RelayCommand<object>(
                    CopyMethod, o => UserSelected());
            }
        }
       
        public RelayCommand<object> DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand<object>(
                    DeleteMethod, o =>UserSelected());
            }
        }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(o => Environment.Exit(0));
            }
        }
        public RelayCommand<object> SortByName
        {
            get
            {
                return _sortByName ??= new RelayCommand<object>(o =>
                    SortMethod(o, 1));
            }
        }
        public RelayCommand<object> SortByLastName
        {
            get
            {
                return _sortByLastName ??= new RelayCommand<object>(o =>
                    SortMethod(o, 2));
            }
        }
        public RelayCommand<object> SortByMail
        {
            get
            {
                return _sortByMail ??= new RelayCommand<object>(o =>
                    SortMethod(o, 3));
            }
        }
        public RelayCommand<object> SortByBirthday
        {
            get
            {
                return _sortByBirthday ??= new RelayCommand<object>(o =>
                    SortMethod(o, 4));
            }
        }
        public RelayCommand<object> SortBySunSign
        {
            get
            {
                return _sortBySunSign ??= new RelayCommand<object>(o =>
                    SortMethod(o, 5));
            }
        }
        public RelayCommand<object> SortByChineseSign
        {
            get
            {
                return _sortByChineseSign ??= new RelayCommand<object>(o =>
                    SortMethod(o, 6));
            }
        }
        public RelayCommand<object> SortByAdultness
        {
            get
            {
                return _sortByAdultness ??= new RelayCommand<object>(o =>
                    SortMethod(o, 7));
            }
        }
        public RelayCommand<object> SortByBirthdayness
        {
            get
            {
                return _sortByBirthdayness ??= new RelayCommand<object>(o =>
                    SortMethod(o, 8));
            }
        }
        public RelayCommand<object> Proceed
        {
            get
            {
                return _proceedCommand ??= new RelayCommand<object>(
                    ProceedMethod, o => CanExecute());
            }
        }

        #endregion

        #endregion
        private void AddMethod(object obj)
        {
            _edit = false;
            Clear();
            NavigationManager.Instance.Navigate(ViewType.Add);

        }
        private async void DeleteMethod(object obj)
        {
            if (MessageBox.Show($"Delete {SelectedPerson}?",
                    "Delete?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                StationManager.DataStorage.DeletePerson(SelectedPerson);
                SelectedPerson = null;
                People = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
            });
            LoaderManager.Instance.HideLoader();
        }
        private async void CopyMethod(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                StationManager.DataStorage.AddPerson(new Person(SelectedPerson.Name, SelectedPerson.LastName,
                    SelectedPerson.Mail, SelectedPerson.Birthday));
                People = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
            });
            LoaderManager.Instance.HideLoader();
        }
        private void EditMethod(object obj)
        {
            _edit = true;
            Fill();
            NavigationManager.Instance.Navigate(ViewType.Edit);
        }

        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(Mail) && Birthday.HasValue && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Name);
        }

        private bool UserSelected()
        {
            return SelectedPerson != null;
        }

        private void Fill()
        {
            Name = SelectedPerson.Name;
            LastName = SelectedPerson.LastName;
            Mail = SelectedPerson.Mail;
            Birthday = SelectedPerson.Birthday;
        }
        private void Clear()
        {
            Name = "";
            LastName = "";
            Mail = "";
            Birthday = DateTime.Today;
        }
   
        private async void SortMethod(object obj, int i)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                var sortedPeople = i switch
                {
                    1 => (from u in _people orderby u.Name select u),
                    2 => (from u in _people orderby u.LastName select u),
                    3 => (from u in _people orderby u.Mail select u),
                    4 => (from u in _people orderby u.Birthday select u),
                    5 => (from u in _people orderby u.SunSign select u),
                    6 => (from u in _people orderby u.ChineseSign select u),
                    7 => (from u in _people orderby u.IsAdult select u),
                    _ => (from u in _people orderby u.IsBirthday select u)
                };
                People = new ObservableCollection<Person>(sortedPeople);
                StationManager.DataStorage.UsersList = People;
            });
            LoaderManager.Instance.HideLoader();
        }

        
        public RelayCommand<object> Cancel
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(
                    CancelMethod);
            }
        }
        private void CancelMethod(object obj)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Cancel?",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    NavigationManager.Instance.Navigate(ViewType.Main);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
       
        private async void ProceedMethod(object obj)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (_edit)
                    {
                        StationManager.DataStorage.EditPerson(SelectedPerson,
                            new Person(Name, LastName, Mail, Birthday));


                        _edit = false;
                    }
                    else
                    {
                        StationManager.DataStorage.AddPerson(new Person(Name, LastName, Mail, Birthday));
                    }

                    People = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
                    NavigationManager.Instance.Navigate(ViewType.Main);
                });
            }
            catch (InvalidFutureDateException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (InvalidPastDateException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (InvalidMailException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
