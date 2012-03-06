using System;
using System.Collections.Generic;
using NHibernate.SqlCommand;

namespace NHibernate.JetDriver.SqlFixes
{
    /// <summary>Base Class to Execute Jet Fixes altering NHibernate SqlString</summary>
    /// <remarks>Ricardo, 2/6/2010.</remarks>
    public abstract class SqlStringFix
    {

        public abstract string FixSql(string sql);


        /// <summary>Split the parts of SqlString </summary>
        /// <param name="sqlString">SqlString to split</param>
        /// <returns>Collection of SqlString parts</returns>
        public static List<object> SplitParts(SqlString sqlString)
        {
            var parts = sqlString.Parts;
            var partList = new List<object>();

            foreach (var part in parts)
            {
                partList.Add(part);
            }

            return partList;
        }

        /// <summary>Join colletion of  SqlString parts </summary>
        /// <param name="partList">Collection of SqlString parts</param>
        /// <returns>Joined SqlString</returns>
        public static SqlString JoinParts(List<object> partList)
        {
            var final = new SqlStringBuilder();
            foreach (var part in partList)
            {
                if (part is string)
                {
                    final.Add((string)part);
                }
                else
                {
                    final.Add((Parameter)part);
                }

            }

            return final.ToSqlString();
        }

        /// <summary>Find position SqlString token position </summary>
        /// <param name="token">token to find. ex: "then" </param>
        /// <param name="start">Start Index</param>
        /// <param name="partList">colletion of  SqlString parts</param>
        /// <returns>position of the token or -1 if token not found.</returns>
        protected int Position(string token, int start, List<object> partList)
        {
            int position = -1;
            position = Position(new string[] { token }, start, partList);
            return position;
        }

        /// <summary>Find position SqlString token position.</summary>
        /// <param name="tokens">multipart token to find. ex: "is null".</param>
        /// <param name="start">Start Index.</param>
        /// <param name="partList">colletion of  SqlString parts.</param>
        /// <returns>position of the multipart token or -1 if token not found.</returns>
        protected int Position(string[] tokens, int start, List<object> partList)
        {
            int position = -1;
            int index = 0;
            bool tokenFound = false;
            for (int i = start; i < partList.Count; i++)
            {
                tokenFound = true;
                for (int j = 0; j < tokens.Length; j++)
                {
                    index = i + j;

                    if (index >= partList.Count)
                    {
                        tokenFound = false;
                        break;
                    }

                    tokenFound = tokenFound & (partList[index].ToString().Trim() == tokens[j]);

                    if (!tokenFound)
                    {
                        break;
                    }
                }

                if (tokenFound)
                {
                    position = i;
                    break;
                }

            }
            return position;
        }

    }
}