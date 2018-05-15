using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    public class ActorController : Controller
    {
        private readonly IActorsService _actorsService;

        public ActorController(IActorsService actorsService)
        {
            _actorsService = actorsService;
        }

        /// <summary>
        /// Get all actors
        /// </summary>
        /// <returns>List of actors</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllActors()
        {
            var list = await _actorsService.GetAllActors();
            return Ok(AutoMapper.Mapper.Map<List<ActorResponse>>(list));
        }

        /// <summary>
        /// Get actor by id
        /// </summary>
        /// <param name="actorId">actor identifier</param>
        /// <returns>Actor if found</returns>
        [HttpGet("{actorId}")]
        public async Task<IActionResult> Get(int actorId)
        {
            Actor actor = await _actorsService.GetActorById(actorId);

            if(actor == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<ActorResponse>(actor));
        }

        /// <summary>
        /// Add new actor to repositorium
        /// </summary>
        /// <param name="actor">new actor</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ActorRequest actor)
        {
            await _actorsService.AddNewActor(AutoMapper.Mapper.Map<Actor>(actor));

            return Ok();
        }

        /// <summary>
        /// Update actor in repositorium
        /// </summary>
        /// <param name="actor">updated actor</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ActorRequest actor)
        {
            if(await _actorsService.UpdateActor(AutoMapper.Mapper.Map<Actor>(actor)))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete actor from repositorium
        /// </summary>
        /// <param name="actorId">actor identifier</param>
        /// <returns></returns>
        [HttpDelete("{actorId}")]
        public async Task<IActionResult> Delete(int actorId)
        {
            await _actorsService.RemoveActor(actorId);
            return Ok();
        }
    }
}