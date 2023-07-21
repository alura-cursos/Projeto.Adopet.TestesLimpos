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
            return Task.FromResult(Result.Ok()
                .WithSuccess(new SuccessWithDocs(GerarDocumentacao(args))));
        }

        private IEnumerable<string> GerarDocumentacao(string[] parametros)
        {
            List<string> resultado = new List<string>();
            if (parametros.Length == 1)
            {
                resultado.Add($"Comando possíveis: ");
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
