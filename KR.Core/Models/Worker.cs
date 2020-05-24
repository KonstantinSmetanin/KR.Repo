using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KR.Core.Models
{
    public class Worker
    {
        public string LastName { get; set; } //Yeap
        public DateTime EmploymentDate { get; set; } //yeap
        public string Education { get; set; } //yeap
        public string Speciality { get; set; } //yeap
        public string Gender { get; set; } //yeap
        public string MilitaryServiceRelation { get; set; } //yeap
        public DateTime BirthDate { get; set; } //yeap
        public ulong ID { get; set; }
        public int Age
        {
            get
            {
                return new DateTime(DateTime.Now.Subtract(BirthDate).Ticks).Year - 1;
            }
        }

        public string GetEmploymentDateAsAString
        {
            get
            {
                return $"{EmploymentDate.Month}-{EmploymentDate.Year}";
            }
        }
    }
}
