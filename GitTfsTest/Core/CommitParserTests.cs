﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sep.Git.Tfs.Core;
using Xunit;
using Xunit.Extensions;

namespace Sep.Git.Tfs.Test.Core
{
    public class CommitParserTests
    {
        public static IEnumerable<object[]> Cases
        {
            get
            {
                return new[] {
                    new object[] { "git-tfs-id: foo;C123", true, 123 },
                    new object[] { "git-tfs-id: handle more than Int32;C" + uint.MaxValue, true, uint.MaxValue },
                    new object[] { "foo-tfs-id: bar;C123", false, 0 },
                    new object[] { "\ngit-tfs-id: foo;C234\n", true, 234 },
                    new object[] { "\r\ngit-tfs-id: foo;C345\r\n", true, 345 },
                };
            }
        }


        [Theory]
        [PropertyData("Cases")]
        public void Run(string message, bool expectParsed, long expectId)
        {
            long id;
            bool parsed = GitRepository.TryParseChangesetId(message, out id);
            Assert.Equal(expectParsed, parsed);
            if (parsed)
            {
                Assert.Equal(id, expectId);
            }
        }
    }
}
