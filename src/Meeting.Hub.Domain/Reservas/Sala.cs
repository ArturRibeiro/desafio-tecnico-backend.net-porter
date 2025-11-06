namespace Meeting.Hub.Domain.Reservas;

public class Sala
{
    private readonly List<Reserva> _reservas = new();

    public long? Id { get; private set; }
    public string Nome { get; private set; }
    public IReadOnlyCollection<Reserva> Reservas => _reservas;

    protected Sala() { }

    public Sala(string nome) => this.Nome = nome;

    public Sala AddReserva(Reserva reserva)
    {
        _reservas.Add(reserva);
        return this;
    }
}
