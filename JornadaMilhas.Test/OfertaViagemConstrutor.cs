using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", 100, true)]
        [InlineData(null, "DestinoTeste", "2025-03-17", "2025-03-22", 100, true)]
        [InlineData("Vitória", "Acre", "2025-03-01", "2025-03-01", 0, false)]
        [InlineData("São Paulo", "Minas Gerais", "2025-04-01", "2025-04-05", -500, false)]
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            //Padrão - AAA

            //Cenário - Arrange
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            //Ação - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Validação - Assert
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemErroQuandoRotaOuPeriodoInvalido()
        {
            //Cenário
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;

            //Ação
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Validação
            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemErroQuandoPeriodoInvalido()
        {
            //Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 5), new DateTime(2024, 2, 1));
            double preco = 100.0;

            //Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert
            Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        [InlineData(-1)]
        public void RetornaMensagemErroQuandoPrecoMenorOuIgualAZero(double preco)
        {
            //Arrange
            Rota rota = new Rota("São Paulo", "Salvador");
            Periodo periodo = new Periodo(new DateTime(2025, 4, 5), new DateTime(2025, 4, 14));

            //Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }
    }
}