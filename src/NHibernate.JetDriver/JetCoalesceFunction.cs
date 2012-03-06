using System;
using System.Collections;
using NHibernate.Dialect.Function;
using NHibernate.Engine;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace NHibernate.JetDriver
{
    /// <summary>
    /// SQL COALESCE(expression1,...n)
    /// </summary>
    [Serializable]
    public class JetCoalesceFunction : ISQLFunction, IFunctionGrammar
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
            if (args.Count < 2)
            {
                throw new QueryException("coalesce() requires at least two arguments");
            }
            
            var sb = new SqlStringBuilder();

            sb.Add("Switch");
            sb.Add("(");
            for (int i = 0; i < args.Count; i++)
            {
                if (i>0)
                {
                    sb.Add(",");  
                }
                var arg = args[i];
                sb.Add("not IsNull(");
                sb.AddObject(arg);
                sb.Add(")");
                sb.Add(",");
                sb.AddObject(arg); 
            }

            sb.Add(",");  
            sb.Add("1=1");
            sb.Add(",");
            sb.AddObject(args[args.Count - 1]);

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