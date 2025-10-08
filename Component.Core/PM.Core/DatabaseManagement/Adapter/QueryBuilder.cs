// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.QueryBuilder
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class QueryBuilder
{
  private readonly LoadEntityOption rootLoadEntityOption;
  private StringBuilder fromBuilder;
  private bool loadHistory;
  private StringBuilder selectBuilder;
  private int tableCount;
  private Dictionary<string, int> tableNumberMap;

  public QueryBuilder(LoadEntityOption loadEntityOption)
  {
    this.rootLoadEntityOption = loadEntityOption != null ? loadEntityOption : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (loadEntityOption));
  }

  public string Select { get; private set; }

  public string From { get; private set; }

  public static string GetCompleteSelectClause(
    LoadEntityOption loadEntityOption,
    string whereCondition,
    bool loadHistory)
  {
    QueryBuilder queryBuilder = new QueryBuilder(loadEntityOption);
    queryBuilder.BuildSelectQuery(loadHistory);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(queryBuilder.Select);
    stringBuilder.Append(queryBuilder.From);
    stringBuilder.Append(" ");
    if (!string.IsNullOrEmpty(whereCondition))
    {
      whereCondition = whereCondition.Trim();
      if (!whereCondition.ToUpper().StartsWith("WHERE"))
        stringBuilder.Append("WHERE ");
      stringBuilder.Append(whereCondition);
    }
    return stringBuilder.ToString();
  }

  public static string GetCompleteSelectClause(
    LoadEntityOption loadEntityOption,
    IEnumerable<object> ids,
    out Dictionary<string, object> parameters)
  {
    int num = 1;
    parameters = new Dictionary<string, object>();
    foreach (object id in ids)
      parameters.Add("Id" + num++.ToString(), id);
    string whereCondition = $"WHERE T1.[{EntityHelper.For(loadEntityOption.LoadType).PrimaryKeyName}] IN ({"@" + string.Join(", @", parameters.Keys.ToArray<string>())})";
    return QueryBuilder.GetCompleteSelectClause(loadEntityOption, whereCondition, false);
  }

  public void BuildSelectQuery(bool loadHistory)
  {
    this.loadHistory = loadHistory;
    this.Init();
    if (loadHistory)
      this.selectBuilder.Append("HistoryVersion").Append(", ").Append("HistoryTimestamp").Append(", ").Append("HistoryUserName").Append(", ").Append("HistoryUserID").Append(", ").Append("HistoryAction").Append(", ");
    this.VisitForSelect(this.rootLoadEntityOption);
    this.BuildResult();
  }

  private int GetTableNumber(string prefixedTableName)
  {
    int tableNumber;
    if (this.tableNumberMap.TryGetValue(prefixedTableName, out tableNumber))
      return tableNumber;
    ++this.tableCount;
    this.tableNumberMap.Add(prefixedTableName, this.tableCount);
    return this.tableCount;
  }

  private string GetProcessedTableName(string tableName)
  {
    return tableName + (this.loadHistory ? "History" : "");
  }

  private void VisitForSelect(LoadEntityOption loadEntityOption)
  {
    string str1;
    if (loadEntityOption.Parent == null)
    {
      string tableName = loadEntityOption.EntityHelper.TableName;
      str1 = "T" + this.GetTableNumber(loadEntityOption.Prefix + tableName).ToString();
      this.fromBuilder.AppendFormat("[{0}] {1}", (object) this.GetProcessedTableName(tableName), (object) str1);
    }
    else
    {
      RelationJoinInfo relationJoinInfo = loadEntityOption.RelationJoinInfo;
      str1 = "T" + this.GetTableNumber(loadEntityOption.Parent.Prefix + loadEntityOption.Parent.EntityHelper.TableName).ToString();
      for (; relationJoinInfo != null; relationJoinInfo = relationJoinInfo.ConsecutiveJoin)
      {
        string str2 = "T" + this.GetTableNumber(loadEntityOption.Prefix + relationJoinInfo.JoinTableName).ToString();
        this.fromBuilder.AppendFormat(" LEFT JOIN [{0}] {1} ON {2}.[{3}]={4}.[{5}]", (object) this.GetProcessedTableName(relationJoinInfo.JoinTableName), (object) str2, (object) str1, (object) relationJoinInfo.TableKey, (object) str2, (object) relationJoinInfo.JoinTableKey);
        if (relationJoinInfo.JoinConditionColumn != null)
          this.fromBuilder.AppendFormat(" AND {0}.[{1}]='{2}'", (object) str2, (object) relationJoinInfo.JoinConditionColumn, relationJoinInfo.JoinConditionValue);
        if (relationJoinInfo.ConsecutiveJoin != null)
        {
          foreach (KeyValuePair<string, PropertyHelper> relationValueProperty in loadEntityOption.EntityHelper.RelationValuePropertyMap)
            this.selectBuilder.AppendFormat("{0}.[{1}] AS [{2}{3}], ", (object) str2, (object) relationValueProperty.Key, (object) loadEntityOption.Prefix, (object) relationValueProperty.Value.PropertyName);
        }
        str1 = str2;
      }
    }
    foreach (string key in loadEntityOption.EntityHelper.ColumnPropertyMap.Keys)
      this.selectBuilder.AppendFormat("{0}.[{1}] AS [{2}{3}], ", (object) str1, (object) key, (object) loadEntityOption.Prefix, (object) key);
    foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) loadEntityOption.Children)
      this.VisitForSelect(child);
  }

  private void Init()
  {
    this.tableCount = 0;
    this.tableNumberMap = new Dictionary<string, int>();
    this.selectBuilder = new StringBuilder("SELECT ");
    this.fromBuilder = new StringBuilder(" FROM ");
  }

  private void BuildResult()
  {
    this.selectBuilder.Length -= 2;
    this.Select = this.selectBuilder.ToString();
    this.From = this.fromBuilder.ToString();
  }
}
