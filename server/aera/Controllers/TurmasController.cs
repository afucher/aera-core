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
        private readonly ProfessoresServiço _professoresServiço;
        private readonly ClientesServiço _clientesServiço;

        public TurmasController(TurmasServiço turmasServiço, CursosServiço cursoServiço, ProfessoresServiço professoresServiço, ClientesServiço clientesServiço)
        {
            _turmasServiço = turmasServiço;
            _cursoServiço = cursoServiço;
            _professoresServiço = professoresServiço;
            _clientesServiço = clientesServiço;
        }

        [HttpGet("{id}")]
        public TurmaDTO Get(int id)
        {
            var turma = _turmasServiço.Obter(id);
            var professor = _professoresServiço.Obter(turma.teacher_id);
            
            return TurmaDTO.De(turma, professor);
        }
        
        [HttpPost]
        public ActionResult<TurmaDTO> Post([FromBody] TurmaDTO turmaDTO)
        {
            var curso = _cursoServiço.Obter(turmaDTO.CursoId);
            var professor = _professoresServiço.Obter(turmaDTO.ProfessorId);
            if (professor == null) return BadRequest("Professor não existe");
            var turma = turmaDTO.ParaModelo();
            turma.Curso = curso;
            var turmaCriada = _turmasServiço.Criar(turma);
            
            if (turmaCriada == null) return BadRequest("Erro ao salvar");
            
            return TurmaDTO.De(turmaCriada, professor);
        }
        
        [HttpPut("{id}")]
        public ActionResult<TurmaDTO> Put(int id, [FromBody] TurmaDTO turma)
        {
            if (!id.Equals(turma.Id)) return BadRequest("Id não é válido");
            var professor = _professoresServiço.Obter(turma.ProfessorId);
            if (professor == null) return BadRequest("Professor não existe");
            var turmaAtualizada = _turmasServiço.Atualizar(turma.ParaModelo());
            
            if (turmaAtualizada == null) return BadRequest("Erro ao salvar");
            
            return TurmaDTO.De(turmaAtualizada, professor);
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
            var turmasDTO = turmas.Select(t => TurmaDTO.De(t, null)).ToArray();

            return new POUIListResponse<TurmaDTO>(turmasDTO, turmas.TemMaisItens);
        }
        
        [HttpPost("{id}/matricular")]
        public ActionResult<TurmaDTO> Matricular(int id, [FromBody] int alunoId)
        {
            var aluno = _clientesServiço.Obter(alunoId);
            if (aluno == null) return BadRequest("Aluno não existe");

            var turma = _turmasServiço.Obter(id);
            if (turma == null) return BadRequest("Turma não existe");
            var turmaAtualizada = _turmasServiço.MatricularAluno(turma, aluno);
            
            if (turmaAtualizada == null) return BadRequest("Erro ao salvar");
            var professor = _professoresServiço.Obter(turmaAtualizada.teacher_id);
            
            return TurmaDTO.De(turmaAtualizada, professor);
        }
    }
}
