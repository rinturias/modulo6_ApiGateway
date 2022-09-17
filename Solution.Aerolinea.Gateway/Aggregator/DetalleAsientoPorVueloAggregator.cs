using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using Solution.Aerolinea.Gateway.Dto;
using Solution.Aerolinea.Gateway.Utilitarios;

namespace Solution.Aerolinea.Gateway.Aggregator
{
    public class DetalleAsientoPorVueloAggregator: IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if (responses.Any(x => x.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
            }

            var vueloStr = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var planillaAsientoStr = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();


            var vueloObj = JsonConvert.DeserializeObject<VueloDto>(vueloStr);
            var productoObj = USerializeObjeto.deserializeJsom_Objeto(planillaAsientoStr);

            //var planilla = USerializeObjeto.ConvertirListObjeto<PlanillaAsientosDto>(productoObj);

            vueloObj.planillaAsientoVuelo = productoObj;

            var contentBody = JsonConvert.SerializeObject(vueloObj);

            var stringContent = new StringContent(contentBody)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");

        }
    }
}
