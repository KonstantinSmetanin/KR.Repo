using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KR.Core.Models
{
    public class Worker
    {
        public string LastName { get; set; } 
        public DateTime EmploymentDate { get; set; } 
        public string Education { get; set; } 
        public string Speciality { get; set; } 
        public string Gender { get; set; } 
        public string MilitaryServiceRelation { get; set; } 
        public DateTime BirthDate { get; set; } 
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
