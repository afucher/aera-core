using System;
using FluentAssertions;
using FluentAssertions.Web;
using Newtonsoft.Json.Linq;

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
    }
}