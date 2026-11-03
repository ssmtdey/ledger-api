using Ledger.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ledger.Api.Data;

public class LedgerDbContext : DbContext
{
    public LedgerDbContext(DbContextOptions<LedgerDbContext> options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transfer> Transfers => Set<Transfer>();
    public DbSet<LedgerEntry> LedgerEntries => Set<LedgerEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // The unique index is the safety net behind idempotency.
        modelBuilder.Entity<Transfer>().HasIndex(t => t.IdempotencyKey).IsUnique();
        modelBuilder.Entity<LedgerEntry>().HasIndex(e => e.AccountId);
    }
}