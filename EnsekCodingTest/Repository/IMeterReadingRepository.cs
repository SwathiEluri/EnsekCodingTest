using EnsekCodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Repository
{
    public interface IMeterReadingRepository
    {
        void CheckMeterReadings();
        List<TestAccount> GetTestAccountDeatils();
        List<MeterReading> GetAccount(int accountId);
    }
}
