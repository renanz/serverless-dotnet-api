using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Entities;
using Todo.API.Helpers;
using Todo.API.Mappers;
using Todo.API.Models;
using Todo.API.ResourceParameters;
using Todo.API.Repositories;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("/api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private IAuthorRepository AuthorRepository
        {
            get { return _unitOfWork.AuthorRepository; }
        }

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                          throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
            [FromQuery] AuthorsResourceParameters authorsResourceParameters)
        {
            var dbAuthors = AuthorRepository.GetAll(authorsResourceParameters);
            var authors = dbAuthors.Select(AuthorMapper.ToAuthorDto);
            return Ok(authors);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public ActionResult<AuthorDto> GetAuthor([FromRoute] Guid id)
        {
            var author = AuthorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(AuthorMapper.ToAuthorDto(author));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor([FromBody] CreateAuthorDto authorDto)
        {
            if (authorDto == null)
            {
                return BadRequest();
            }

            var author = AuthorMapper.ToAuthor(authorDto);

            AuthorRepository.Create(author);
            _unitOfWork.Commit();

            return CreatedAtRoute("GetAuthor", new {id = author.Id}, AuthorMapper.ToAuthorDto(author));
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor([FromRoute] Guid id)
        {
            var author = AuthorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            AuthorRepository.Delete(author);
            _unitOfWork.Commit();

            return NoContent();
        }
    }
}