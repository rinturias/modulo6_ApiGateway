namespace Solution.Aerolinea.Gateway.Dto
{
    public class VueloDto
    {

        public DateTime horaLLegada { get; set; }
        public decimal precio { get; set; }
        public DateTime fecha { get; set; }
        public Guid vueloId { get; set; }

        public dynamic? planillaAsientoVuelo { get; set; }
       
    }
}
