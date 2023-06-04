using System;
using ChatRoom.Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.Context
{
	public class ChatRoomDbContext : DbContext, IDisposable
    {
		public ChatRoomDbContext(DbContextOptions<ChatRoomDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Room> Rooms { get; set; }
		public virtual DbSet<Stock> Stocks { get; set; }
		public virtual DbSet<Message> Messages { get; set; }
		public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(r => r.RoomId);
                entity.ToTable("Room");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(s => s.Code);
                entity.ToTable("Stock");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(s => s.MessageId);
                entity.ToTable("Message");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(s => s.UserId);
                entity.ToTable("User");
            });
        }
    }
}

