using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class AdminResultModel
    {
        public List<ResultListModel> gridModel = new List<ResultListModel>();
    }

    public class ResultListModel
    {
        public string DoctorName { get; set; }
        public string Procedure { get; set; }
        public DateTime Time { get; set; }
        public string PacentName { get; set; }
        public string PacentEmail { get; set; }
        public string PacentPhone { get; set; }
        public string PacentComment { get; set; }

    }
}
