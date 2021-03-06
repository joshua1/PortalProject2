﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PortalProject2.Logic
{
    public class Repo<TContext> : IDisposable where TContext : DbContext, IObjectContextAdapter, new()
    {
        private TContext context;

        private Repo()
        {

        }

        public Repo(string connectionStringName)
        {
            context = new TContext();
            context.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        /// <summary>
        /// Dipose repository
        /// </summary>
        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }

        /// <summary>
        /// Select data from database
        /// </summary>
        /// <typeparam name="TItem">Type of data to select</typeparam>
        /// <returns></returns>
        public IQueryable<TItem> Select<TItem>()
           where TItem : class, new()
        {
            PropertyInfo property = GetDbSet(typeof(TItem));

            DbSet<TItem> set = property.GetValue(context, null) as DbSet<TItem>;

            return set;
        }

        /// <summary>
        /// Insert new item into database
        /// </summary>
        /// <typeparam name="TItem">Type of item to insert</typeparam>
        /// <param name="item">Item to insert</param>
        /// <returns>Inserted item</returns>
        public TItem Insert<TItem>(TItem item)
            where TItem : class, new()
        {
            DbSet<TItem> set = GetDbSet(typeof(TItem)).GetValue(context, null) as DbSet<TItem>;
            try
            {
                set.Add(item);
                context.SaveChanges();
                return item;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                throw;  // You can also choose to handle the exception here...
            }
        }

        /// <summary>
        /// Update na item
        /// </summary>
        /// <typeparam name="TItem">Type of item to update</typeparam>
        /// <param name="item">Item to update</param>
        /// <returns>Updated item</returns>
        public TItem Update<TItem>(TItem item)
            where TItem : class, new()
        {
            DbSet<TItem> set = GetDbSet(typeof(TItem)).GetValue(context, null) as DbSet<TItem>;
            set.Attach(item);
            context.Entry(item).State = System.Data.EntityState.Modified;
            context.SaveChanges();
            return item;
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <typeparam name="TItem">Type of item to delete</typeparam>
        /// <param name="item">Item to delete</param>
        public void Delete<TItem>(TItem item)
           where TItem : class, new()
        {
            DbSet<TItem> set = GetDbSet(typeof(TItem)).GetValue(context, null) as DbSet<TItem>;
            var entry = context.Entry(item);
            if (entry != null)
            {
                entry.State = System.Data.EntityState.Deleted;
            }
            else
            {
                set.Attach(item);
            }
            context.Entry(item).State = System.Data.EntityState.Deleted;
            context.SaveChanges();
        }

        private PropertyInfo GetDbSet(Type itemType)
        {
            var properties = typeof(TContext).GetProperties().Where(item => item.PropertyType.Equals(typeof(DbSet<>).MakeGenericType(itemType)));

            return properties.First();
        }

    }
}