// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.ExpressionTranslator
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class ExpressionTranslator : ExpressionVisitor
{
  private readonly Dictionary<string, object> parameters = new Dictionary<string, object>();
  private readonly StringBuilder sb = new StringBuilder();
  private int paramNum = 1000;

  protected ExpressionTranslator()
  {
  }

  public static TranslatorResult Translate(Expression expression)
  {
    ExpressionTranslator expressionTranslator = new ExpressionTranslator();
    expressionTranslator.Visit(expression);
    return new TranslatorResult(expressionTranslator.sb.ToString().Replace("<> NULL", "IS NOT NULL").Replace("= NULL", "IS NULL"), expressionTranslator.parameters);
  }

  private static Expression StripQuotes(Expression e)
  {
    while (e.NodeType == ExpressionType.Quote)
      e = ((UnaryExpression) e).Operand;
    return e;
  }

  protected override Expression VisitMethodCall(MethodCallExpression m)
  {
    if (!(m.Method.DeclaringType == typeof (string)) || !(m.Method.Name == "StartsWith"))
      throw new NotSupportedException($"The method '{m.Method.Name}' is not supported");
    this.sb.Append(((MemberExpression) m.Object).Member.Name);
    this.sb.Append(" LIKE ");
    this.AddParameter((object) (((ConstantExpression) m.Arguments[0]).Value?.ToString() + "%"));
    return (Expression) m;
  }

  protected override Expression VisitUnary(UnaryExpression u)
  {
    switch (u.NodeType)
    {
      case ExpressionType.Convert:
        this.Visit(u.Operand);
        break;
      case ExpressionType.Not:
        this.sb.Append(" NOT ");
        this.Visit(u.Operand);
        break;
      default:
        throw new NotSupportedException($"The unary operator '{u.NodeType}' is not supported");
    }
    return (Expression) u;
  }

  protected override Expression VisitBinary(BinaryExpression b)
  {
    this.sb.Append("(");
    this.Visit(b.Left);
    switch (b.NodeType)
    {
      case ExpressionType.AndAlso:
        this.sb.Append(" AND ");
        break;
      case ExpressionType.Equal:
        this.sb.Append(" = ");
        break;
      case ExpressionType.GreaterThan:
        this.sb.Append(" > ");
        break;
      case ExpressionType.GreaterThanOrEqual:
        this.sb.Append(" >= ");
        break;
      case ExpressionType.LessThan:
        this.sb.Append(" < ");
        break;
      case ExpressionType.LessThanOrEqual:
        this.sb.Append(" <= ");
        break;
      case ExpressionType.NotEqual:
        this.sb.Append(" <> ");
        break;
      case ExpressionType.OrElse:
        this.sb.Append(" OR ");
        break;
      default:
        throw new NotSupportedException($"The binary operator '{b.NodeType}' is not supported");
    }
    this.Visit(b.Right);
    this.sb.Append(")");
    return (Expression) b;
  }

  protected override Expression VisitConstant(ConstantExpression c)
  {
    if (c.Value == null)
      this.sb.Append("NULL");
    else if (c.Value is bool)
      this.sb.Append((bool) c.Value ? "1" : "0");
    else
      this.AddParameter(c.Value);
    return (Expression) c;
  }

  protected override Expression VisitMemberAccess(MemberExpression m)
  {
    if (m.Expression != null)
    {
      if (m.Expression.NodeType == ExpressionType.Parameter)
      {
        string key = EntityHelper.For(m.Member.DeclaringType).ColumnPropertyMap.Single<KeyValuePair<string, PropertyHelper>>((Func<KeyValuePair<string, PropertyHelper>, bool>) (pair => pair.Value.PropertyName == m.Member.Name)).Key;
        this.sb.Append("[T1].[").Append(key).Append("]");
        return (Expression) m;
      }
      if (m.Expression.NodeType == ExpressionType.MemberAccess || m.Expression.NodeType == ExpressionType.Constant)
      {
        this.AddParameter(Expression.Lambda((Expression) m).Compile().DynamicInvoke());
        return (Expression) m;
      }
    }
    throw new NotSupportedException($"The member '{m.Member.Name}' is not supported");
  }

  protected void AddParameter(object value)
  {
    ++this.paramNum;
    this.parameters.Add("p" + this.paramNum.ToString(), value);
    this.sb.Append("@p" + this.paramNum.ToString());
  }
}
