using System;
using aera_core.Domain;
using aera_core.Persistencia;
using AutoBogus;

namespace AeraIntegrationTest.Builders
{
    public class ClienteDBBuilder : AutoFaker<ClienteDB>
    {
        public ClienteDBBuilder()
        {
            CustomInstantiator(f => new ClienteDB())
                .RuleFor(x => x.edu_lvl, faker => faker.Random.String2(2))
                .RuleFor(x => x.cpf, faker => faker.Random.String2(11, "123456789"))
                .RuleFor(x => x.zip_code, faker => faker.Random.String2(8, "123456789"))
                .RuleFor(x => x.cel_phone, () => null)
                .RuleFor(x => x.com_phone, () => null)
                .RuleFor(x => x.phone, () => null)
                .RuleFor(x => x.old_code, () => "O5000")
                .RuleFor(x => x.birth_date, () => DateTime.Today)
                .RuleFor(x => x.birth_hour, () => new TimeSpan(11, 50, 0));

        }
    }
}