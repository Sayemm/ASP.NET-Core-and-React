﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviesAPI.Entities;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    public class GenresController: ControllerBase
    {
        private readonly IRepository _repository;

        public GenresController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]                // domain/api/genres
        [HttpGet("list")]        // domain/api/genres/list
        [HttpGet("/allgenres")]  // domain/allgenres
        public async Task<ActionResult<List<Genre>>> Get()
        {
            return  await _repository.GetAllGenres();
        }

        [HttpGet("{Id:int}")]
        public ActionResult<Genre> Get(int Id, [BindRequired] string name)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = _repository.GetGenreById(Id);
            
            if(genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Genre genre)
        {
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}
