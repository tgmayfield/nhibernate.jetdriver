using System;
using System.Collections;
using NHibernate.Dialect.Function;
using NHibernate.Engine;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace NHibernate.JetDriver
{
    /// <summary>Jet locate function. </summary>
    /// <remarks>
    ///    	SQL:LOCATE( stringToFind, stringToSearch  [, start] )
    ///     ACCESS: Instr ( [start], stringToSearch, stringToFind)
    /// </remarks>
    [Serializable]
    public class JetLocateFunction : ISQLFunction, IFunctionGrammar
    {
        #region ISQLFunction Members

        public IType ReturnType(IType columnType, IMapping mapping)
        {
            return NHibernateUtil.Int32;
        }

        public bool HasArguments
        {
            get { return true; }
        }

        public bool HasParenthesesIfNoArguments
        {
            get { return true; }
        }

        public SqlString Render(IList args, ISessionFactoryImplementor factory)
        {
            if (args.Count != 3)
            {
                throw new QueryException("locates requires three arguments");
            }

            
            var sqlBuilder =  new SqlStringBuilder();

            sqlBuilder.Add("locate(")
                .AddObject(args[0])
                .Add(",")
                .AddObject(args[1])
                .Add(",")
                .AddObject(args[2])
                .Add(")");
                
            return sqlBuilder.ToSqlString();
        }


        #endregion

        #region IFunctionGrammar Members

        bool IFunctionGrammar.IsSeparator(string token)
        {
            return false;
        }

        bool IFunctionGrammar.IsKnownArgument(string token)
        {
            return false;
        }

        #endregion
    }
}