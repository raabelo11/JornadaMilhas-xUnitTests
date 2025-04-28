using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Fact]
        public void RetornaOfertaValidaQuandoDadosValidos()
        {
            //Padrão - AAA

            //Cenário - Arrange
            bool validacao = true;
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;

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

        [Fact]
        public void RetornaMensagemErroQuandoPrecoMenorQueZero()
        {
            //Arrange
            Rota rota = new Rota("Origem1", "Destino1");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 5), new DateTime(2024, 2, 14));
            double preco = -250;

            //Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }
    }
}