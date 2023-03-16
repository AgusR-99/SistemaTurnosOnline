﻿using System.Linq.Expressions;

namespace SistemaTurnosOnline.Web.Extensions
{
    public static class ExpressionHelper
    {
        public static string GetMemberName(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)expression).Member.Name;
                case ExpressionType.Convert:
                    return GetMemberName(((UnaryExpression)expression).Operand);
                default:
                    throw new NotSupportedException(expression.NodeType.ToString());
            }
        }
    }
}
