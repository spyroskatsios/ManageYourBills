using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.BlazorServer.Models;
public class BillModel
{
    public Guid Id { get; set; }

    [Required]
    public string Type { get; set; }

    [Required]
    public ProviderModel Provider { get; set; }

    [Required]
    public DateTime? Issued { get; set; }

    [Required]
    public DateTime? From { get; set; }

    [Required]
    public DateTime? To { get; set; }

    [Required]
    public DateTime? Expiration { get; set; }
    public string Comments { get; set; }
    public decimal Cost { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }

    public BillModel(Guid id, string type, ProviderModel provider, DateTime? issue, DateTime? from, DateTime? to, DateTime? expiration,
        string comments, decimal cost, bool isPaid, DateTime? paidAt)
    {
        Id = id;
        Type = type;
        Provider = provider;
        Issued = issue;
        From = from;
        To = to;
        Expiration = expiration;
        Comments = comments;
        Cost = cost;
        IsPaid = isPaid;
        PaidAt = paidAt;
    }

    public BillModel()
    {

    }
}
