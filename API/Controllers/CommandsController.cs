using API.Data;
using API.DTOs;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandItem = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandItem);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandItem);

            return CreatedAtRoute(nameof(GetCommandById), new { id = commandReadDto.Id }, commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commandItem);
            _repository.UpdateCommand(commandItem); // Redundant

            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandItem);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandItem);
            _repository.UpdateCommand(commandItem); // Redundant
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandItem);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
