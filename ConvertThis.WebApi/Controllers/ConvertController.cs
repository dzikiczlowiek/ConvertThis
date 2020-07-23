using ConvertThis.Infrastructure;

using Microsoft.AspNetCore.Mvc;

namespace ConvertThis.WebApi.Controllers
{
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly IInputToByteArrayConverter _toByteArrayConverter;
        private readonly IConverterFactory _converterFactory;

        public ConvertController(IInputToByteArrayConverter toByteArrayConverter, IConverterFactory converterFactory)
        {
            _toByteArrayConverter = toByteArrayConverter;
            _converterFactory = converterFactory;
        }

        [HttpGet("{input}/To/{convertType}")]
        public IActionResult ConvertTo(string convertType, string input)
        {
            var converter = _converterFactory.Create(convertType);
            if(converter == null)
            {
                return BadRequest();
            }

            try
            {
                var byteArr = _toByteArrayConverter.Convert(input);
                var result = converter.Convert(byteArr);
                return Ok(result);
            }
            finally
            {
                _converterFactory.Release(converter);
            }
        }
    }
}
