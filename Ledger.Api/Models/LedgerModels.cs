namespace Ledger.Api.Models;

/* Business rules:
 * Amount must be greater than 0
 * Both accounts must exist, and must be different
 * Both accounts must have the same currency
 * The sender can't go below 0 balance (no overdraft)
 * Sending the same Idempotency key twice, should not result in a double transfer
 */

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string OwnerName { get; set; } = string.Empty;
    public string Currency { get; set; } = "GBP";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Transfer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public long AmountMinor { get; set; }
    public string Currency { get; set; } = "GBP";
    public string IdempotencyKey { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class LedgerEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AccountId { get; set; }
    public Guid? TransferId { get; set; }          // null for deposits
    public long AmountMinor { get; set; }          // positive = money in, negative = money out
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}