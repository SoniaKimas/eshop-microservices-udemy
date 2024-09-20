﻿namespace Ordering.Domain.Models;

public class Order :  Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;

    public OrderName OrderName { get; private set; } = default!;

    public Address ShippingAddress { get; private set; } = default!;

    public Address BillingAddress { get; private set; } = default!;

    public Payment Payment { get; private set; } = default!;

    public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice {
        get => _orderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    public static Order Create(
        OrderId orderId,
        CustomerId customerId,
        OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment payment
      )
    {
        var order = new Order
        {
            Id = orderId,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            OrderStatus = OrderStatus.Pending

        };

        order.AddDomainEvent(new OrderCreatedEvent(order));   

        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        OrderStatus = OrderStatus.Pending;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void AddOrderItem(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

        var orderItem = new OrderItem( Id, productId, quantity, price);
        _orderItems.Add(orderItem);

    }

    public void removeOrderItem(OrderItemId orderItemId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.Id == orderItemId);
        if (orderItem is null)
        {
            throw new InvalidOperationException("Order item not found");
        }

        _orderItems.Remove(orderItem);
    }
}
