﻿using Alura.Adopet.Console.Modelos;
using Alura.Adopet.Console.Servicos;
using FluentResults;

namespace Alura.Adopet.Console.Comandos
{
    [DocComandoAttribute(instrucao: "list",
      documentacao: "adopet list comando que exibe no terminal o conteúdo cadastrado na base de dados da AdoPet.")]
    internal class List: IComando
    {
        private readonly HttpClientPet clientpet;

        public List(HttpClientPet clientpet)
        {
            this.clientpet = clientpet;
        }
        public Task<Result> ExecutarAsync(string[] args)
        {
            return this.ListaDadosPetsDaAPIAsync();
        }

        private async Task<Result> ListaDadosPetsDaAPIAsync()
        {

            IEnumerable<Pet>? pets = await clientpet.ListPetsAsync();
            System.Console.WriteLine("----- Lista de Pets importados no sistema -----");
            foreach (var pet in pets)
            {
                System.Console.WriteLine(pet);
            }
            return Result.Ok();
        }

    }
}
