using System;
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
                .OrderByDescending(t => t.end_date)
                .Include(t => t.Curso)
                .Skip((opções.Página-1) * opções.LimitePágina)
                .Take(opções.LimitePágina)
                .ToList();
            return new ListaPaginada<TurmaDB>(turmas, total, opções.Página, opções.LimitePágina);
        }

        public IReadOnlyCollection<TurmaDB> ObterTurmasDosAlunos(List<int> idsAlunos)
        {
            return _contexto.Matriculas
                .Include(m => m.Turma)
                    .ThenInclude(t => t.Alunos)
                .Include(m => m.Turma.Curso)
                .Where(m => idsAlunos.Contains(m.ClienteId))
                .Select(m => m.Turma)
                .ToList();
        }

        public TurmaDB Obter(int id)
        {
            return _contexto.Turmas
                .Include(t => t.Curso)
                .Include(t => t.Alunos)
                .Include(t => t.Professor)
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

        public TurmaDB MatricularAluno(TurmaDB turmaParaMatricular, int clienteId)
        {
            var turma = Obter(turmaParaMatricular.id);
            if (turma == null) return null;
            var aluno = _contexto.Clientes.FirstOrDefault(c => c.id == clienteId);
            if (aluno == null) return null;
            turma.Alunos.Add(aluno);
            
            _contexto.Turmas.Update(turma);
            _contexto.SaveChanges();

            return turma;
        }

        public IReadOnlyCollection<TurmaAluno> MatriculasDoAluno(int clientId)
        {
            return _contexto.Matriculas
                            .Include(m => m.Turma)
                            .Include(m => m.Turma.Curso)
                            .Include(m => m.Pagamentos)
                            .Where(m => m.ClienteId == clientId)
                            .ToList();
        }

        public IReadOnlyCollection<TurmaDB> ObterPagamentos(DateTime De, DateTime Até)
        {
            return _contexto.Turmas
                .Include(t => t.Curso)
                .Include(t => t.TurmaAlunos)
                .ThenInclude(m => m.Pagamentos.Where(p => p.Paid == null || p.Paid == false).Where(p => p.DueDate <= Até && p.DueDate >= De))
                .Include(t => t.TurmaAlunos)
                .ThenInclude(m => m.Cliente)
                .Where(t => t.TurmaAlunos.Any(m => m.Pagamentos.Any(p => p.Paid == false && p.DueDate <= Até && p.DueDate >= De)))
                .ToList();
        }
    }
}