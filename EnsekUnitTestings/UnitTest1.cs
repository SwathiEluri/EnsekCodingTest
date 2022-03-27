using System;
using Xunit;
using EnsekCodingTest.Models;
using EnsekCodingTest.Repository;
using EnsekCodingTest.Controllers;
using System.Collections.Generic;

namespace EnsekUnitTestings
{
    public class UnitTest1
    {
        [Fact]
        public void TestMeterReadingEntry()
        {
            var meterReadings = new List<MeterReading>()
            {
                new MeterReading {AccountId = 501, MeterReadingDateTime= DateTime.Now, MeterReadValue = "X3425"},
                new MeterReading {AccountId = 133, MeterReadingDateTime= DateTime.Now, MeterReadValue = "10091" },
                new MeterReading {AccountId = 127, MeterReadingDateTime= DateTime.Now, MeterReadValue = "156" },
                new MeterReading {AccountId = 1229, MeterReadingDateTime= DateTime.Now, MeterReadValue = "289" }
            };

            var sampleAccounts = new List<TestAccount>
            {
                new TestAccount() {AccountId=501,FirstName="Jhon",LastName="Smith" },
                new TestAccount() {AccountId=127,FirstName="Jeorge",LastName="Luies" },
                new TestAccount() {AccountId=1229,FirstName="Kate",LastName="Jordon" }
            };
        }
    }
}
