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
         
            IEnumerable<Pet> listaDePet = leitorDeArquivo.RealizaLeitura();
            foreach (var pet in listaDePet)
            {
                System.Console.WriteLine(pet);
                try
                {
                       await clientpet.CreatePetAsync(pet);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
            return Result.Ok().WithSuccess(new SuccessWithPets(listaDePet));
            
        }
    }
}
