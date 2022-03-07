using System.Collections.Generic;
using JDC.Common.Entities;

namespace JDC.IntegrationTests.Infrastructure.TestData
{
    internal static class TestGroups
    {
        internal static Group Group = new ()
        {
            Name = "name",
        };

        internal static List<Group> Groups = new ()
        {
            Group,
        };
    }
}
