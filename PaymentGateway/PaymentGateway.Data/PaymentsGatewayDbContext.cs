using Microsoft.EntityFrameworkCore;
using PaymentGateway.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Data
{
    public class PaymentsGatewayDbContext : DbContext
    {

        public PaymentsGatewayDbContext(DbContextOptions<PaymentsGatewayDbContext> options)
        : base (options)
        {

        }

        public DbSet<CardEntity> CardDetails { get; set; }
        public DbSet<PaymentEntity> PaymentDetails { get; set; }
    }
}
