using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Web;
using KR.Core.Models;
using System.Reflection;
using Microsoft.AspNetCore.Html;

namespace KR.Core.Mocks
{
    public class WorkersMock
    {
        private List<Worker> _workers;

        public static string[] edTypes = new string[] { "Высшее", "Среднее специальное", "Среднее", "Неполное среднее", "Начальное" };
        public static string[] militaryServiceRelationTypes = new string[] { "Военнослужащий", "Военнообязанный", "Невоеннослужащий", "Призывник", "Служащий таможенных органов" };
        private string DBPath = "DB.json";

        //It's for testing
        public void ChangeDatabasePath(string newPath) => DBPath = newPath;

        private ulong ID
        {
            get
            {
                if (_workers != null)
                {
                    if (_workers.Any())
                    {
                        return (from t in _workers select t.ID).Max() + 1;
                    }
                    else return 0;
                }
                else return 0;
            }
        }

        public void SaveDB()
        {
            using (StreamWriter sw = new StreamWriter(DBPath))
            {
                sw.Write(JsonSerializer.Serialize<List<Worker>>(_workers));
            }
        }

        public void LoadDB()
        {
            using (StreamReader sr = new StreamReader(DBPath))
            {
                _workers = JsonSerializer.Deserialize<List<Worker>>(sr.ReadToEnd());
            }
        }

        public void AddToDB(Worker w)
        {
            w.ID = ID;

            if (_workers == null)
            {
                _workers = new List<Worker>();
            }

            _workers.Add(w);
            SaveDB();
        }

        public void DeleteFromDB(ulong ID)
        {
            _workers.Remove(_workers.Find(t => t.ID == ID));
            SaveDB();
        }

        public Worker Find(ulong ID)
        {
            return _workers.Find(t => t.ID == ID);
        }

        public List<Worker> Workers => _workers;

        public IEnumerable<Worker> Younger30
        {
            get
            {
                return from t in _workers where t.Age <= 30 select t;
            }
        }

        public Dictionary<string, double> GenderDivision
        {
            get
            {
                Dictionary<string, double> Res = new Dictionary<string, double>();

                List<int> girlsAge = (from t in _workers where t.Gender == "Женщина" select t.Age).ToList();
                double girlsAverageAge = (!girlsAge.Any()) ? 0 : girlsAge.Average();
                Res.Add("Female", girlsAverageAge);

                List<int> boysAge = (from t in _workers where t.Gender == "Мужчина" select t.Age).ToList();
                double boysAverageAge = (!boysAge.Any()) ? 0 : boysAge.Average();
                Res.Add("Male", boysAverageAge);

                return Res;
            }
        }

        public static string ConvertToDatePicker(DateTime date)
        {
            string day = date.Day.ToString();
            string month = date.Month.ToString();
            string year = date.Year.ToString();

            if (day.Length < 2) day = $"0{day}";
            if (month.Length < 2) month = $"0{month}";
            while (year.Length < 4) year = $"0{year}";

            return $"{year}-{month}-{day}";
        }

        public static string DateMaxValue => ConvertToDatePicker(Convert.ToDateTime(DateTime.Now.Subtract(new TimeSpan(365 * 18, 0, 0, 0, 0))));
        public static string MonthMaxValue => ConvertToMonthPicker(DateTime.Now);

        public static string ConvertToMonthPicker(DateTime date)
        {
            string month = date.Month.ToString();
            string year = date.Year.ToString();

            if (month.Length < 2) month = $"0{month}";
            while (year.Length < 4) year = $"0{year}";

            return $"{year}-{month}";
        }

        public List<Worker> ThisYearEmployedWorkers
        {
            get
            {
                return (from t in _workers where DateTime.Now.Subtract(t.EmploymentDate) < new TimeSpan(365, 0, 0, 0, 0) orderby t.Age select t).Reverse().ToList();
            }
        }

        public Dictionary<string, List<Worker>> AgeDivision
        {
            get
            {
                Dictionary<string, List<Worker>> Res = new Dictionary<string, List<Worker>>();
                Res.Add("Младше 30", (from t in _workers where t.Age <= 30 select t).ToList());
                Res.Add("От 31 до 50", (from t in _workers where t.Age > 30 && t.Age <= 50 select t).ToList());
                Res.Add("Старше 51", (from t in _workers where t.Age > 50 select t).ToList());
                return Res;
            }
        }

        public List<object> AgeDivisionChartData
        {
            get
            {
                var res = new List<object>();
                var ageDivision = AgeDivision;

                foreach(string ageRange in ageDivision.Keys)
                {
                    res.Add(ageRange);
                    res.Add(ageDivision[ageRange].Count());
                }

                return res;
            }
        }
        public Dictionary<string, List<Worker>> EducationDivisionListResult(List<Worker> list)
        {
            var res = new Dictionary<string, List<Worker>>();

            foreach (string type in edTypes)
                res.Add(type, (from t in list where t.Education == type select t).ToList());

            return res;
        }

        public Dictionary<string, List<Worker>> EducationDivisionListResult() => EducationDivisionListResult(_workers);

        public Dictionary<string, int> EducatinoDivision(List<Worker> list)
        {
            var res = new Dictionary<string, int>();
            var MidProcessResult = EducationDivisionListResult(list);

            foreach (string key in MidProcessResult.Keys)
            {
                res.Add(key, MidProcessResult[key].Count());
            }

            return res;
        }

        public List<object> EducationDivisionChartData
        {
            get{
                var res = new List<object>();
                var input = EducatinoDivision(_workers);

                foreach(string type in input.Keys)
                {
                    res.Add(type);
                    res.Add(input[type]);
                }

                return res;
            }
            
        }

        public List<object> AgeEducationDivision
        {
            get
            {
                var res = new List<object>();
                var ageDivision = AgeDivision;
                Dictionary<string, int> educationDivision = null;

                foreach (string ageKey in ageDivision.Keys)
                {
                    educationDivision = EducatinoDivision(ageDivision[ageKey]);

                    foreach (string eduKey in educationDivision.Keys)
                    {
                        res.Add(eduKey);
                        res.Add(educationDivision[eduKey]);
                    }
                }

                return res;
            }
        }

        public List<Worker> Take(int startPosition, int amount)
        {
            List<Worker> res = new List<Worker>();

            for (int i = 0; i < amount; i++)
            {
                res.Add(_workers[startPosition + i]);
            }

            return res;
        }

    }
}
