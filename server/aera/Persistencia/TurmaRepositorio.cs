using System.Collections.Generic;
using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace aera_core.Persistencia
{
    public class TurmaRepositorio : ITurmasPort
    {
        
        private readonly AplicaçãoContexto _contexto;
        public TurmaRepositorio(AplicaçãoContexto contexto)
        {
            _contexto = contexto;
        }

        public ListaPaginada<TurmaDB> ObterTurmas(OpçõesBusca opções)
        {
            var total = _contexto.Turmas.Count();
            var turmas =  _contexto.Turmas
                .Include(t => t.Curso)
                .Skip((opções.Página-1) * opções.LimitePágina)
                .Take(opções.LimitePágina)
                .ToList();
            return new ListaPaginada<TurmaDB>(turmas, total, opções.Página, opções.LimitePágina);
        }

        public TurmaDB Obter(int id)
        {
            return _contexto.Turmas
                .Include(t => t.Curso)
                .Include(t => t.Alunos)
                .FirstOrDefault(t => t.id == id);
        }

        public TurmaDB Criar(TurmaDB turma)
        {
            var turmaCriada = _contexto.Turmas.Add(turma);
            _contexto.SaveChanges();
            return turmaCriada.Entity;
        }

        public TurmaDB Atualizar(TurmaDB turmaParaAtualizar)
        {
            var turma = Obter(turmaParaAtualizar.id);
            if (turma == null) return null;
            
            turma.classes = turmaParaAtualizar.classes;
            turma.start_date = turmaParaAtualizar.start_date;
            turma.end_date = turmaParaAtualizar.end_date;
            turma.start_hour = turmaParaAtualizar.start_hour;
            turma.end_hour = turmaParaAtualizar.end_hour;
            turma.teacher_id = turmaParaAtualizar.teacher_id;
            _contexto.Turmas.Update(turma);
            _contexto.SaveChanges();

            return turma;
        }
    }
}