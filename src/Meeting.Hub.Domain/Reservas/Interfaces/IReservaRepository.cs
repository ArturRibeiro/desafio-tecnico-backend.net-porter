namespace Meeting.Hub.Domain.Reservas.Interfaces;

public interface IReservaRepository
{
    Task<Reserva> SaveAsync(Reserva reserva);
    Task<Reserva> GetAsync(Expression<Func<Reserva, bool>> expression);
    Task<Reserva> GetAsync<TProperty>(Expression<Func<Reserva, bool>> expression, Expression<Func<Reserva, TProperty>> include);
    Task<Reserva> RemoveAsync(Expression<Func<Reserva, bool>> expression);
}