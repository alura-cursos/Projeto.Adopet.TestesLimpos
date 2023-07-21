using Alura.Adopet.Console.Modelos;
using Alura.Adopet.Console.Servicos;
using Alura.Adopet.Console.Util;
using FluentResults;

namespace Alura.Adopet.Console.Comandos
{
    [DocComandoAttribute(instrucao: "import",
        documentacao: "adopet import <ARQUIVO> comando que realiza a importação do arquivo de pets.")]
    public class Import:IComando
    {
        private readonly HttpClientPet clientpet;
        private readonly LeitorDeArquivo leitorDeArquivo;

        public Import(HttpClientPet clientpet,LeitorDeArquivo leitorDeArquivo)
        {
            this.clientpet = clientpet;
            this.leitorDeArquivo = leitorDeArquivo;
        }
        public async Task<Result> ExecutarAsync(string[] args)
        {
           return await this.ImportacaoArquivoPetAsync(caminhoDoArquivoDeImportacao: args[1]);
        }

        private async Task<Result> ImportacaoArquivoPetAsync(string caminhoDoArquivoDeImportacao)
        {
            try
            {
                IEnumerable<Pet> listaDePet = leitorDeArquivo.RealizaLeitura();
                foreach (var pet in listaDePet)
                {
                              
                    await clientpet.CreatePetAsync(pet);                 
                   
                }
                return Result.Ok().WithSuccess(new SuccessWithPets(listaDePet,
                    "Importação realizada com sucesso!"));
            }
            catch (Exception exception)
            {
                return Result.Fail(new Error("Importação Falhou!").CausedBy(exception));
            }

            
        }
    }
}
