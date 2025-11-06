// namespace Meeting.Hub.Domain.Unit.Tests.Reservas.Fakers;
//
// public class DeveLancarExcecaoSeHouverConflitoParcialNoinicioFaker : TheoryData<Sala, DateTime, DateTime, string>
// {
//     public DeveLancarExcecaoSeHouverConflitoParcialNoinicioFaker()
//     {
//         var sala = new Sala("Sala Financeiro");
//         sala.AddReserva(new DateTime(2025, 11, 2, 10, 0, 0), new DateTime(2025, 11, 2, 11, 0, 0), "Fulano");
//         
//         Add(sala, new DateTime(2025, 11, 2, 9, 30, 0), new DateTime(2025, 11, 2, 10, 30, 0), "Fulano de Tal");
//     }
// }