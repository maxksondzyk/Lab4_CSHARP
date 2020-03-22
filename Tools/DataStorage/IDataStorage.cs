using System.Collections.ObjectModel;
using Lab4_CSHARP.Models;

namespace Lab4_CSHARP.Tools.DataStorage
{
    internal interface IDataStorage
    {

        void AddPerson(Person person);
        void DeletePerson(Person person);
        void EditPerson(Person person, Person resPerson);
        ObservableCollection<Person> UsersList { get; set; }
    }
}
