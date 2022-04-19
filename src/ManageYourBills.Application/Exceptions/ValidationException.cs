using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Exceptions;
public class ValidationException : Exception
{
    public IDictionary<string, string[]> Failures { get; }

    public ValidationException(List<ValidationFailure> failures) : base("One or more validation errors have occured.")
    {
        Failures = new Dictionary<string, string[]>();

        var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            Failures.Add(propertyName, propertyFailures);
        }
    }


}

