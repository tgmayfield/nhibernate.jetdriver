using System;
using System.Collections.Generic;

using NHibernate.JetDriver.Tests.Entities;

using NUnit.Framework;

namespace NHibernate.JetDriver.Tests
{
    [TestFixture]
    public class JetCriteriaFixture : JetTestBase
    {
        public JetCriteriaFixture()
            : base(true)
        {
        }

        protected override IList<System.Type> EntityTypes
        {
            get
            {
                return new[]
                {
                    typeof(Foo),
                    typeof(ReportDTO),
                    typeof(Catalog),
                    typeof(Category),
                    typeof(ProductType),
                    typeof(Product),
                };
            }
        }

        protected override IList<string> Mappings
        {
            get { return new[] { "Entities.NHCD37.hbm.xml" }; }
        }

        [Test]
        public void can_limit()
        {
            using (var s = SessionFactory.OpenSession())
            {
                var criteria = s.CreateCriteria<Catalog>()
                    .SetMaxResults(10);

                criteria.List();
            }
        }
    }
}