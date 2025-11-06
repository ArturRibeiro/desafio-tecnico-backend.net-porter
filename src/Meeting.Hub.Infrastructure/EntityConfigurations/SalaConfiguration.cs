namespace Meeting.Hub.Infrastructure.EntityConfigurations;

public class SalaConfiguration : IEntityTypeConfiguration<Sala>
{
    public void Configure(EntityTypeBuilder<Sala> builder)
    {
        builder.ToTable("Salas");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(s => s.Nome)
            .HasColumnName("Nome")
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasMany(s => s.Reservas)
            .WithOne(r => r.Sala)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
