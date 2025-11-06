namespace Meeting.Hub.Application.CommandQuerys.Reservas.Imp.Responses;

public record ReservaMessageResponse(
    long ReservaId,
    DateTime Inicio,
    DateTime Fim,
    string ReservadoPor,
    long SalaId,
    string SalaNome);