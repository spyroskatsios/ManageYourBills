using ManageYourBills.Application.Interfaces;
using ManageYourBills.Application.ServiceResults;
using ManageYourBills.Domain.Entities;
using ManageYourBills.Domain.Enums;
using ManageYourBills.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Infrastructure.Services;
public class FileService : IFileService
{
    private readonly IAppDbContext _context;

    public FileService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<FileResult> AddAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new FileResult { Errors = new List<string> { "File does not exists!" } };
        }

        var lines = File.ReadAllLines(filePath);
        var addedBills = new List<Bill>();
        var result = new FileResult();
        int lineCounter = 1;

        foreach (var line in lines)
        {
            if (lineCounter == 1)
            {
                lineCounter++;
                continue;
            }

            result.Errors.AddRange(await AddBillForLine(line, lineCounter, addedBills));
            lineCounter++;
        }

        result.AddedBills = addedBills;
        return result;
    }

    private async Task<IEnumerable<string>> AddBillForLine(string line, int lineCounter, List<Bill> addedBills)
    {
        var errors = new List<string>();
        var values = line.Split(';');

        if (values.Length > 10)
        {
            errors.Add($"Too many columns in line {lineCounter}!");
            return errors;
        }

        if (values.Length == 0)
        {
            errors.Add($"Line {lineCounter} is empty!");
        }

        BillType type = BillType.Electricity;

        try
        {
            type = (BillType)Enum.Parse(typeof(BillType), values[0]);
        }
        catch (Exception exception)
        {
            errors.Add($"Couldn't find type for bill {lineCounter}!");
        }

        Provider provider;

        try
        {
            provider = await _context.Providers.FirstOrDefaultAsync(x => x.Name == new ProviderName(values[1]));
        }
        catch (Exception exception)
        {
            provider = null;
        }

        if (provider is null)
        {
            errors.Add($"Couldn't find provider with the given name for bill {lineCounter}!");
        }

        DateOnly issued;


        if (string.IsNullOrWhiteSpace(values[2]))
        {
            errors.Add($"Couldn't read Issue Date for bill {lineCounter}");
        }

        try
        {
            issued = DateOnly.Parse(values[2]);
        }
        catch (Exception exception)
        {
            errors.Add($"Couldn't read Issue Date for bill {lineCounter}");
        }


        DateOnly expiration;


        if (string.IsNullOrWhiteSpace(values[3]))
        {
            errors.Add($"Couldn't read Expiration Date for bill {lineCounter}");
        }

        try
        {
            expiration = DateOnly.Parse(values[3]);
        }
        catch (Exception exception)
        {
            errors.Add($"Couldn't read Expiration Date for bill {lineCounter}");
        }


        DateOnly from;

        if (string.IsNullOrWhiteSpace(values[4]))
        {
            errors.Add($"Couldn't read Start Date of Period of Consumption for bill {lineCounter}");
        }

        try
        {
            from = DateOnly.Parse(values[4]);
        }
        catch (Exception exception)
        {
            errors.Add($"Couldn't read Start Date of Period of Consumption for bill {lineCounter}");
        }

        DateOnly to;

        if (string.IsNullOrWhiteSpace(values[5]))
        {
            errors.Add($"Couldn't read Finish Date of Period of Consumption for bill {lineCounter}");
        }

        try
        {
            to = DateOnly.Parse(values[5]);
        }
        catch (Exception exception)
        {
            errors.Add($"Couldn't read Finish Date of Period of Consumption for bill {lineCounter}");
        }

        decimal cost = 0;

        try
        {
            cost = decimal.Parse(values[6]);
        }
        catch (Exception exception)
        {
            errors.Add($"Couldn't read Cost for line {lineCounter}");
        }


        bool isPaid = false;

        try
        {
            isPaid = bool.Parse(values[7]);
        }
        catch (Exception exception)
        {
            errors.Add($"Couldn't read if the bill is paid for line {lineCounter}");
        }

        DateOnly? paidAt = null;

        if (isPaid)
        {
            if (string.IsNullOrWhiteSpace(values[8]))
            {
                errors.Add($"Couldn't read Payment Date for line {lineCounter}");
            }

            try
            {
                paidAt = DateOnly.Parse(values[8]);
            }
            catch (Exception exception)
            {
                errors.Add($"Couldn't read Payment Date for line {lineCounter}");
            }
        }


        var comments = values[9];

        if (errors.Any())
        {
            return errors;
        }

        Bill bill;

        try
        {
            bill = new Bill(Guid.NewGuid(), type, provider, issued, from, to, expiration, comments, cost, isPaid, paidAt);
        }
        catch (Exception exception)
        {
            errors.Add($"Error in line {lineCounter}: {exception.Message}");
            return errors;
        }

        try
        {
            await _context.Bills.AddAsync(bill);
            await _context.SaveAsync();
            addedBills.Add(bill);
        }
        catch (Exception exception)
        {
            errors.Add($"Error in line {lineCounter}: {exception.Message}");
            return errors;
        }

        return errors;
    }
}
