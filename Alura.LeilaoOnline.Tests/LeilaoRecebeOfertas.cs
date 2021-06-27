
using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOfertas
    {

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - metodo sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdEsperada = 1;
            var qtdObtido = leilao.Lances.Count();

            Assert.Equal(qtdEsperada, qtdObtido);
        }

        [Theory]
        [InlineData(4, new double[] {100, 1200, 1400, 1300})]
        [InlineData(2, new double[] {800, 900})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEsperada, double[] ofertas)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulana", leilao);
            var maria = new Interessada("maria", leilao);
            leilao.IniciaPregao();

            for(int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            //Act - metodo sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdObtido = leilao.Lances.Count();

            Assert.Equal(qtdEsperada, qtdObtido);
        }
    }
}
