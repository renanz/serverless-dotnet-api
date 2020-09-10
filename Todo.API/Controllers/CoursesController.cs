using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Todo.API.Mappers;
using Todo.API.Models;
using Todo.API.Repositories;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("/api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private IAuthorRepository AuthorRepository
        {
            get { return _unitOfWork.AuthorRepository; }
        }
        
        private ICourseRepository CourseRepository
        {
            get { return _unitOfWork.CourseRepository; }
        }

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                          throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor([FromRoute] Guid authorId)
        {
            if (!AuthorRepository.Exists(authorId))
            {
                return NotFound();
            }

            var dbCourses = CourseRepository.GetAuthorCourses(authorId);
            var courses = dbCourses.Select(course => new CourseDto()
            {
                Id = course.Id,
                Description = course.Description,
                Title = course.Title,
                AuthorId = course.AuthorId
            });

            return Ok(courses);
        }

        [HttpGet("{id}", Name = "GetCourseForAuthor")]
        public ActionResult<CourseDto> GetCourseForAuthor([FromRoute] Guid authorId, [FromRoute] Guid id)
        {
            if (!AuthorRepository.Exists(authorId))
            {
                return NotFound();
            }

            var course = CourseRepository.GetAuthorCourse(authorId, id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(new CourseDto()
            {
                Id = course.Id,
                Description = course.Description,
                Title = course.Title,
                AuthorId = course.AuthorId
            });
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor(
            [FromRoute] Guid authorId,
            [FromBody] CreateCourseDto courseDto
        )
        {
            if (!AuthorRepository.Exists(authorId))
            {
                return NotFound();
            }

            if (courseDto == null)
            {
                return BadRequest();
            }

            var course = CourseMapper.ToCourse(courseDto, authorId);
            CourseRepository.Create(course);
            _unitOfWork.Commit();

            return CreatedAtRoute(
                "GetCourseForAuthor",
                new {authorId, id = course.Id},
                CourseMapper.ToCourseDto(course)
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourseForAuthor(
            [FromRoute] Guid authorId,
            [FromRoute] Guid id,
            [FromBody] UpdateCourseDto courseDto
        )
        {
            if (!AuthorRepository.Exists(authorId))
            {
                return NotFound();
            }

            if (courseDto == null)
            {
                return BadRequest();
            }

            var course = CourseRepository.GetAuthorCourse(authorId, id);

            if (course == null)
            {
                var courseToAdd = CourseMapper.ToCourse(courseDto, authorId);
                courseToAdd.Id = id;

                CourseRepository.Create(courseToAdd);

                return CreatedAtRoute(
                    "GetCourseForAuthor",
                    new {authorId, id = courseToAdd.Id},
                    CourseMapper.ToCourseDto(courseToAdd)
                );
            }

            CourseMapper.ToCourse(courseDto, authorId, course);

            CourseRepository.Update(course);
            _unitOfWork.Commit();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartiallyUpdateCourseForAuthor(Guid authorId, Guid id,
            JsonPatchDocument<UpdateCourseDto> patchDocument)
        {
            if (!AuthorRepository.Exists(authorId))
            {
                return NotFound();
            }

            var course = CourseRepository.GetAuthorCourse(authorId, id);
            if (course == null)
            {
                var courseDto = new UpdateCourseDto();
                patchDocument.ApplyTo(courseDto, ModelState);
                if (!TryValidateModel(courseDto))
                {
                    return ValidationProblem(ModelState);
                }

                var courseToAdd = CourseMapper.ToCourse(courseDto, authorId);
                courseToAdd.Id = id;
                CourseRepository.Create(courseToAdd);
                _unitOfWork.Commit();

                return CreatedAtRoute(
                    "GetCourseForAuthor",
                    new {authorId, id = courseToAdd.Id},
                    CourseMapper.ToCourseDto(courseToAdd)
                );
            }

            var courseToPatch = new UpdateCourseDto()
            {
                Description = course.Description,
                Title = course.Title,
            };
            patchDocument.ApplyTo(courseToPatch, ModelState);

            if (!TryValidateModel(courseToPatch))
            {
                return ValidationProblem(ModelState);
            }

            CourseMapper.ToCourse(courseToPatch, authorId, course);
            CourseRepository.Update(course);
            _unitOfWork.Commit();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCourseForAuthor(Guid authorId, Guid id)
        {
            if (!AuthorRepository.Exists(authorId))
            {
                return NotFound();
            }

            var course = CourseRepository.GetAuthorCourse(authorId, id);
            if (course == null)
            {
                return NotFound();
            }

            CourseRepository.Delete(course);
            _unitOfWork.Commit();

            return NoContent();
        }


        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult) options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}