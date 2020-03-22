using System;
using System.Text.RegularExpressions;
using Lab4_CSHARP.Tools.Exceptions;

namespace Lab4_CSHARP.Models
{
    [Serializable]
    public class Person
    {
        #region Fields

        #endregion

        public Person(string name, string lastName, string mail, DateTime? birthday)
        {
            Name = name;
            LastName = lastName;
            Mail = mail;
            var rx = new Regex(@"\w+@\w+.\w+",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = rx.Matches(Mail);
            if (matches.Count == 0&&Mail!="\0")
            {
                throw new InvalidMailException("The mail address is incorrect");
            }
            Birthday = birthday;
            
            IsAdult = AdultCalculate();
            IsBirthday = BirthdayCalculate();
            SunSign = SunSignCalculate();
            ChineseSign = ChineseSignCalculate();
        }

        public string IsAdult { get; }

        public string IsBirthday { get; }

        public string ChineseSign { get; }

        public string SunSign { get; }
        public Person(string name = " ", string lastName = " ", string mail = "\0") :this(name,lastName,mail, DateTime.Today) { }

        public Person(string name, string lastName, DateTime? birthday) : this(name, lastName, "no@email.address", birthday) { }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public DateTime? Birthday { get; set; }
        private string ChineseSignCalculate()
        {
            var result = (Birthday.Value.Year % 12) switch
            {
                4 => "Rat",
                5 => "Ox",
                6 => "Tiger",
                7 => "Rabbit",
                8 => "Dragon",
                9 => "Snake",
                10 => "Horse",
                11 => "Goat",
                0 => "Monkey",
                1 => "Rooster",
                2 => "Dog",
                3 => "Pig",
                _ => throw new Exception(),
            };
            return result;
        }

        private string SunSignCalculate()
        {
            var day = Birthday.Value.Day;
            var result = Birthday.Value.Month switch
            {
                3 => (day >= 21 ? "Pisces" : "Aries"),
                4 => (day <= 20 ? "Aries" : "Taurus"),
                5 => (day <= 21 ? "Taurus" : "Gemini"),
                6 => (day <= 21 ? "Gemini" : "Cancer"),
                7 => (day <= 22 ? "Cancer" : "Leo"),
                8 => (day <= 23 ? "Leo" : "Virgo"),
                9 => (day <= 23 ? "Virgo" : "Libra"),
                10 => (day <= 23 ? "Libra" : "Scorpio"),
                11 => (day <= 22 ? "Scorpio" : "Sagitarius"),
                12 => (day <= 21 ? "Sagitarius" : "Capricorn"),
                1 => (day <= 20 ? "Capricorn" : "Aquarius"),
                2 => (day <= 18 ? "Aquarius" : "Pisces"),
                _ => throw new Exception(),
            };
            return result;
        }

        private string BirthdayCalculate()
        {
            if (Birthday.Value.Day.Equals(DateTime.Today.Day) && Birthday.Value.Month.Equals(DateTime.Today.Month))
                return "true";

            return "false";
        }

        private string AdultCalculate()
        {
            var leapYears = (DateTime.Now.Year - Birthday.Value.Year) / 4;
            var leapDays = leapYears * 366;
            var timeSpan = (DateTime.Today - Birthday.Value.Date);
            var totalDays = timeSpan.Days;
            totalDays -= leapDays;
            var years = leapYears + totalDays / 365;
            if (timeSpan.Days < 0)
            {
                throw new InvalidFutureDateException("You haven't been born yet");
            }

            if (years >= 135)
            {
                throw new InvalidPastDateException("You are too old to be alive");
            }

            return years >= 18 ? "true":"false";
        }
    }
}