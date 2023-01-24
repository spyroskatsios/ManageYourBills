using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Responses;
public record FileResponse(IEnumerable<BillResponse> AddedBills, IEnumerable<string> Errors);
