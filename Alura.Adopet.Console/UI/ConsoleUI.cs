using Alura.Adopet.Console.Comandos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                else
                {
                    
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
