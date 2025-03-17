using Microsoft.EntityFrameworkCore;
using ProdutoCadastro.Domain.Entities;

namespace ProdutoCadastro.Data.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public required DbSet<Associado> Associados { get; set; }
    }
}
