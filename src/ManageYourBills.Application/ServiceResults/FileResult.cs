using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.ServiceResults;
public class FileResult
{
    public List<Bill> AddedBills { get; set; } = new();
    public List<string> Errors { get; set; } = new();
}
