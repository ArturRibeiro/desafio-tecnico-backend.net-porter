namespace Meeting.Hub.Infrastructure.EntityConfigurations;

public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.ToTable("Reservas");

        builder.HasKey(r => r.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(x => x.DataInicio)
            .HasColumnName("DateInicio")
            .IsRequired();

        builder.Property(x => x.DataFim)
            .HasColumnName("DateFim")
            .IsRequired();
        
        builder.Property(x => x.ReservadoPor)
            .HasColumnName("ReservadoPor")
            .IsRequired();

        builder.HasOne(r => r.Sala)
            .WithMany(s => s.Reservas)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
