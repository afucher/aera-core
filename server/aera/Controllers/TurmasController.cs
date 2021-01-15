using System;
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
        private readonly CursosServiço _cursoServiço;

        public TurmasController(TurmasServiço turmasServiço, CursosServiço cursoServiço)
        {
            _turmasServiço = turmasServiço;
            _cursoServiço = cursoServiço;
        }

        [HttpGet("{id}")]
        public TurmaDTO Get(int id)
        {
            var turma = _turmasServiço.Obter(id);
            return TurmaDTO.De(turma);
        }
        
        [HttpPost()]
        public ActionResult<TurmaDTO> Post([FromBody] TurmaDTO turmaDTO)
        {
            var curso = _cursoServiço.Obter(turmaDTO.CursoId);
            var turma = turmaDTO.ParaModelo();
            turma.Curso = curso;
            var turmaCriada = _turmasServiço.Criar(turma);
            
            if (turmaCriada == null) return BadRequest("Erro ao salvar");
            
            return TurmaDTO.De(turmaCriada);
        }
        
        [HttpPut("{id}")]
        public ActionResult<TurmaDTO> Put(int id, [FromBody] TurmaDTO turma)
        {
            if (!id.Equals(turma.id)) return BadRequest("Id não é válido");
            var turmaAtualizada = _turmasServiço.Atualizar(turma.ParaModelo());
            
            if (turmaAtualizada == null) return BadRequest("Erro ao salvar");
            
            return TurmaDTO.De(turmaAtualizada);
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
            var turmasDTO = turmas.Select(TurmaDTO.De).ToArray();

            return new POUIListResponse<TurmaDTO>(turmasDTO, turmas.TemMaisItens);
        }
    }
}
