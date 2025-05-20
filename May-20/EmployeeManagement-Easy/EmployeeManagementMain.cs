using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    internal class EmployeeManagementMain
    {
        public static void Main(string[] args)
        {
            EmployeePromotion ep = new EmployeePromotion();
            ep.GetPromotionList();
            ep.DisplayPromotionList();
            ep.DisplayPositionInList();
            ep.PromotedList();
        }
    }
}
