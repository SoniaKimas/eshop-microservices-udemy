using Ordering.Domain.Enums;

namespace Ordering.Infraestruture.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
            x => x.Value, 
            x => OrderId.Of(x));


        builder.HasOne<Customer>()
              .WithMany()
              .HasForeignKey(o => o.CustomerId)
              .IsRequired();


        builder.HasMany(x => x.OrderItems)
            .WithOne()
            .HasForeignKey(x => x.OrderId);

        builder.ComplexProperty(
            x => x.OrderName,
            namebuilder =>
            {
                namebuilder.Property(p => p.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.ShippingAddress,
            addressBuilder =>
            {
                addressBuilder.Property(p => p.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(p => p.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(p => p.EmailAddress)
                    .HasMaxLength(50);

                addressBuilder.Property(p => p.AddressLine)
                    .HasColumnName(nameof(Order.ShippingAddress.AddressLine))
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(p => p.State)
                    .HasMaxLength(50);

                addressBuilder.Property(p => p.Country)
                    .HasMaxLength(50);

                addressBuilder.Property(p => p.ZipCode)
                    .HasMaxLength(20)
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.BillingAddress,
            addressBuilder =>
            {
                addressBuilder.Property(p => p.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(p => p.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(p => p.EmailAddress)
                    .HasMaxLength(50);

                addressBuilder.Property(p => p.AddressLine)
                    .HasColumnName(nameof(Order.BillingAddress.AddressLine))
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(p => p.State)
                    .HasMaxLength(50);

                addressBuilder.Property(p => p.Country)
                    .HasMaxLength(50);

                addressBuilder.Property(p => p.ZipCode)
                    .HasMaxLength(20)
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.Payment,
            paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName)
                    .HasMaxLength(50);

                paymentBuilder.Property(p => p.CardNumber)
                    .HasMaxLength(25)
                    .IsRequired();

                paymentBuilder.Property(p => p.Expiration)
                    .HasMaxLength(10);

                paymentBuilder.Property(p => p.CVV)
                    .HasMaxLength(3);

                paymentBuilder.Property(p => p.PaymentMethod);
            });

        builder.Property(x => x.OrderStatus)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                x => x.ToString(),
                x => (OrderStatus)Enum.Parse(typeof(OrderStatus), x, true)
            );

        builder.Property(x => x.TotalPrice);

    }
}
