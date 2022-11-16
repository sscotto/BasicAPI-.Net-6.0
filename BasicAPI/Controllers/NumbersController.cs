using BasicAPI.Models;
using BasicAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumbersController : ControllerBase
    {

        private readonly ILogger<NumbersController> _logger;
        private readonly DataContainer _dataContainer;

        public NumbersController(ILogger<NumbersController> logger, DataContainer dataContainer)
        {
            _logger = logger;
            _dataContainer = dataContainer;
        }
          
        [HttpGet("All")]
        public ActionResult<NumberResponse> Get()
        {
            return Ok(new NumberResponse() { Message = "", Success = true, Numbers = _dataContainer.GetAllNumbers } );
        }
           
        [HttpGet("{x}")]
        public ActionResult<NumberResponse> Get(int x)
        {
            var numbers = _dataContainer.GetAllNumbers;

            bool exists = numbers.Any(y => y == x);         

            if (!exists) return NotFound(new NumberResponse() { Message = $"Number {x} was not found", Numbers = null, Success = false });
            return Ok(new NumberResponse() { Message = String.Empty, Numbers = new List<int>() { x }, Success = true });
        }

        [HttpGet("GreaterThan/{x}")]
        public ActionResult<NumberResponse> GetGreaterThan(int x)
        {
            var numbers = _dataContainer.GetAllNumbers.Where(number => number > x).ToList();

            if (!numbers.Any()) return NotFound(new NumberResponse() { Message = $"There are no numbers greater than {x}", Numbers = null, Success = false });
            return Ok(new NumberResponse() { Message = String.Empty, Numbers = numbers, Success = true });
        }

        [HttpPut("{oldValue}/{newValue}")]
        public ActionResult<NumberResponse> Update(int oldValue, int newValue)
        {
            var updated = _dataContainer.Update(oldValue, newValue, out bool duplicated);

            if (duplicated) return BadRequest(new NumberResponse() { Message = $"Number {newValue} already exist", Success = false });
            if (!updated) return NotFound(new NumberResponse() { Message = $"Number {oldValue} was not found", Success = updated });
            return Ok(new { Message = $"{oldValue} was replace for {newValue}", Success = updated, Numbers = _dataContainer.GetAllNumbers});
        }

        [HttpPost]
        public ActionResult<NumberResponse> Add(int x)
        {
            bool added = _dataContainer.TryAdd(x);

            if (!added) return BadRequest(new NumberResponse { Message = $"Number {x} already exists", Success = added });

            return Ok(new NumberResponse { Message = String.Empty, Success = added, Numbers = _dataContainer.GetAllNumbers });
        }

        [HttpDelete]
        public ActionResult<NumberResponse> Remove(int x)
        {
            _dataContainer.Remove(x);
          
            return Ok(new NumberResponse { Message = String.Empty, Success = true, Numbers = _dataContainer.GetAllNumbers });
        }
    }
}