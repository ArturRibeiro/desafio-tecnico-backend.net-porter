namespace Meeting.Hub.Infrastructure.Repositorys;

public class ReservaRepository : IReservaRepository
{
    private readonly ApplicationDbContext _context;

    public ReservaRepository(ApplicationDbContext context) => _context = context;

    public async Task<Reserva> SaveAsync(Reserva reserva)
    {
        if (reserva.Id is null) await _context.Reservas.AddAsync(reserva);
        else _context.Reservas.Update(reserva);
        var queryString = _context.Reservas.ToQueryString();
        return await Task.FromResult(reserva);
    }

    public async Task<Reserva> GetAsync
        (Expression<Func<Reserva, bool>> expression) 
            => await _context.Reservas.FirstOrDefaultAsync(expression);

    public async Task<Reserva> GetAsync<TProperty>
        (Expression<Func<Reserva, bool>> expression, Expression<Func<Reserva, TProperty>> include)
        => await _context.Reservas
            .Include(include)
            .FirstOrDefaultAsync(expression);

    public async Task<Reserva> RemoveAsync(Expression<Func<Reserva, bool>> expression)
    {
        var reserva = _context.Reservas.FirstOrDefault(expression);
        _context.Reservas.Remove(reserva);
        return reserva;
    }
}