using System;
using System.Collections.Generic;
using aera_core.Domain;
using aera_core.Persistencia;
using AutoBogus;

namespace AeraIntegrationTest.Builders
{
    public class CursoDBBuilder : AutoFaker<CursoDB>
    {
        public CursoDBBuilder()
        {
            CustomInstantiator(f => new CursoDB())
                .RuleFor(x => x.Turmas, () => new List<TurmaDB>());
        }
    }
}