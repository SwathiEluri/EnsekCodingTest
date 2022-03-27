using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Data
{
    public interface IDbInitialiser
    {
        void LoadTestCsvToDb();
    }
}
