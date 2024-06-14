namespace ACME.SchoolManagement.Core.Application.Request
{
    public class SendMailRequestBase
    {
        public int Tipo { get; set; }
        public int Subtipo { get; set; }
        public string? NumeroCliente { get; set; }
        public string? EmailCliente { get; set; }
        public string? CasoDeUso { get; set; }
        public string? IdLlamada { get; set; }
    }
}
