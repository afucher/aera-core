using System;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Web;
using Newtonsoft.Json.Linq;
using FluentAssertions.Json;
using NSubstitute.Core;

namespace AeraIntegrationTest
{
    public static class ExtensaoFluentAssertionWeb
    {
        public static AndConstraint<HttpResponseMessageAssertions> SatisfyJTokenContent(
            this HttpResponseMessageAssertions assertions, Action<JToken> condicao)
        {
            assertions.Satisfy(async response =>
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                condicao(JToken.Parse(stringContent));
            });

            return new AndConstraint<HttpResponseMessageAssertions>(assertions);
        }

        public static void DeveSerEquivalenteAoJToken(this JToken jToken, JToken jTokenParaComparação)
        {
            jToken.Should().BeEquivalentTo(jTokenParaComparação);
        }

        public static JToken SerializarComoJToken(this object objeto)
        {
            if (objeto is string)
            {
                throw new ArgumentException("Já é string, ta errado");
            }

            return JToken.Parse(JsonSerializer.Serialize(objeto));
        }
    }
    
    
}