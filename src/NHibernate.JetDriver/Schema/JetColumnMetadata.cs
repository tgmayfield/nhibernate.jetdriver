using System;
using System.Data;
using NHibernate.Dialect.Schema;

namespace NHibernate.JetDriver.Schema
{
    public class JetColumnMetadata : AbstractColumnMetaData
    {
        public JetColumnMetadata(DataRow rs)
            : base(rs)
        {
            Name = Convert.ToString(rs["COLUMN_NAME"]);
            object aValue;

            aValue = rs["CHARACTER_MAXIMUM_LENGTH"];
            if (aValue != DBNull.Value)
                ColumnSize = Convert.ToInt32(aValue);

            aValue = rs["NUMERIC_PRECISION"];
            if (aValue != DBNull.Value)
                NumericalPrecision = Convert.ToInt32(aValue);

            Nullable = Convert.ToString(rs["IS_NULLABLE"]);
            TypeName = JetDataTypes.GetJetDriverTypeName(Convert.ToString(rs["DATA_TYPE"]));
        }
    }
}