using System;
using System.IO;

using ConvertThis.Infrastructure;
using ConvertThis.WebApi.Models;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ConvertThis.WebApi.Controllers
{
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ConvertController : ControllerBase
    {
        private readonly IInputToByteArrayConverter _toByteArrayConverter;
        private readonly IConverterFactory _converterFactory;

        public ConvertController(IInputToByteArrayConverter toByteArrayConverter, IConverterFactory converterFactory)
        {
            _toByteArrayConverter = toByteArrayConverter;
            _converterFactory = converterFactory;
        }

        [HttpGet("{input}/to/{converterType}")]
        public IActionResult ConvertTo([FromRoute]ConvertInputRequestModel request)
        {
            var converter = _converterFactory.Create(request.ConverterType);
            if (converter == null)
            {
                return BadRequest();
            }

            try
            {
                var byteArr = _toByteArrayConverter.Convert(request.Input);
                var result = converter.Convert(byteArr);
                return Ok(result);
            }
            finally
            {
                _converterFactory.Release(converter);
            }
        }



        [HttpPost("to")]
        public IActionResult ConvertTo2([FromBody] ConvertInputRequestModel request)
        {
            var converter = _converterFactory.Create(request.ConverterType);
            if (converter == null)
            {
                return BadRequest();
            }

            try
            {
                var byteArr = _toByteArrayConverter.Convert(request.Input);
                var result = converter.Convert(byteArr);
                return Ok(result);
            }
            finally
            {
                _converterFactory.Release(converter);
            }
        }

        [HttpGet("Info")]
        public IActionResult Info()
        {
            using Stream stream = this.GetType().Assembly.
                GetManifestResourceStream("ConvertThis.WebApi.meta._gitinfo.txt");
            using StreamReader sr = new StreamReader(stream);

            return Ok(sr.ReadToEnd());
        }
    }
}
