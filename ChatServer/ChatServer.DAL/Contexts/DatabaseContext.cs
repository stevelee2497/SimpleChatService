﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ChatServer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace ChatServer.DAL.Contexts
{
	public class DatabaseContext : DbContext, IDataContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Conversation> Conversations { get; set; }

		public DbSet<UserConversation> UserConversations { get; set; }

		public DbSet<Message> Messages { get; set; }

		private readonly IConfigurationRoot _configRoot;

		public DatabaseContext(IConfigurationRoot configRoot)
		{
			_configRoot = configRoot;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_configRoot.GetConnectionString("DefaultConnection"));
		}
	}

	public interface IDataContext : IDisposable
	{
		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
	}
}