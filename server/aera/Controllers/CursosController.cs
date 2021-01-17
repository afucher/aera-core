using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly CursosServiço _cursosServiço;

        public CursosController(CursosServiço cursosServiço)
        {
            _cursosServiço = cursosServiço;
        }

        [HttpGet]
        public POUIListResponse<CursoDTO> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var opções = new OpçõesBusca
            {
                Página = page,
                LimitePágina = pageSize,
            };
            var cursos = _cursosServiço.ObterCursos(opções);
            var cursosDTO = cursos.Select(curso => CursoDTO.De(curso)).ToArray();

            return new POUIListResponse<CursoDTO>(cursosDTO, cursos.TemMaisItens);
        }
        
        [HttpGet("{id}")]
        public CursoDTO Get(int id)
        {
            var curso = _cursosServiço.Obter(id);
            return CursoDTO.De(curso);
        }
        
        [HttpPut("{id}")]
        public ActionResult<CursoDTO> Put(int id, [FromBody] CursoDTO curso)
        {
            if (!id.Equals(curso.id)) return BadRequest("Id não é válido");
            
            var cursoAtualizado = _cursosServiço.Atualizar(curso.ParaModelo());
            
            if (cursoAtualizado == null) return BadRequest("Erro ao salvar");
            
            return CursoDTO.De(cursoAtualizado);
        }
    }
}
