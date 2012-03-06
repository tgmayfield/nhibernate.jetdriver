using System;
using System.Data;
using NHibernate.Dialect.Schema;

namespace NHibernate.JetDriver.Schema
{
    public class JetTableMetadata : AbstractTableMetadata
    {

        public JetTableMetadata(DataRow rs, IDataBaseSchema meta, bool extras)
            : base(rs, meta, extras)
        {
        }

        protected override void ParseTableInfo(DataRow rs)
        {
            Catalog = Convert.ToString(rs["TABLE_CATALOG"]);
            Schema = Convert.ToString(rs["TABLE_SCHEMA"]);
            if (string.IsNullOrEmpty(Catalog)) Catalog = null;
            if (string.IsNullOrEmpty(Schema)) Schema = null;
            Name = Convert.ToString(rs["TABLE_NAME"]);
        }

        protected override string GetConstraintName(DataRow rs)
        {
            return Convert.ToString(rs["FK_NAME"]);
        }

        protected override string GetColumnName(DataRow rs)
        {
            return Convert.ToString(rs["COLUMN_NAME"]);
        }

        protected override string GetIndexName(DataRow rs)
        {
            return Convert.ToString(rs["INDEX_NAME"]);
        }

        protected override IColumnMetadata GetColumnMetadata(DataRow rs)
        {
            return new JetColumnMetadata(rs);
        }

        protected override IForeignKeyMetadata GetForeignKeyMetadata(DataRow rs)
        {
            return new JetForeignKeyMetadata(rs);
        }

        protected override IIndexMetadata GetIndexMetadata(DataRow rs)
        {
            return new JetIndexMetadata(rs);
        }
    }
}