using Alura.Adopet.Console.Comandos;
using FluentResults;

namespace Alura.Adopet.Console.UI
{
    internal static class ConsoleUI
    {
        public static void ExibeResultado(Result result)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            try
            {
                if (result.IsFailed)
                {
                    ExibeFalha(result);
                }
            }   
            finally
            {
                System.Console.ForegroundColor = ConsoleColor.White;
            }

        }

        private static void ExibeFalha(Result result)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            var error = result.Errors.First();
            System.Console.WriteLine($"Aconteceu um exceção: {error.Message}");
        }
    }
}
