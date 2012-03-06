using System;
using System.Collections;
using NHibernate.Dialect.Function;
using NHibernate.Engine;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace NHibernate.JetDriver
{
    [Serializable]
    public class JetNullIfFunction : ISQLFunction, IFunctionGrammar
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

        public SqlString Render(IList args, ISessionFactoryImplementor factory)
        {
            if (args.Count != 2)
            {
                throw new QueryException("NullIf() requires two arguments");
            }

            var sb = new SqlStringBuilder();

            sb.Add("iif");
            sb.Add("(");
            sb.AddObject(args[0]);
            sb.Add("=");
            sb.AddObject(args[1]);
            sb.Add(",");
            sb.Add("NULL");
            sb.Add(",");
            sb.AddObject(args[0]);
            sb.Add(")");


            return sb.ToSqlString();
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