using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.POUIHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessoresController : ControllerBase
    {
        private readonly ProfessoresServiço _professoresServiço;

        public ProfessoresController(ProfessoresServiço professoresServiço)
        {
            _professoresServiço = professoresServiço;
        }

        [HttpGet]
        public POUIListResponse<ProfessorDTO> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var opções = new OpçõesBusca
            {
                Página = page < 1 ? 1 : page,
                LimitePágina = pageSize == 0 ? 10 : pageSize,
            };
            var professores = _professoresServiço.ObterTodos(opções);

            return new POUIListResponse<ProfessorDTO>(
            professores.Select(c => new ProfessorDTO{id = c.Id, nome = c.Nome}).ToArray(), 
                professores.TemMaisItens);
        }
        
        [HttpGet("{id}")]
        public ClienteDTO Get(int id)
        {
            var professor = _professoresServiço.Obter(id);
            return new ClienteDTO
            {
                id = professor.Id,
                nome = professor.Nome
            };
        }
    }
}
