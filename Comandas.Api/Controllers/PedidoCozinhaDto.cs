namespace Comandas.Api.Controllers
{
    internal class PedidoCozinhaDto
    {
        public PedidoCozinhaDto()
        {
        }

        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public int NumeroMesa { get; set; }
        public string Item { get; set; }
    }
}