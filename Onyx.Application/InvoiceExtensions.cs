using Onyx.Application.Models;

namespace Onyx.Application;

public static class InvoiceExtensions
{
    /// <summary>
    /// Get all guest names that occur on more than once (across all invoice groups and invoices, not per invoice group or invoice)
    /// </summary>
    /// <param name="invoiceGroups"></param>
    /// <returns>The collection of GuestNames</returns>
    public static IEnumerable<string> GetRepeatedGuestNamesAccrossGroups(IEnumerable<InvoiceGroup> invoiceGroups)
    {
        return invoiceGroups
            .SelectMany(i => i.Invoices)
            .SelectMany(o => o.Observations)
            .GroupBy(g => g.GuestName)
            .Select(r => new KeyValuePair<string, int>(r.Key, r.Count()))
            .Where(k => k.Value > 1)
            .Select(n => n.Key);
    }

    /// <summary>
    /// Get total number of nights per travel agent for invoice groups issued in specific year
    /// </summary>
    /// <param name="invoiceGroups">Groups</param>
    /// <param name="year">Issued Year</param>
    /// <returns></returns>
    public static IEnumerable<TravelAgentInfo> CalculateNumberOfNightsByTravelAgentWithDate(IEnumerable<InvoiceGroup> invoiceGroups, int year)
    {
        return invoiceGroups
            .Where(i => i.IssueDate.Year == year)
            .SelectMany(i => i.Invoices)
            .SelectMany(o => o.Observations)
            .GroupBy(
            g => g.TravelAgent,
            g => g.NumberOfNights,
            (agentName, nights) => new TravelAgentInfo
            {
                TravelAgent = agentName,
                TotalNumberOfNights = nights.Sum()
            });
    }
}
