using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ManageYourBills.Tests.Integration;

[CollectionDefinition("TestBase")]
public class TestBaseCollection : ICollectionFixture<TestBase>
{

}
