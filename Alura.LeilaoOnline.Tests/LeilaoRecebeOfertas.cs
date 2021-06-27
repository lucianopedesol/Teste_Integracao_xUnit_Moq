
using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOfertas
    {
        [Theory]
        [InlineData(4, new double[] {100, 1200, 1400, 1300})]
        [InlineData(2, new double[] {800, 900})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEsperada, double[] ofertas)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulana", leilao);
            foreach(var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
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
