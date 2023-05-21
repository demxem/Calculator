using Sprache;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services
{
        public static class Calculator
        {
            public static Expression<Func<double>> ParseExpression(string expression)
            {
                return Lambda.Parse(expression);
            }

            static Parser<ExpressionType> Operator(string op, ExpressionType opType)
            {
                return Parse.String(op).Token().Return(opType);
            }

            static readonly Parser<ExpressionType> Add = Operator("+", ExpressionType.AddChecked);
            static readonly Parser<ExpressionType> Subtract = Operator("-", ExpressionType.SubtractChecked);
            static readonly Parser<ExpressionType> Multiply = Operator("*", ExpressionType.MultiplyChecked);
            static readonly Parser<ExpressionType> Divide = Operator("/", ExpressionType.Divide);
            static readonly Parser<ExpressionType> Modulo = Operator("%", ExpressionType.Modulo);
            static readonly Parser<ExpressionType> Power = Operator("^", ExpressionType.Power);
            static readonly Parser<ExpressionType> Root = Operator("", ExpressionType.Dynamic);
        
        static readonly Parser<Expression> Function =
                from name in Parse.Letter.AtLeastOnce().Text()
                from lparen in Parse.Char('(')
                from expr in Parse.Ref(() => Expr).DelimitedBy(Parse.Char(',').Token())
                from rparen in Parse.Char(')')
                select CallFunction(name, expr.ToArray());

            static Expression CallFunction(string name, Expression[] parameters)
            {
                var methodInfo = typeof(Math).GetTypeInfo().GetMethod(name, parameters.Select(e => e.Type).ToArray());
                if (methodInfo == null)
                    throw new ParseException(string.Format("Function '{0}({1})' does not exist.", name,
                                                           string.Join(",", parameters.Select(e => e.Type.Name))));

                return Expression.Call(methodInfo, parameters);
            }

            static readonly Parser<Expression> Constant =
                 Parse.Decimal
                 .Select(x => Expression.Constant(double.Parse(x)))
                 .Named("number");

            static readonly Parser<Expression> Factor =
                (from lparen in Parse.Char('(')
                 from expr in Parse.Ref(() => Expr)
                 from rparen in Parse.Char(')')
                 select expr).Named("expression")
                 .XOr(Constant)
                 .XOr(Function);

            static readonly Parser<Expression> Operand =
                ((from sign in Parse.Char('-')
                  from factor in Factor
                  select Expression.Negate(factor)
                 ).XOr(Factor)).Token();

            static readonly Parser<Expression> InnerTerm = Parse.ChainRightOperator(Power, Operand, Expression.MakeBinary);

            static readonly Parser<Expression> Term = Parse.ChainOperator(Multiply.Or(Divide).Or(Modulo), InnerTerm, Expression.MakeBinary);

            static readonly Parser<Expression> Expr = Parse.ChainOperator(Add.Or(Subtract), Term, Expression.MakeBinary);

            static readonly Parser<Expression<Func<double>>> Lambda =
                Expr.End().Select(body => Expression.Lambda<Func<double>>(body));

            public static string OperationType(string expression)
            {
                var sb = new StringBuilder();

                var query = expression.Where(x => x.Equals('-')
                            || x.Equals('+')
                            || x.Equals('*')
                            || x.Equals('^')
                            || x.Equals('/')
                            || x.Equals('%'));

                foreach (char ch in query)
                {
                    sb.Append(ch);
                }

                var operationType = sb.ToString();

                var combineOperations = new Regex(@"[+*/-^%]");
                var matches = combineOperations.Matches(operationType);

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
                if (expression.Contains('%'))
                {
                    return "Modulo";
                }
                if (expression.Contains('^'))
                {
                    return "Power";
                }
                return "Other";
            }

            public static double Calculate(string expression)
            {
                var operation = ParseExpression(expression);
                var func = operation.Compile();

                return func();
            }
        }
    }


