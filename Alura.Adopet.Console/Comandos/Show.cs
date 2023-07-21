using Alura.Adopet.Console.Util;
using FluentResults;

namespace Alura.Adopet.Console.Comandos
{
    [DocComandoAttribute(instrucao: "show",
       documentacao: "adopet show <ARQUIVO> comando que exibe no terminal o conteúdo do arquivo importado.")]
    internal class Show:IComando
    {
        public Task<Result> ExecutarAsync(string[] args)
        {
            LeitorDeArquivo leitor = new LeitorDeArquivo(caminhoDoArquivoASerLido: args[1]);
            var listaDepets = leitor.RealizaLeitura();           

            return Task.FromResult(Result.Ok().
                WithSuccess(new SuccessWithPets(listaDepets, "Leitura do arquivo concluída!")));
        }

    }
}
