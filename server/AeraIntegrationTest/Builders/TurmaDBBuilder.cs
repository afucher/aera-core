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
                .RuleFor(x => x.TurmaAlunos, () => new List<TurmaAluno>())
                .RuleFor(x => x.Alunos, () => new List<ClienteDB>());
        }
    }
}