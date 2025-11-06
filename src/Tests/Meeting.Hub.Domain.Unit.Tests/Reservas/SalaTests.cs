
using FluentValidation;

namespace Meeting.Hub.Domain.Unit.Tests.Reservas;

public class SalaTests
{
    [Theory(DisplayName = "Deve adicionar uma reserva válida quando não há conflitos")]
    [ClassData(typeof(DeveAdicionarUmaReservaValidaQuandoNaoHaConflitosFaker))]
    public void AddReserva_DeveAdicionarReserva_QuandoNaoHaConflito(Reserva reserva, Reserva reservaEsperada)
    {
        // Arrange
        reserva.Should().NotBeNull();
        
        // Act
        
        // Assert
        reserva.Should().NotBeNull();
        reserva.Sala.Should().NotBeNull();
        reserva.Sala.Nome.Should().Be(reservaEsperada.Sala.Nome);
        reserva.DataInicio.Should().Be(reservaEsperada.DataInicio);
        reserva.DataFim.Should().Be(reservaEsperada.DataFim);
        reserva.ReservadoPor.Should().Be(reservaEsperada.ReservadoPor);
            
    }

    // [Theory(DisplayName = "Deve lançar exceção se a data de início for igual ou maior que a final")]
    // [ClassData(typeof(DeveLancarExcecaoSeDataDeinicioForIgualOuMaiorQueFinalFaker))]
    // public void AddReserva_DeveLancarExcecao_QuandoInicioMaiorOuIgualFim(Sala sala, DateTime inicio, DateTime fim, string reservadoPor)
    // {
    //     // Arrange
    //
    //     // Act
    //     Action act = () => sala.AddReserva(inicio, fim, reservadoPor);
    //
    //     // Assert
    //     act.Should().Throw<ValidationException>()
    //         .Where(e => e.Errors.Any(err => err.ErrorMessage == "A data de início deve ser anterior à data de fim."));
    // }
    //
    // [Theory(DisplayName = "Deve lançar exceção se houver conflito total de horário")]
    // [ClassData(typeof(DeveLancarExcecaoSeHouverConflitoTotalDeHorarioFaker))]
    // public void AddReserva_DeveLancarExcecao_QuandoConflitoTotal(Sala sala, DateTime inicio, DateTime fim, string reservadoPor)
    // {
    //     // Arrange
    //
    //     // Act Reserva em conflito total
    //     Action act = () => sala.AddReserva(inicio, fim, reservadoPor);
    //
    //     // Assert
    //     act.Should().Throw<ValidationException>()
    //         .Where(e => e.Errors.Any(err => err.ErrorMessage == $"Conflito de horário para a sala {sala.Nome} Data: {sala.Inicio.Date.ToShortDateString()} horário de início: {sala.Inicio:HH:mm} e fim: {sala.Fim:HH:mm}."));
    // }
    //
    // [Theory(DisplayName = "Deve lançar exceção se houver conflito parcial no início")]
    // [ClassData(typeof(DeveLancarExcecaoSeHouverConflitoParcialNoinicioFaker))]
    // public void AddReserva_DeveLancarExcecao_QuandoConflitoParcialInicio(Sala sala, DateTime inicio, DateTime fim, string reservadoPor)
    // {
    //     // Arrange
    //     
    //     // Act - Nova reserva começa antes e termina dentro do período existente
    //     Action act = () => sala.AddReserva(inicio, fim, reservadoPor);
    //
    //     // Assert
    //     act.Should().Throw<ValidationException>()
    //         .Where(e => e.Errors.Any(err => err.ErrorMessage == $"Conflito de horário para a sala {sala.Nome} Data: {sala.Inicio.Date.ToShortDateString()} horário de início: {sala.Inicio:HH:mm} e fim: {sala.Fim:HH:mm}."));
    // }
    //
    // [Theory(DisplayName = "Deve lançar exceção se houver conflito parcial no fim")]
    // [ClassData(typeof(DeveLancarExcecaoSeHouverConflitoParcialNoFimFaker))]
    // public void AddReserva_DeveLancarExcecao_QuandoConflitoParcialFim(Sala sala, DateTime inicio, DateTime fim, string reservadoPor)
    // {
    //     // Arrange
    //
    //     // Act - Nova reserva inicia antes do fim da anterior
    //     Action act = () => sala.AddReserva(inicio, fim, reservadoPor);
    //
    //     // Assert
    //     act.Should().Throw<ValidationException>()
    //         .Where(e => e.Errors.Any(err => err.ErrorMessage == $"Conflito de horário para a sala {sala.Nome} Data: {sala.Inicio.Date.ToShortDateString()} horário de início: {sala.Inicio:HH:mm} e fim: {sala.Fim:HH:mm}."));
    // }
}