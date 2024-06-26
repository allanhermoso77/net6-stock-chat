﻿using Stock.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stock.Chat.Infrastructure.Data.Mapping
{
    public class MessageMapping : IEntityTypeConfiguration<Messages>
    {
        public void Configure(EntityTypeBuilder<Messages> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Consumer)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.Sender)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.Message)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
