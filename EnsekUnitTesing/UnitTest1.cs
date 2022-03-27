using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EnsekCodingTest.Models;

namespace EnsekUnitTesing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            List<MeterReading> lst = new List<MeterReading>
            {
                new MeterReading
                {
                   AccountId=2199,
                   TestAccount=new TestAccount{AccountId=2199,FirstName="Jhon",LastName="Smith"},
                   MeterReadingDateTime=System.DateTime.Now,
                   MeterReadValue="1945"
                },
                 new MeterReading
                {
                   AccountId=2200,
                   TestAccount=new TestAccount{AccountId=2200,FirstName="Jack",LastName="William"},
                   MeterReadingDateTime=System.DateTime.Now,
                   MeterReadValue="00198"
                },
            };
            //    GuestiaCodingTask.GetGuestList obj = new GetGuestList();

            //EnsekCodingTest.Repository.MeterReadingRepository objTest =

            //    obj.UnregisterdGuests();
            
        }
    }
}
