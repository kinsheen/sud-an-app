using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Sudan_Api.Dto;
using Sudan_Api.Interface;
using Sudan_Api.Models;
using System.Reflection.Metadata.Ecma335;

namespace Sudan_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SudanCategoryController : Controller
    {
        private readonly ISudanCategoryRepository _sudanCategoryRepository;
        private readonly IMapper _mapper;

        public SudanCategoryController(ISudanCategoryRepository sudanCategoryRepository, IMapper mapper)
        {
            _sudanCategoryRepository = sudanCategoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SudanCategory))]
        public IActionResult GetSudanCategories()
        {
            var sudanCategory = _mapper.Map<List<SudanCategoryDto>>(_sudanCategoryRepository.GetSudanCategories());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(sudanCategory);
        }

        [HttpGet("{sudanCategoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(SudanCategory))]
        public IActionResult GetSudanCategory(int sudanCategoryId)
        {
            if (!_sudanCategoryRepository.SudanCategoryExist(sudanCategoryId))
                return NotFound();

            var sudanCategory = _mapper.Map<SudanCategoryDto>(_sudanCategoryRepository.GetSudanCategory(sudanCategoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(sudanCategory);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSudanCategory([FromBody] SudanCategoryDto createdSudanCategory)
        {
            if (createdSudanCategory == null)
                return BadRequest(ModelState);

            var sudanCategory = _sudanCategoryRepository.GetSudanCategories()
                .Where(s => s.sudanTitle.Trim().ToUpper() == createdSudanCategory.sudanTitle.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (sudanCategory != null)
            {
                ModelState.AddModelError("", "Sudan Category Already Exist");
                return StatusCode(402, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sudanCategoryMap = _mapper.Map<SudanCategory>(createdSudanCategory);

            if (!_sudanCategoryRepository.CreateSudanCategories(sudanCategoryMap))
            {
                ModelState.AddModelError("", "Something went wrong adding");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Added");
        }
        [HttpPut("{sudanCategoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSudanCategory(int sudanCategoryId, [FromBody] SudanCategoryDto updatedSudanCategory)
        {
            if (updatedSudanCategory == null)
                return BadRequest(ModelState);

            if (sudanCategoryId != updatedSudanCategory.sudanCatId)
                return BadRequest(ModelState);

            if (!_sudanCategoryRepository.SudanCategoryExist(sudanCategoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sudanCategoryMap = _mapper.Map<SudanCategory>(updatedSudanCategory);

            if (!_sudanCategoryRepository.UpdateSudanCategories(sudanCategoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{sudanCategoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSudanCategory(int sudanCategoryId)
        {
            if (!_sudanCategoryRepository.SudanCategoryExist(sudanCategoryId))
                return NotFound();

            var sudanCategory = _sudanCategoryRepository.GetSudanCategory(sudanCategoryId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_sudanCategoryRepository.DeleteSudanCategories(sudanCategory))
            {
                ModelState.AddModelError("", "Something went wrong deleting");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
