using Alura.Adopet.Console.Comandos;
using Alura.Adopet.Console.Modelos;
using Alura.Adopet.Console.Servicos;
using Alura.Adopet.Console.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Testes
{
    public class ImportTest
    {
        [Fact]
        public async Task QuandoAPIEstiverNoArOImportDeveFuncionar()
        {
            //Arrange
            var leitorDeArquivo = new Mock<LeitorDeArquivo>(MockBehavior.Default,
                It.IsAny<string>());
            var listaDePet = new List<Pet>();

            var pet = new Pet(new Guid("456b24f4-19e2-4423-845d-4a80e8854a41"),
                  "Lima", TipoPet.Cachorro); //"456b24f4-19e2-4423-845d-4a80e8854a41;Lima Limão;1";
            listaDePet.Add(pet);

            leitorDeArquivo.Setup(_ => _.RealizaLeitura()).Returns(listaDePet);

            var httpClient = new AdopetAPIClientFactory().CreateClient("API");
            var httpClientPet = new HttpClientPet(httpClient);
            var import = new Import(httpClientPet, leitorDeArquivo.Object);
            string[] args = { "import", "lista.csv" };
            //Act
            await import.ExecutarAsync(args);

            //Assert
            var listaPet = await httpClientPet.ListPetsAsync();
            Assert.NotEmpty(listaPet);
        }

        [Fact]
        public async Task QuandoListaVaziaNuncaDeveChamarCreatePetAsync()
        {
            //Arrange
            var leitorDeArquivo = new Mock<LeitorDeArquivo>(MockBehavior.Default,
                It.IsAny<string>());
            var listaDePet = new List<Pet>();
            leitorDeArquivo.Setup(_ => _.RealizaLeitura()).Returns(listaDePet);

            //var httpClient = new AdopetAPIClientFactory().CreateClient("API");
            var httpClientPet = new Mock<HttpClientPet>(MockBehavior.Default,
                It.IsAny<HttpClient>());
                       
            var import = new Import(httpClientPet.Object, leitorDeArquivo.Object);
            string[] args = { "import", "lista.csv" };
            //Act
            await import.ExecutarAsync(args);

            //Assert
            httpClientPet.Verify(_ => _.CreatePetAsync(It.IsAny<Pet>()), Times.Never);
        }

        [Fact]
        public async Task QuandoArquivoNaoExistenteDeveGerarException()
        {
            //Arrange
            var leitor = new Mock<LeitorDeArquivo>(MockBehavior.Strict, It.IsAny<string>());
            var listaDePet = new List<Pet>();
            leitor.Setup(_ => _.RealizaLeitura()).Throws<FileNotFoundException>();

            var httpClientPet = new Mock<HttpClientPet>(MockBehavior.Default,
                It.IsAny<HttpClient>());

            string[] args = { "import", "lista.csv" };

            var import = new Import(httpClientPet.Object, leitor.Object);

            //Act+Assert
            await Assert.ThrowsAnyAsync<Exception>(() => import.ExecutarAsync(args));
        }

        [Fact]
        public async Task QuandoListaNaoVaziaDeveChamarCreatePetAsync()
        {
            //Arrange       
            var listaDePet = new List<Pet>();
            var pet = new Pet(new Guid("456b24f4-19e2-4423-845d-4a80e8854a99"),
                  "Lima", TipoPet.Cachorro);
            listaDePet.Add(pet);
          
            var leitor = new Mock<LeitorDeArquivo>(MockBehavior.Strict, It.IsAny<string>());
         
            var httpClientPet = new Mock<HttpClientPet>(MockBehavior.Default,
                          It.IsAny<HttpClient>());
                   
            leitor.Setup(_ => _.RealizaLeitura()).Returns(listaDePet);
         
            string[] args = { "import", "lista.csv" };
            var import = new Import(httpClientPet.Object,
                leitor.Object);

            //Act
            await import.ExecutarAsync(args);

            //Assert
            httpClientPet.Verify(_ => _.CreatePetAsync(It.IsAny<Pet>()), Times.Once);

        }

        [Fact]
        public async Task QuandoPetEstiverNoArquivoDeveSerImportado()
        {
            //Arrange       
            var listaDePet = new List<Pet>();
            var pet = new Pet(new Guid("456b24f4-19e2-4423-845d-4a80e8854a99"),
                  "Lima", TipoPet.Cachorro);
            listaDePet.Add(pet);

            var leitor = new Mock<LeitorDeArquivo>(MockBehavior.Strict, It.IsAny<string>());

            var httpClientPet = new Mock<HttpClientPet>(MockBehavior.Default,
                          It.IsAny<HttpClient>());

            leitor.Setup(_ => _.RealizaLeitura()).Returns(listaDePet);

            string[] args = { "import", "lista.csv" };
            var import = new Import(httpClientPet.Object,
                leitor.Object);

            //Act
           var resultado = await import.ExecutarAsync(args);

            //Assert
            Assert.True(resultado.IsSuccess);
            

        }

    }
}
