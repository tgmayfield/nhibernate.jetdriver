using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.JetDriver.Tests.Entities;
using NHibernate.Linq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NHibernate.JetDriver.Tests
{
    [TestFixture]
    public class JetLinqFixture
        : JetTestBase
    {
        private const int InsertCount = 50;

        public JetLinqFixture()
            : base(true)
        {
            using (var session = SessionFactory.OpenSession())
            using (var tran = session.BeginTransaction())
            {
                for (int count = 0; count < InsertCount; count++)
                {
                    session.Save(new Foo());
                }
                tran.Commit();
            }
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
        public void can_limit()
        {
            using (var session = SessionFactory.OpenSession())
            {
                var query = session.Query<Foo>()
                    .Take(10);
                int count = query.ToList().Count;

                Assert.That(count, Is.EqualTo(10));
            }
        }

        [Test]
        public void can_count()
        {
            using (var sesison = SessionFactory.OpenSession())
            {
                var query = sesison.Query<Foo>();
                int count = query.Count();

                Assert.That(count, Is.EqualTo(InsertCount));
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

        [Test]
        public void can_call_ToUpper_and_ToLower()
        {
            using (var session = SessionFactory.OpenSession())
            {
                var query = session.Query<Foo>()
                    .Select(f => new
                    {
                        Test = f.Description.ToUpper(),
                        Test2 = f.Description.ToLower(),
                    });
                query.ToList();
            }
        }
    }
}