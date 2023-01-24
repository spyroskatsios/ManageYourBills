using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.BillCommands.Handlers;
public class AddBillsFromFileHandler : IRequestHandler<AddBillsFromFileCommand, FileResponse>
{
    private readonly IFileService _fileService;

    public AddBillsFromFileHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<FileResponse> Handle(AddBillsFromFileCommand request, CancellationToken cancellationToken)
    {
        var result = await _fileService.AddAsync(request.FilePath);
        var addedBillsResponse = result.AddedBills.ToBillsResponse();
        return new FileResponse(addedBillsResponse, result.Errors);
    }
}
