using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using NHibernate.Dialect.Schema;

namespace NHibernate.JetDriver.Schema
{
    public class JetDataBaseSchema : AbstractDataBaseSchema
    {
        public JetDataBaseSchema(DbConnection connection)
            : base(connection)
        {
            _Connection = (OleDbConnection)connection;
        }

        private OleDbConnection _Connection;

        public override ITableMetadata GetTableMetadata(DataRow rs, bool extras)
        {

            return new JetTableMetadata(rs, this, extras);
        }

        public override DataTable GetForeignKeys(string catalog, string schema, string table)
        {
            //How To Retrieve Schema Information by Using GetOleDbSchemaTable and Visual Basic .NET
            //http://support.microsoft.com/kb/309488
            var oledbConnection = (OleDbConnection)Connection;
            object[] restrictions = new object[] { null, null, table, null };

            // Open the schema information for the foreign keys.
            var schemaTable = oledbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, restrictions);

            return schemaTable;

        }

        public override DataTable GetIndexColumns(string catalog, string schemaPattern, string tableName, string indexName)
        {

            //How To Retrieve Schema Information by Using GetOleDbSchemaTable and Visual Basic .NET
            //http://support.microsoft.com/kb/309488
            var oledbConnection = (OleDbConnection)Connection;
            object[] restrictions = new object[] { null, null, tableName, null };

            // Open the schema information for indexes.
            var schemaTable = oledbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, restrictions);

            return schemaTable;
        }


    }
}