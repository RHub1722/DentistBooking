using System;
using System.Collections.Generic;

namespace Services
{
    public interface IRegisterPacient
    {
        List<KeyValuePair<int, string>> GetAllDoctors();
        List<KeyValuePair<int, string>> GetProcedures();
        bool ValidateDate(int docId, DateTime dateTime);
    }
}