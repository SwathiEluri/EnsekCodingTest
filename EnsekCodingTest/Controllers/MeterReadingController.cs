﻿using AutoMapper;
using EnsekCodingTest.Models;
using EnsekCodingTest.Models.Dtos;
using EnsekCodingTest.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly ILogger<MeterReadingController> _logger;
        private readonly IMeterReadingRepository _repo;
        private readonly EnsekDbContext _context;
        private readonly IMapper _mapper;

        public MeterReadingController(ILogger<MeterReadingController> logger, IMeterReadingRepository repo, EnsekDbContext context, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        public IActionResult AddCsvFileToDb()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.CheckMeterReadings();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, null);
            }
            return Created("Records inserted successfully", null);
        }

        [HttpGet]
        [Route("get-test-account-data")]
        public IActionResult GetAllTestAccountRecords()
        {
            List<TestAccount> objTestAccountList = new List<TestAccount>();
            try
            {
                objTestAccountList = _repo.GetTestAccountDeatils();

                var objDtoList = new List<TestAccountDto>();

                foreach (var item in objTestAccountList)
                {
                    objDtoList.Add(_mapper.Map<TestAccountDto>(item));
                }
                return Ok(objDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                return StatusCode(500);
            }
        }
    }
}
