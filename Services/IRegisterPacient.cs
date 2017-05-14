using System;
using System.Collections.Generic;
using Shared.DTO;

namespace Services
{
    public interface IRegisterPacient 
    {
        List<KeyValuePair<int, string>> GetAllDoctors();
        List<KeyValuePair<int, string>> GetProcedures();
        bool ValidateDate(int docId, DateTime dateTime);
        void Register(RegisterModel model);
        List<ResultListModel> GetAllRegisterProcedures(string doc, string proc, string user);
    }
}