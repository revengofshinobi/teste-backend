using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FullBar.Core;

namespace FullBar.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        protected readonly DB_TESTEEntities Context;

        protected BaseRepository(DB_TESTEEntities context)
        {
            Context = context;
        }

        protected BaseRepository() : this(new DB_TESTEEntities()) { }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}