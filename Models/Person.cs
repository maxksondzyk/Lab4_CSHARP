using System;
using System.Text.RegularExpressions;
using KsondzykLab2.Tools;
using KsondzykLab2.Tools.Exceptions;

namespace KsondzykLab2.Models
{
    internal class Person
    {
        #region Fields

        private string _name;
        private string _lastName;
        private string _mail;
        private DateTime? _birthday;
        public readonly string IsAdult;
        public readonly string SunSign;
        public readonly string ChineseSign;
        public readonly string isBirthday;
        #endregion

        public Person(string name, string lastName, string mail, DateTime? birthday)
        {
            this._name = name;
            this._lastName = lastName;
            this._mail = mail;
            this._birthday = birthday;
            
            IsAdult = AdultCalculate();
            isBirthday = BirthdayCalculate();
            SunSign = SunSignCalculate();
            ChineseSign = ChineseSignCalculate();
           
        }

        public Person(string name = " ", string lastName = " ", string mail = " "):this(name,lastName,mail, DateTime.Today) { }

        public Person(string name, string lastName, DateTime? birthday) : this(name, lastName, "no@email.address", birthday) { }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Mail
        {
            get
            {
                var rx = new Regex(@"\w+@\w+.\w+",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var matches = rx.Matches(_mail);
                if (matches.Count == 0)
                {
                    throw new InvalidMailException("The mail address is incorrect");
                }
                return _mail;
            }
            set => _mail = value;
        }

        public DateTime? Birthday
        {
            get => _birthday;
            set => _birthday = value;
        }
        private string ChineseSignCalculate()
        {
            var result = (_birthday.Value.Year % 12) switch
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
            var day = _birthday.Value.Day;
            var result = _birthday.Value.Month switch
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
            if (_birthday.Value.Day.Equals(DateTime.Today.Day) && _birthday.Value.Month.Equals(DateTime.Today.Month))
                return "true";

            return "false";
        }

        private string AdultCalculate()
        {
            var leapYears = (DateTime.Now.Year - _birthday.Value.Year) / 4;
            var leapDays = leapYears * 366;
            var timeSpan = (DateTime.Today - _birthday.Value.Date);
            var totalDays = timeSpan.Days;
            totalDays -= leapDays;
            var years = leapYears + totalDays / 365;
            if (timeSpan.Days < 0)
            {
                throw new InvalidFutureDateException("You haven't been born yet");
            }

            if (years > 135)
            {
                throw new InvalidPastDateException("You are too old to be alive");
            }

            return years >= 18 ? "true":"false";
        }
    }
}