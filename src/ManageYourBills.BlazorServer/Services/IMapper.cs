using ManageYourBills.Application.Commands.BillCommands;
using ManageYourBills.Application.Queries.BillQueries;
using ManageYourBills.Application.Responses;
using ManageYourBills.BlazorServer.Models;
using ManageYourBills.BlazorServer.SearchParameters;

namespace ManageYourBills.BlazorServer.Services;

public interface IMapper
{
    IEnumerable<ProviderModel> Map(IEnumerable<ProviderResponse> providers);
    CreateBillCommand MapToCreateCommand(BillModel bill);
    BillModel? Map(BillResponse bill);
    UpdateBillCommand MapToUpdateCommand(BillModel bill);
    GetBillsQuery Map(BillsParameters parameters);
}