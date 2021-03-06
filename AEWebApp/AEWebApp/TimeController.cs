﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AEWebApp
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : Controller
    {
        [HttpGet]
        public ActionResult GET()
        {
            var errorParam = Request.QueryString.Value;
            if (errorParam.Equals("?500"))
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            if (errorParam.Equals("?timeout"))
            {
                System.Threading.Thread.Sleep(5000);
            }
            
            return Ok(DateTime.Now.ToString("H:mm:ss tt"));
        }
    }
}
