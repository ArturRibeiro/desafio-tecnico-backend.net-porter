// namespace Meeting.Hub.Domain.Unit.Tests.Reservas.Fakers;
//
// public class DeveLancarExcecaoSeHouverConflitoParcialNoFimFaker : TheoryData<Sala, DateTime, DateTime, string>
// {
//     public DeveLancarExcecaoSeHouverConflitoParcialNoFimFaker()
//     {
//         var sala = new Sala("Sala Vendas");
//         sala.AddReserva(new DateTime(2025, 11, 2, 14, 0, 0), new DateTime(2025, 11, 2, 15, 0, 0), "Fulano");
//         
//         Add(sala, new DateTime(2025, 11, 2, 14, 30, 0), new DateTime(2025, 11, 2, 15, 30, 0), "Fulano de Tal");
//     }
// }