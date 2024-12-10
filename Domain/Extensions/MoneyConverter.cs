namespace Domain.Extensions;

internal static class MoneyConverter
{
    internal static int PoundsToPence(this decimal amount)
        => (int)(amount * 100);

    internal static decimal PenceToPounds(this int amount)
        => amount / 100;
}
