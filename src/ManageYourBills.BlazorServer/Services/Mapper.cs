using ManageYourBills.Application.Commands.BillCommands;
using ManageYourBills.Application.Queries.BillQueries;
using ManageYourBills.Application.Responses;
using ManageYourBills.BlazorServer.Models;
using ManageYourBills.BlazorServer.SearchParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.BlazorServer.Services;
public class Mapper : IMapper
{
    public IEnumerable<ProviderModel> Map(IEnumerable<ProviderResponse> providers)
        => providers.Select(x => new ProviderModel(x.Id, x.Name)).ToList();

    public ProviderModel Map(ProviderResponse provider)
        => new ProviderModel(provider.Id, provider.Name);

    public BillModel? Map(BillResponse bill)
        => bill is null ? null : new BillModel(bill.Id, bill.Type, Map(bill.Provider), bill.Issued.ToDateTime(TimeOnly.MinValue), bill.From.ToDateTime(TimeOnly.MinValue),
            bill.To.ToDateTime(TimeOnly.MinValue), bill.Expiration.ToDateTime(TimeOnly.MinValue), bill.Comments, bill.Cost, bill.IsPaid,
            bill.PaidAt is null ? null : bill.PaidAt.Value.ToDateTime(TimeOnly.MinValue));

    public CreateBillCommand MapToCreateCommand(BillModel bill)
        => new CreateBillCommand(bill.Type, bill.Provider.Id, DateOnly.FromDateTime(bill.Issued.Value), DateOnly.FromDateTime(bill.From.Value),
             DateOnly.FromDateTime(bill.To.Value), DateOnly.FromDateTime(bill.Expiration.Value), bill.Comments, bill.Cost, bill.IsPaid,
               bill.PaidAt is null ? null : DateOnly.FromDateTime(bill.PaidAt.Value));

    public UpdateBillCommand MapToUpdateCommand(BillModel bill)
        => new UpdateBillCommand(bill.Id, bill.Type, bill.Provider.Id, DateOnly.FromDateTime(bill.Issued.Value), DateOnly.FromDateTime(bill.From.Value),
             DateOnly.FromDateTime(bill.To.Value), DateOnly.FromDateTime(bill.Expiration.Value), bill.Comments, bill.Cost, bill.IsPaid,
               bill.PaidAt is null ? null : DateOnly.FromDateTime(bill.PaidAt.Value));

    public GetBillsQuery Map(BillsParameters parameters)
        => new GetBillsQuery
        {
            OrderParameter = parameters.OrderParameter,
            SortOrder = parameters.SortOrder,
            BillType = parameters.BillType,
            CostFrom = parameters.CostFrom,
            CostTo = parameters.CostTo,
            ExpirationFrom = parameters.ExpirationFrom is null ? null : DateOnly.FromDateTime(parameters.ExpirationFrom.Value),
            ExpirationTo = parameters.ExpirationTo is null ? null : DateOnly.FromDateTime(parameters.ExpirationTo.Value),
            PaidAtFrom = parameters.PaidAtFrom is null ? null : DateOnly.FromDateTime(parameters.PaidAtFrom.Value),
            PaidAtTo = parameters.PaidAtTo is null ? null : DateOnly.FromDateTime(parameters.PaidAtTo.Value),
            ProviderId = parameters.Provider is null ? null : parameters.Provider.Id,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
}

