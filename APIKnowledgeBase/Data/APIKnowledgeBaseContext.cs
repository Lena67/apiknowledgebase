using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIKnowledgeBase.Models;

namespace APIKnowledgeBase.Data
{
    public class APIKnowledgeBaseContext : DbContext
    {
        public APIKnowledgeBaseContext (DbContextOptions<APIKnowledgeBaseContext> options)
            : base(options)
        {
        }

        public DbSet<APIKnowledgeBase.Models.Notebook> Notebook { get; set; } = default!;
    }
}
