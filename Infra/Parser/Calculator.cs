using Sprache;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class Calculator : ICalculator
    {
        private static readonly Parser<Expression> Constant =
            Parse.DecimalInvariant
                .Select(n => double.Parse(n, CultureInfo.InvariantCulture))
                .Select(n => Expression.Constant(n, typeof(double)))
                .Token();

        private static readonly Parser<ExpressionType> Operator =
            Parse.Char('+').Return(ExpressionType.Add)
                .Or(Parse.Char('-').Return(ExpressionType.Subtract))
                .Or(Parse.Char('*').Return(ExpressionType.Multiply))
                .Or(Parse.Char('/').Return(ExpressionType.Divide))
                .Or(Parse.Char('^').Return(ExpressionType.Power));
                

        private static readonly Parser<Expression> Operation =
            Parse.ChainOperator(Operator, Constant, Expression.MakeBinary);

        private static readonly Parser<Expression> FullExpression =
            Operation.Or(Constant).End();

        public string OperationType(string expression)
        {
            var combineOperations = new Regex(@"[+*/-^]");
            var matches = combineOperations.Matches(expression);

            // If arithmetic operators two or more type - combine
            if (matches.Count > 1)
            {
                return "Combine";
            }

            if (expression.Contains('+'))
            {
                return "Addition";
            }
            if (expression.Contains('-'))
            {
                return "Subtract";
            }
            if (expression.Contains('*'))
            {
                return "Multiply";
            }
            if (expression.Contains('/'))
            {
                return "Divide";
            }
            if (expression.Contains('^'))
            {
                return "Power";
            }

            return "Other";
        }

      
        public double Calculate(string expression)
        {
            var operation = FullExpression.Parse(expression);
            var func = Expression.Lambda<Func<double>>(operation).Compile();

            return func();
        }
    }
}
