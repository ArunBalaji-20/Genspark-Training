using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class SearchModel
    {
        public int? Id { get; set; }
        public string? PatientName { get; set; } 
        public Range<int>? PatientAge { get; set; }
    }

    public class Range<T>
    {
        public T? MinVal { get; set; }
        public T? MaxVal { get; set; }
    }
}
