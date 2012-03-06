using System;
using System.Collections.Generic;

namespace NHibernate.JetDriver.Schema
{
    /// <summary>
    ///    Convert Dataschema native number types to Jetdriver typenames 
    /// </summary>
    public class JetDataTypes
    {

        private static Dictionary<string, string> _JetDriverTypes;

        static JetDataTypes()
        {
            _JetDriverTypes = new Dictionary<string, string>();
            _JetDriverTypes.Add("2", "SMALLINT");
            _JetDriverTypes.Add("3", "INT");
            _JetDriverTypes.Add("4", "REAL");
            _JetDriverTypes.Add("5", "FLOAT");
            _JetDriverTypes.Add("6", "MONEY");
            _JetDriverTypes.Add("7", "DATETIME");
            _JetDriverTypes.Add("11", "BIT");
            _JetDriverTypes.Add("17", "BYTE");
            _JetDriverTypes.Add("72", "GUID");
            _JetDriverTypes.Add("204", "IMAGE");//BigBinary
            _JetDriverTypes.Add("205", "IMAGE");//longBinary
            _JetDriverTypes.Add("203", "MEMO");//LongText
            _JetDriverTypes.Add("202", "TEXT");
            _JetDriverTypes.Add("131", "DECIMAL");
            _JetDriverTypes.Add("130", "TEXT");
        }

        public static string GetJetDriverTypeName(string nativeDatatype)
        {
            return _JetDriverTypes[nativeDatatype];
        }

    }
}