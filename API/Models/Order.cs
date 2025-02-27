﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.DTO;

namespace API.Models
{
    public enum OrderStatus
    {
        Pending, Dispatched, Delivered, Cancelled
    }
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required]
        public ApplicationUser Customer { get; set; }

        [Required]
        public Hotel Hotel { get; set; }

        [Required]
        public List<OrderItem> OrderItems { get; set; }
        public double GrandTotal { get; set; }

        [Column("Status")]
        public string OrderStatusString
        {
            get => OrderStatus.ToString();
            private set => OrderStatus = value.ParseEnum<OrderStatus>();
        }
 
        [NotMapped]
        public OrderStatus OrderStatus { get; set; }

        public Order()
        {
            
        }

        public Order(ApplicationUser customer, Hotel hotel, List<OrderItem> orderItems, OrderStatus status)
        {
            Customer = customer;
            Hotel = hotel;
            OrderItems = orderItems;
            orderItems.ForEach(i => GrandTotal =+ i.Amount);
            OrderStatus = OrderStatus.Pending;
        }

        public double CalculateGrandTotal(List<OrderItem> orderItems)
        {
            var amt = 0.0;
            foreach (var i in orderItems)
            {
                amt = +i.Amount;
            }
            return amt;
        }
    }
}
