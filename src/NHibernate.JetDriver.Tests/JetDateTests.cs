using System;
using System.Collections.Generic;

using NHibernate.JetDriver.Tests.Entities;

using NUnit.Framework;

namespace NHibernate.JetDriver.Tests
{
    [TestFixture]
    public class JetDateTests
        : JetTestBase
    {
        public JetDateTests()
            : base(true)
        {
        }

        protected override IList<System.Type> EntityTypes
        {
            get
            {
                return new List<System.Type>()
                {
                    typeof(NullDated),
                };
            }
        }

        [Test]
        public void can_update_mixed_value_and_null_dates()
        {
            var records = new List<NullDated>()
            {
                new NullDated()
                {
                    Optional = new DateTime(2011, 1, 1),
                },
                new NullDated()
                {
                    Optional = null,
                },
                new NullDated()
                {
                    Optional = new DateTime(2011, 1, 2),
                },
            };

            using (var session = SessionFactory.OpenSession())
            using (var tran = session.BeginTransaction())
            {
                foreach (var record in records)
                {
                    session.Save(record);
                }

                tran.Commit();
            }

            using (var session = SessionFactory.OpenSession())
            using (var tran = session.BeginTransaction())
            {
                foreach (var record in records)
                {
                    record.Required = record.Required.AddDays(-1); // To trigger an update
                    session.Update(record);
                }

                tran.Commit();
            }
        }
    }
}