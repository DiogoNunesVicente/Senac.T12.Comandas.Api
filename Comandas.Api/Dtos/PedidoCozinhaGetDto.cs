﻿namespace Comandas.Api.Dtos
{
    //Data tranfer object
    public class PedidoCozinhaGetDto
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public string Titulo { get; set; } = default!;

    }
}