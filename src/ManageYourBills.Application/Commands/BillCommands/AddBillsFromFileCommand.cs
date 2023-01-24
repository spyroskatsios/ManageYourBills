using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.BillCommands;
public record AddBillsFromFileCommand(string FilePath) : IRequest<FileResponse>;
