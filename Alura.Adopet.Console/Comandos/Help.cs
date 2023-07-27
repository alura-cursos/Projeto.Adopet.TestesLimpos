using Alura.Adopet.Console.Util;
using FluentResults;
using System.Reflection;

namespace Alura.Adopet.Console.Comandos
{
    [DocComandoAttribute(instrucao: "help",
     documentacao: "adopet help comando que exibe informações da ajuda. \n" +
        "adopet help <NOME_COMANDO> para acessar a ajuda de um comando específico.")]
    internal class Help:IComando
    {
        private Dictionary<string, DocComandoAttribute> docs;

        public Help()
        {
            docs = DocumentacaoDoSistema.ToDictionary(Assembly.GetExecutingAssembly());
        }

        public Task<Result> ExecutarAsync(string[] args)
        {
            try
            {
                  return Task.FromResult(Result.Ok()
                    .WithSuccess(new SuccessWithDocs(this.GerarDocumentacao(parametros: args))));
            }
            catch (Exception exception)
            {
                return Task.FromResult(Result.Fail(new Error("Exibição da documentação falhou!").CausedBy(exception)));
            }
        }

        private IEnumerable<string> GerarDocumentacao(string[] parametros)
        {
            List<string> resultado = new List<string>();
            // se não passou mais nenhum argumento mostra help de todos os comandos
            if (parametros.Length == 1)
            {
                foreach (var doc in docs.Values)
                {
                    resultado.Add(doc.Documentacao);
                }
            }
            // exibe o help daquele comando específico
            else if (parametros.Length == 2)
            {
                string comandoASerExibido = parametros[1];
                if (docs.ContainsKey(comandoASerExibido))
                {
                    var comando = docs[comandoASerExibido];
                    resultado.Add(comando.Documentacao);
                }
                else
                {
                    resultado.Add("Comando não encontrado!");
                }

            }
            return resultado;
        }
    }
}
