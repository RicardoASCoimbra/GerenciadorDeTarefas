using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Services.Interfaces.Tarefa;
using Tarefas.Services.ViewModels.Tarefa;

namespace Tarefas.Web.Controllers.Tarefa
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TarefaController_TesteController : ControllerBase
    {
        private readonly ITarefaAppService _tarefaAppService;

        public TarefaController_TesteController(ITarefaAppService tarefaAppService)
        {
            _tarefaAppService = tarefaAppService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var tarefas = await _tarefaAppService.GetAll();
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var tarefa = await _tarefaAppService.GetById(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpGet]
        [Route("GetByFilter")] 
        public async Task<IActionResult> GetByFilter(string nome, string responsavel)   
        {
            var tarefas = await _tarefaAppService.GetByFilter(nome, responsavel);

            if (tarefas == null || !tarefas.Any())
            {
                return NotFound();
            }

            return Ok(tarefas);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TarefaViewModel tarefaViewModel)
        {
            var createdTarefa = await _tarefaAppService.Create(tarefaViewModel);
            return CreatedAtAction(nameof(Get), new { id = createdTarefa.Id }, createdTarefa);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TarefaViewModel tarefaViewModel)
        {
            if (tarefaViewModel.Id == null)
            {
                return BadRequest();
            }

            await _tarefaAppService.Update(tarefaViewModel);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _tarefaAppService.Delete(id);
            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}