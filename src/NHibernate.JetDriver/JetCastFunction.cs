using System;
using System.Collections;
using NHibernate.Dialect.Function;
using NHibernate.Engine;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace NHibernate.JetDriver
{
    /// <summary>
    /// ANSI-SQL style cast(foo as type) where the type is a NHibernate type
    /// </summary>
    [Serializable]
    public class JetCastFunction : ISQLFunction, IFunctionGrammar
    {
        #region ISQLFunction Members

        public IType ReturnType(IType columnType, IMapping mapping)
        {
            //note there is a weird implementation in the client side
            //TODO: cast that use only costant are not supported in SELECT. Ex: cast(5 as string)
            return columnType;
        }

        public bool HasArguments
        {
            get { return true; }
        }

        public bool HasParenthesesIfNoArguments
        {
            get { return true; }
        }

        static JetCastFunction()
        {
            JetConversionFunctions.Add("TINYINT", "CByte");
            JetConversionFunctions.Add("MONEY", "CCur");
            JetConversionFunctions.Add("DATETIME", "CDate");
            JetConversionFunctions.Add("DECIMAL", "CDec");
            JetConversionFunctions.Add("REAL", "CSng");
            JetConversionFunctions.Add("FLOAT", "CDbl");
            JetConversionFunctions.Add("SMALLINT", "CInt");
            JetConversionFunctions.Add("INTEGER", "CLng");
            JetConversionFunctions.Add("INT", "CLng");
            JetConversionFunctions.Add("LONG", "CLng");

        }

        private static readonly Hashtable JetConversionFunctions = new Hashtable();

        private SqlString GetJetConvertionFunction(Dialect.Dialect dialect, string sqlType, object arg)
        {
            sqlType = sqlType.ToUpper();
            var sb = new SqlStringBuilder();



            if (JetConversionFunctions.ContainsKey(sqlType))
            {
                var functionname = JetConversionFunctions[sqlType];

                var sqlArg = arg.ToString();

                if (sqlArg.IndexOf("?") >= 0)
                {
                    sb.Add((string)functionname);
                    sb.Add("(");
                    sb.AddObject(arg);
                    sb.Add(")");
                }
                else
                {
                    sb.Add("iif");
                    sb.Add("(");

                    sb.Add("ISNULL");
                    sb.Add("(");
                    sb.AddObject(arg);
                    sb.Add(")");

                    sb.Add(",");
                    sb.Add("NULL");

                    sb.Add(",");

                    sb.Add((string)functionname);

                    sb.Add("(");
                    sb.AddObject(arg);
                    sb.Add(")");

                    sb.Add(")");
                }



                //sb.Add((string)_JetConversionFunctions[sqlType]);
                //sb.Add("(");
                //sb.AddObject(arg);
                //sb.Add(")");
            }
            else if (sqlType.Contains("TEXT") || sqlType.Contains("CHAR"))
            {
                sb.Add("CStr");
                sb.Add("(");
                sb.AddObject(arg);
                sb.Add(")");
            }
            else if (sqlType == "BIT")
            {
                sb.Add("iif");
                sb.Add("(");
                sb.AddObject(arg);
                sb.Add("<>");
                sb.Add("0");
                sb.Add(",");
                sb.Add(dialect.ToBooleanValueString(true));
                sb.Add(",");
                sb.Add(dialect.ToBooleanValueString(false));
                sb.Add(")");
            }
            else
            {
                sb.Add("(");
                sb.AddObject(arg);
                sb.Add(")");
            }

            return sb.ToSqlString();
        }

        public SqlString Render(IList args, ISessionFactoryImplementor factory)
        {
            if (args.Count != 2)
            {
                throw new QueryException("cast() requires two arguments");
            }
            string typeName = args[1].ToString();
            string sqlType;
            IType hqlType = TypeFactory.HeuristicType(typeName);

            if (hqlType != null)
            {
                SqlType[] sqlTypeCodes = hqlType.SqlTypes(factory);
                if (sqlTypeCodes.Length != 1)
                {
                    throw new QueryException("invalid NHibernate type for cast(), was:" + typeName);
                }
                sqlType = factory.Dialect.GetCastTypeName(sqlTypeCodes[0]);

                if (sqlType == null)
                {
                    //TODO: never reached, since GetTypeName() actually throws an exception!
                    sqlType = typeName;
                }

            }
            else
            {
                throw new QueryException(string.Format("invalid Hibernate type for cast(): type {0} not found", typeName));
            }

            return GetJetConvertionFunction(factory.Dialect, sqlType, args[0]);
        }

        #endregion

        #region IFunctionGrammar Members

        bool IFunctionGrammar.IsSeparator(string token)
        {
            return "as".Equals(token, StringComparison.InvariantCultureIgnoreCase);
        }

        bool IFunctionGrammar.IsKnownArgument(string token)
        {
            return false;
        }

        #endregion
    }
}