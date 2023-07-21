using Alura.Adopet.Console.Modelos;
using Alura.Adopet.Console.Servicos;
using Alura.Adopet.Console.Util;
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
        public async Task<Result> ExecutarAsync(string[] args)
        {
            IEnumerable<Pet>? pets = await clientpet.ListPetsAsync();
            return Result.Ok()
                .WithSuccess(new SuccessWithPets(pets,"Consulta concluída!"));
        }

       
    }
}
