﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimulationWebservice.Models;
using SimulationWebservice.Services;

namespace SimulationWebservice.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("[controller]")]
    public class GensetDataController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public GensetDataController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        /// <summary>
        /// Default GET method - Will (upon implmentation) return database details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            string feedbackMessage = "All-item GET method not yet implemented.";

            return feedbackMessage;
        }

        /// <summary>
        /// GET method for returning one dataset entry by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<string> GetAsync(string id)
        {
            GensetData data = new GensetData();

            // Try to retrieve data from database.
            try
            {
                data = await _cosmosDbService.GetDataAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            // Return data.
            string returnString = data.Id.ToString() + ": Power :" + data.GensetPower.ToString();

            return returnString;
        }

        /// <summary>
        /// POST method implementation creating a database Dataset entry.
        /// </summary>
        /// <param name="gensetData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateDataEntryAsync(GensetData gensetData)
        {
            // Post data to database.
            try
            {
                await _cosmosDbService.AddDataAsync(gensetData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            // return result.
            return CreatedAtAction("Genset Data POST", new { id = gensetData.Id }, gensetData);
        }

        // PUT: api/GensetData/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}