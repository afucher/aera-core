using System;
using System.Collections.Generic;
using aera_core.Domain;
using aera_core.Persistencia;
using AutoBogus;

namespace AeraIntegrationTest.Builders
{
    public class TurmaDBBuilder : AutoFaker<TurmaDB>
    {
        public TurmaDBBuilder()
        {
            CustomInstantiator(f => new TurmaDB())
                .RuleFor(x => x.TurmaAlunos, () => null)
                .RuleFor(x => x.Alunos, () => null)
                .RuleFor(x => x.Curso, () => new CursoDBBuilder().Generate())
                .RuleFor(x => x.start_date,() => new DateTime(2021,1,1,0,0,0))
                .RuleFor(x => x.end_date,() => new DateTime(2021,1,1,0,0,0));
        }
    }
}