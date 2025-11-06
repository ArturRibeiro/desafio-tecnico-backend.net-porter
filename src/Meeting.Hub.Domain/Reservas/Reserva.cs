namespace Meeting.Hub.Domain.Reservas;

public class Reserva
{
    public long? Id { get; private set; }
    public bool IsNew => Id == null;
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }
    public string ReservadoPor { get; private set; }
    public Sala Sala { get; private set; }

    protected Reserva() { }

    public Reserva(Sala sala, DateTime inicio, DateTime fim, string reservadoPor)
    {
        Sala = sala;
        DataInicio = inicio;
        DataFim = fim;
        ReservadoPor = reservadoPor;
    }
    
    public Reserva(DateTime inicio, DateTime fim, string reservadoPor)
    {
        DataInicio = inicio;
        DataFim = fim;
        ReservadoPor = reservadoPor;
    }
    
    public void Alterar(DateTime inicio, DateTime fim)
    {
        this.DataInicio = inicio;
        this.DataFim = fim;
    }

    public void Validate()
    {
        var validator = new ReservaValidator();
        var result = validator.Validate(this);
        if (!result.IsValid) throw new ValidationException(result.Errors);
    }
}