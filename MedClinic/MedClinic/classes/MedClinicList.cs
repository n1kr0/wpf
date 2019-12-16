using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedClinic.classes
{
   public class MedClinicList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Diagnoz { set; get; }
        public DateTime DateOfCreate { set; get; }
    }
}
