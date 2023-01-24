using ManageYourBills.Application.ServiceResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Interfaces;
public interface IFileService
{
    Task<FileResult> AddAsync(string filePath);
}
