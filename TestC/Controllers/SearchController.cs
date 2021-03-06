﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestC.Models;
using TestC.Services.Interface;

namespace TestC.Controllers
{
    public class SearchController : ApiController
    {
        private IPropService _propService;
        public SearchController(IPropService propService)
        {
            _propService = propService;
        }
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public IHttpActionResult Get([FromUri]ParametersModel model)
        {
            return Ok(JsonConvert.SerializeObject(_propService.Search(model)));
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}