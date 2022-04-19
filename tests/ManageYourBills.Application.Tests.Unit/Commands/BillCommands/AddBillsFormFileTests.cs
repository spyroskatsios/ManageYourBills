using FluentAssertions;
using ManageYourBills.Application.Commands.BillCommands;
using ManageYourBills.Application.Commands.BillCommands.Handlers;
using ManageYourBills.Application.Interfaces;
using ManageYourBills.Application.ServiceResults;
using ManageYourBills.Domain.Entities;
using ManageYourBills.Domain.Enums;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ManageYourBills.Application.Tests.Unit.Commands.BillCommands;
public class AddBillsFormFileTests
{
    private readonly AddBillsFromFileHandler _sut;
    private readonly IFileService _fileService;

    public AddBillsFormFileTests()
    {
        _fileService = Substitute.For<IFileService>();
        _sut = new(_fileService);
    }

    [Fact]
    public async Task AddBillsFormFile_ShouldReturnAddedBillsAndErrors()
    {
        // Arrange
        var fileResult = new FileResult
        {
            AddedBills = new List<Bill>
            {
                new Bill(Guid.NewGuid(), BillType.Tv, new Provider(Guid.NewGuid(), "qw"),
                    DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5)), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-40)),
                    DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10)), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(20)),
                    null, 10, true,  DateOnly.FromDateTime(DateTime.UtcNow)),
                new Bill(Guid.NewGuid(), BillType.Tv, new Provider(Guid.NewGuid(), "qw"),
                    DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5)), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-40)),
                    DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10)), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(20)),
                    null, 10, false,  null)
            },

            Errors = new List<string> { "Error 1", "Error 2" }
        };

        _fileService.AddAsync(Arg.Any<string>()).Returns(fileResult);

        // Act
        var result = await _sut.Handle(new AddBillsFromFileCommand("path"), new CancellationToken());

        // Assert
        result.AddedBills.Count().Should().Be(2);
        result.Errors.Count().Should().Be(2);
    }
}
