using Alura.Adopet.Console.Comandos;
using Alura.Adopet.Console.Servicos;
using Alura.Adopet.Console.Util;

var httpClientPet = new HttpClientPet(new AdopetAPIClientFactory().CreateClient("adopet"));
var leitorDeArquivos = new LeitorDeArquivo(caminhoDoArquivoASerLido: args[1]);
Dictionary<string, IComando> comandosDoSistema = new()
{
    {"help",new Help() },
    {"import",new Import(httpClientPet,leitorDeArquivos)},
    {"list",new List(httpClientPet) },
    {"show",new Show(leitorDeArquivos) },
};

Console.ForegroundColor = ConsoleColor.Green;
try
{    
    string comando = args[0].Trim();
    if (comandosDoSistema.ContainsKey(comando))
    {
        IComando? cmd = comandosDoSistema[comando];
        await cmd.ExecutarAsync(args);
    }
    else
    {
        Console.WriteLine("Comando inválido!");
    }         
}
catch (Exception ex)
{
    // mostra a exceção em vermelho
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Aconteceu um exceção: {ex.Message}");
}
finally
{
    Console.ForegroundColor = ConsoleColor.White;
}
