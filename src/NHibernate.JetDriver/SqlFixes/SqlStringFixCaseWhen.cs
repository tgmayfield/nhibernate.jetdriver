using System;
using System.Text.RegularExpressions;
using MacroScope;

namespace NHibernate.JetDriver.SqlFixes
{
    /// <summary>Fix "Case When then else end" SQL with Access Switch. 
    ///          example test NegativeTimesheetsWithEqualsFalse   
    /// </summary>
    public class SqlStringFixCaseWhen : SqlStringFix
    {

        private static Regex _regexCaseWhen = new Regex(
                  "case[^\\w](.*?)[^\\w]end",
                RegexOptions.IgnoreCase
                | RegexOptions.Singleline
                | RegexOptions.CultureInvariant
                );

        public override string FixSql(string sql)
        {

            if (!_regexCaseWhen.IsMatch(sql))
            {
                return sql;
            }

            var matches = _regexCaseWhen.Matches(sql);

            foreach (Match match in matches)
            {
                IStatement statement = Factory.CreateStatement("SELECT XXXX," + match.Value + " FROM YYYY");

                var tailor = new MAccessTailor();
                statement.Traverse(tailor);
                Stringifier stringifier = new Stringifier();
                statement.Traverse(stringifier);
                var sqlCaseWhenFixed = stringifier.ToSql();
                sqlCaseWhenFixed = sqlCaseWhenFixed.Replace("SELECT XXXX,", "");
                sqlCaseWhenFixed = sqlCaseWhenFixed.Replace("FROM YYYY", "");

                sql = sql.Replace(match.Value, sqlCaseWhenFixed);
            }

            return sql;

        }
    }
}