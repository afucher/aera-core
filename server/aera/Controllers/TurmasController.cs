using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.POUIHelpers;
using Microsoft.AspNetCore.Mvc;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly TurmasServiço _turmasServiço;

        public TurmasController(TurmasServiço turmasServiço)
        {
            _turmasServiço = turmasServiço;
        }

        [HttpGet("{id}")]
        public TurmaDTO Get(int id)
        {
            var turma = _turmasServiço.Obter(id);
            return new TurmaDTO
            {
                id = turma.id,
                Curso = turma.Curso.name,
                DataInicial = turma.start_date.ToString("yyyy-MM-dd"),
                DataFinal = turma.end_date.ToString("yyyy-MM-dd")
            };
        }

        [HttpGet]
        public POUIListResponse<TurmaDTO> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var opções = new OpçõesBusca
            {
                Página = page,
                LimitePágina = pageSize,
            };
            var turmas = _turmasServiço.ObterTurmas(opções);
            var turmasDTO = turmas.Select(turma => new TurmaDTO
            {
                id = turma.id,
                Curso = turma.Curso.name,
                DataInicial = turma.start_date.ToString("yyyy-MM-dd"),
                DataFinal = turma.end_date.ToString("yyyy-MM-dd")
            }).ToArray();

            return new POUIListResponse<TurmaDTO>(turmasDTO, turmas.TemMaisItens);
        }
    }
}
