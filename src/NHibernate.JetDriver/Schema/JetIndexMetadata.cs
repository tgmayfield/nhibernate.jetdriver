using System;
using System.Data;
using NHibernate.Dialect.Schema;

namespace NHibernate.JetDriver.Schema
{
    public class JetIndexMetadata : AbstractIndexMetadata
    {
        public JetIndexMetadata(DataRow rs)
            : base(rs)
        {
            Name = Convert.ToString(rs["INDEX_NAME"]);
        }
    }
}