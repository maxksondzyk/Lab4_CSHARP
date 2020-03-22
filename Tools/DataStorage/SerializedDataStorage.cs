using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Lab4_CSHARP.Models;
using Lab4_CSHARP.Tools.Managers;
using Lab4_CSHARP.Tools;

namespace Lab4_CSHARP.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        internal SerializedDataStorage()
        {
            try
            {
                UsersList = SerializationManager.Deserialize<ObservableCollection<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                UsersList = new ObservableCollection<Person>();
                InitFill();
                SaveChanges();
            }
        }
        private void InitFill()
        {
            var rand = new Random();
            for (var i = 0; i < 50; i++)
            {
                AddPerson(new Person($"{rand.Next(1, 200)}John",
                    $"Smith{i}",
                    $"john{rand.Next(1, 101)}@smith.com",
                    new DateTime(rand.Next(DateTime.Today.Year-134, DateTime.Today.Year), rand.Next(1, 12), rand.Next(1, 27))));
            }

        }

        public void AddPerson(Person person)
        {
            Application.Current.Dispatcher.Invoke((Action) delegate { UsersList.Add(person); });
                SaveChanges();
        }

        public void DeletePerson(Person person)
        {
            Application.Current.Dispatcher.Invoke((Action) delegate { UsersList.Remove(person); });
            SaveChanges();
        }

        public void EditPerson(Person person, Person resPerson)
        {
            Application.Current.Dispatcher.Invoke((Action) delegate
            {
                UsersList[UsersList.IndexOf(person)] = resPerson;
            });
            SaveChanges();
        }

        public void SaveChanges()
        {
            SerializationManager.Serialize(UsersList, FileFolderHelper.StorageFilePath);
        }

        public ObservableCollection<Person> UsersList { get; set; }
    }
}
