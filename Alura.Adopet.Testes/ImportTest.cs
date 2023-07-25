using Alura.Adopet.Console.Comandos;
using Alura.Adopet.Console.Modelos;
using Alura.Adopet.Testes.Builder;
using Moq;

namespace Alura.Adopet.Testes
{
    public class ImportTest
    {
        [Fact]
        public async void QuandoListaVaziaNaoDeveChamarCreatPetAsync()
        {
            //Arrange
            List<Pet> listaDePet = new();
            var leitorDeArquivo = LeitorDeArquivosMockBuilder.GetMock(listaDePet);

            var httpClientPet = HttpClientPetMockBuilder.GetMock();

            var import = new Import(httpClientPet.Object, leitorDeArquivo.Object);
            string[] args = { "import", "lista.csv" };

            //Act
            await import.ExecutarAsync(args);

            //Assert
            httpClientPet.Verify(_=>_.CreatePetAsync(It.IsAny<Pet>()),Times.Never);
        }

        [Fact]
        public async Task QuandoArquivoNaoExistenteDeveGerarException()
        {
            //Arrange
            List<Pet> listaDePet = new();
            var leitor = LeitorDeArquivosMockBuilder.GetMock(listaDePet);
            leitor.Setup(_ => _.RealizaLeitura()).Throws<FileNotFoundException>();

            var httpClientPet = HttpClientPetMockBuilder.GetMock();

            string[] args = { "import", "lista.csv" };

            var import = new Import(httpClientPet.Object, leitor.Object);

            //Act+Assert
            await Assert.ThrowsAnyAsync<Exception>(() => import.ExecutarAsync(args));
        }

    }
}
