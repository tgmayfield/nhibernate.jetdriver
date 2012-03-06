using System;
using System.Collections.Generic;

namespace NHibernate.JetDriver.Schema
{
    /// <summary>
    ///    Convert Dataschema native number types to Jetdriver typenames 
    /// </summary>
    public class JetDataTypes
    {

        private static readonly Dictionary<string, string> JetDriverTypes;

        static JetDataTypes()
        {
            JetDriverTypes = new Dictionary<string, string>();
            JetDriverTypes.Add("2", "SMALLINT");
            JetDriverTypes.Add("3", "INT");
            JetDriverTypes.Add("4", "REAL");
            JetDriverTypes.Add("5", "FLOAT");
            JetDriverTypes.Add("6", "MONEY");
            JetDriverTypes.Add("7", "DATETIME");
            JetDriverTypes.Add("11", "BIT");
            JetDriverTypes.Add("17", "BYTE");
            JetDriverTypes.Add("72", "GUID");
            JetDriverTypes.Add("204", "IMAGE");//BigBinary
            JetDriverTypes.Add("205", "IMAGE");//longBinary
            JetDriverTypes.Add("203", "MEMO");//LongText
            JetDriverTypes.Add("202", "TEXT");
            JetDriverTypes.Add("131", "DECIMAL");
            JetDriverTypes.Add("130", "TEXT");
        }

        public static string GetJetDriverTypeName(string nativeDatatype)
        {
            return JetDriverTypes[nativeDatatype];
        }

    }
}