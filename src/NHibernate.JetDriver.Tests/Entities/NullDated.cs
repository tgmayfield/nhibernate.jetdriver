using System;

namespace NHibernate.JetDriver.Tests.Entities
{
    public class NullDated
    {
        public NullDated()
        {
            Required = DateTime.Today;
        }

        public virtual int Id { get; set; }
        public virtual DateTime Required { get; set; }
        public virtual DateTime? Optional { get; set; }
    }
}