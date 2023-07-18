using System.Text.RegularExpressions;
using Alura.Adopet.Console.Modelos;

namespace Alura.Adopet.Console.Util
{
    public class LeitorDeArquivo
    {
        private string? caminhoDoArquivoASerLido;

        public LeitorDeArquivo(string? caminhoDoArquivoASerLido)
        {

            this.caminhoDoArquivoASerLido = caminhoDoArquivoASerLido;
        }
        public virtual List<Pet> RealizaLeitura()
        {
            if (string.IsNullOrEmpty(this.caminhoDoArquivoASerLido))
            {
                return null;
            }
            List<Pet> listaDePet = new List<Pet>();
            using (StreamReader sr = new StreamReader(caminhoDoArquivoASerLido))
            {
                while (!sr.EndOfStream)
                {
                    var linha = sr.ReadLine();
                    if (!ValidaFormato(linha))
                    {
                        return null;
                    }

                    // separa linha usando ponto e vírgula
                    string[]? propriedades = linha.Split(';');

                    // cria objeto Pet a partir da separação
                    Pet pet = new Pet(Guid.Parse(propriedades[0]),
                    propriedades[1],
                    int.Parse(propriedades[2]) == 1 ? TipoPet.Gato : TipoPet.Cachorro
                    );
                    listaDePet.Add(pet);
                }
            }

            return listaDePet;
        }

        private bool ValidaFormato(string? linha)
        {
            Regex regex = new Regex(@"^[\da-fA-F]{8}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{12};[^;]+;\d+$");
            return regex.IsMatch(linha!);
        }
    }
}
