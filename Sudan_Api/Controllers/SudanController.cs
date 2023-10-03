using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sudan_Api.Dto;
using Sudan_Api.Interface;
using Sudan_Api.Models;
using System.Drawing.Printing;

namespace Sudan_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SudanController : Controller
    {
        private readonly ISudanRepository _sudanRepository;
        private readonly IMapper _mapper;

        public SudanController(ISudanRepository sudanRepository, IMapper mapper)
        {
            _sudanRepository = sudanRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Sudan))]
        public IActionResult GetSudans()
        {
            var sudan = _mapper.Map<List<SudanDto>>(_sudanRepository.GetSudans());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(sudan);
        }
        [HttpGet("{sudanId}")]
        [ProducesResponseType(200, Type = typeof(Sudan))]
        [ProducesResponseType(400)]
        public IActionResult GetSudan(int sudanId)
        {
            if (!_sudanRepository.SudanExist(sudanId))
                return NotFound();

            var sudan = _mapper.Map<SudanDto>(_sudanRepository.GetSudan(sudanId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(sudan);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSudan([FromBody] SudanDto sudanCreate)
        {
            if (sudanCreate == null)
                return BadRequest(ModelState);

            var sudan = _sudanRepository.GetSudans()
                .Where(s => s.sudanName.Trim().ToUpper() == sudanCreate.sudanName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (sudan != null)
            {
                ModelState.AddModelError("", "Sudan is Already exists");
                return StatusCode(402, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sudanMap = _mapper.Map<Sudan>(sudanCreate);

            if (!_sudanRepository.CreateSudan(sudanMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully added");

        }
        [HttpPut("{sudanId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSudan(int sudanId, [FromBody] SudanDto updatedSudan)
        {
            if (updatedSudan == null)
                return BadRequest(ModelState);

            if (sudanId != updatedSudan.sudanId)
                return BadRequest(ModelState);

            if (!_sudanRepository.SudanExist(sudanId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sudanMap = _mapper.Map<Sudan>(updatedSudan);

            if (!_sudanRepository.UpdateSudan(sudanMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{sudanId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult DeleteSudan (int sudanId)
        {
            if (!_sudanRepository.SudanExist(sudanId))
                return NotFound();

            var sudanDelete = _sudanRepository.GetSudan(sudanId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_sudanRepository.DeleteSudan(sudanDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting");
                return StatusCode(500, ModelState);
            }
            return NoContent();


        }

    }
}
