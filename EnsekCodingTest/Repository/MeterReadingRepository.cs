﻿using EnsekCodingTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Repository
{
    public class MeterReadingRepository: IMeterReadingRepository
    {
        private readonly EnsekDbContext _context;

        /// <summary>
        /// Constructor loading Dbcontext using DependencyInjection
        /// </summary>

        public MeterReadingRepository(EnsekDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is used to store the valid data from CSV file to MeterReading Database table
        /// </summary>

        public void CheckMeterReadings()
        {
            var readingCsvTable = new DataTable();
            using (var csvReader = new LumenWorks.Framework.IO.Csv.CsvReader(new StreamReader(@"CSVFiles\Meter_Reading.csv"), true))
            {
                readingCsvTable.Load(csvReader);
            }
            for (int i = 0; i < readingCsvTable.Rows.Count; i++)
            {
                var meterReading = new MeterReading();
                meterReading.AccountId = Convert.ToInt32(readingCsvTable.Rows[i][0]);
                meterReading.MeterReadingDateTime = DateTime.ParseExact(readingCsvTable.Rows[i][1].ToString(), "dd/MM/yyyy HH:mm", null);

                meterReading.MeterReadValue = readingCsvTable.Rows[i][2].ToString();

                // Reading.MeterReadingDateTime = _context.MeterReadings.OrderByDescending(p => p.MeterReadingDateTime).FirstOrDefault();

                if (_context.TestAccounts.FirstOrDefault(t => t.AccountId == meterReading.AccountId) != null
                     && (meterReading.MeterReadValue != string.Empty) && meterReading.MeterReadValue.Length <= 5
                     && meterReading.MeterReadingDateTime.ToString() != string.Empty)
                {
                    _context.MeterReadings.Add(meterReading);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// This method is used to get all the TestAccount deatils from the Database
        /// </summary>
        public List<TestAccount> GetTestAccountDeatils()
        {
            return _context.TestAccounts.ToList();
        }


        /// <summary>
        /// This method is used to get the MeterReading with Test account names based on AccountId
        /// </summary>
        public List<MeterReading> GetAccount(int accountId)
        {
            List<MeterReading> objmeterreading = new List<MeterReading>();

            objmeterreading = _context.MeterReadings.Join(_context.TestAccounts,
                m => m.AccountId,
                t => t.AccountId, (m, t) =>
                   new
                   {
                       AccountId = m.AccountId,
                       FirstName = t.FirstName,
                       LastName = t.LastName,
                       MeaterReadingDatetime = m.MeterReadingDateTime,
                       ReadingValue = m.MeterReadValue

                   }).OrderByDescending(m => m.MeaterReadingDatetime)
                    .Select(s => new MeterReading()
                    {
                        AccountId = s.AccountId,
                        TestAccount = new TestAccount() { FirstName = s.FirstName, LastName = s.LastName },
                        MeterReadingDateTime = s.MeaterReadingDatetime,
                        MeterReadValue = s.ReadingValue
                    }).ToList();

            return objmeterreading;
        }
    }
}

