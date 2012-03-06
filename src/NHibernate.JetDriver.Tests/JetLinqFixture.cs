using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.JetDriver.Tests.Entities;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibernate.JetDriver.Tests
{
    [TestFixture]
    public class JetLinqFixture
        : JetTestBase
    {
        public JetLinqFixture()
            : base(true)
        {
        }

        protected override IList<System.Type> EntityTypes
        {
            get
            {
                return new[]
                {
                    typeof(Foo)
                };
            }
        }

        [Test]
        [Ignore]
        public void can_limit()
        {
            using (var session = SessionFactory.OpenSession())
            {
                var query = session.Query<Foo>()
                    .Take(10);
                query.ToList();
            }
        }

        [Test]
        [Ignore]
        public void can_count()
        {
            using (var sesison = SessionFactory.OpenSession())
            {
                var query = sesison.Query<Foo>();
                query.Count();
            }
        }

        [Test]
        public void can_issue_FirstOrDefault()
        {
            using (var session = SessionFactory.OpenSession())
            {
                var query = session.Query<Foo>()
                    .Where(f => f.Description == "invalid");

                query.FirstOrDefault();
            }
        }
    }
}