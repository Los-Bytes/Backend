using Backend.API.Laboratories.Domain.Model.Aggregates;
using Backend.API.Laboratories.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Backend.API.Laboratories.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model Builder Extensions for Laboratory Context
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyLaboratoryConfiguration(this ModelBuilder builder)
    {
        // Configuración de tabla
        builder.Entity<Laboratory>().ToTable("laboratories");
        
        // Clave primaria
        builder.Entity<Laboratory>().HasKey(l => l.Id);
        builder.Entity<Laboratory>().Property(l => l.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        // VALUE OBJECT: LaboratoryName
        builder.Entity<Laboratory>()
            .Property(l => l.Name)
            .HasConversion(
                v => v.Value,
                v => new LaboratoryName(v))
            .HasColumnName("Name")
            .HasMaxLength(200)
            .IsRequired();

        // VALUE OBJECT: Address (SIMPLIFICADO - como string único)
        builder.Entity<Laboratory>()
            .Property(l => l.Address)
            .HasConversion(
                v => v.ToFullAddressString(),
                v => Address.FromFullAddress(v))
            .HasColumnName("Address")
            .HasMaxLength(500)
            .IsRequired();

        // VALUE OBJECT: PhoneNumber
        builder.Entity<Laboratory>()
            .Property(l => l.Phone)
            .HasConversion(
                v => v.Value,
                v => new PhoneNumber(v))
            .HasColumnName("Phone")
            .HasMaxLength(20)
            .IsRequired();

        // VALUE OBJECT: Capacity
        builder.Entity<Laboratory>()
            .Property(l => l.Capacity)
            .HasConversion(
                v => v.Value,
                v => new Capacity(v))
            .HasColumnName("Capacity")
            .IsRequired();

        // VALUE OBJECT: MemberList (con ValueComparer)
        builder.Entity<Laboratory>()
            .Property(l => l.Members)
            .HasConversion(
                v => string.Join(",", v.ToList()),
                v => new MemberList(string.IsNullOrWhiteSpace(v) 
                    ? new List<int>() 
                    : v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToList()))
            .HasColumnName("MemberUserIds")
            .Metadata.SetValueComparer(
                new ValueComparer<MemberList>(
                    (c1, c2) => c1 != null && c2 != null && c1.ToList().SequenceEqual(c2.ToList()),
                    c => c.ToList().Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => new MemberList(c.ToList())));

        // Propiedades simples
        builder.Entity<Laboratory>()
            .Property(l => l.RegistrationDate)
            .HasColumnName("RegistrationDate")
            .IsRequired();
        
        builder.Entity<Laboratory>()
            .Property(l => l.LabResponsibleId)
            .HasColumnName("LabResponsibleId");
        
        builder.Entity<Laboratory>()
            .Property(l => l.AdminUserId)
            .HasColumnName("AdminUserId")
            .IsRequired();
        
        builder.Entity<Laboratory>()
            .Property(l => l.CreatedDate)
            .HasColumnName("CreatedAt");
        
        builder.Entity<Laboratory>()
            .Property(l => l.UpdatedDate)
            .HasColumnName("UpdatedAt");

        // Índices
        builder.Entity<Laboratory>().HasIndex(l => l.AdminUserId);
    }
}
