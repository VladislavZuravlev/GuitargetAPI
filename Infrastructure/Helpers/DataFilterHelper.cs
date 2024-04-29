using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Infrastructure.Contexts;

namespace Infrastructure.Helpers;

public class DataFilterHelper
{
    private static string[] stringOperators = new[] { "Contains", "StartsWith", "EndsWith" };
    private static string[] dateOperators = new[] { "OnDate", "NotOnDate", "AfterDate", "BeforeDate" };
    private static string[] nullOperators = new[] { "IsNull", "IsNotNull" };
    
    
    
    
    
    public static void ApplyFilterCollection<TEntity>(IEnumerable<Tuple<string, string, object>>? filterCollection, ref IQueryable<TEntity> query)
    {
        if (filterCollection != null)
        {
            foreach (var filter in filterCollection)
            {
                var predicate = GetFilterPredicateWithParams(filter);

                query = query.Where(predicate.Item1, predicate.Item2);
            }
        }
    }

    public static Tuple<string, object[]> GetFilterPredicateWithParams(Tuple<string, string, object> filter)
    {
        string predicate;
        var paramList = new List<object>();


        if (stringOperators.Contains(filter.Item2))
        {
            if (filter.Item1.Contains(".Any"))
            {
                predicate = filter.Item1 + "." + filter.Item2 + "(@0)" + ")";
                paramList.Add(filter.Item3);
            }
            else
            {
                predicate = filter.Item1 + "." + filter.Item2 + "(@0)";
                paramList.Add(filter.Item3);
            }
        }
        else if (dateOperators.Contains(filter.Item2))
        {
            predicate = filter.Item1;
            var isIenumerablePropery = filter.Item1.Contains(".Any(");
            var currentDate = ((DateTime)filter.Item3).Date;
            var nextDate = currentDate.AddDays(1);
            if (filter.Item2 == "OnDate")
            {
                predicate += 
                    " >= @0 AND " 
                    +
                    (isIenumerablePropery ? filter.Item1.Remove(0, filter.Item1.LastIndexOf(".Any(") + 5) : filter.Item1)
                    + 
                    " < @1";
                paramList.Add(currentDate);
                paramList.Add(nextDate);
            }
            else if (filter.Item2 == "NotOnDate")
            {
                predicate += 
                    " < @0 OR "
                    +
                    (isIenumerablePropery ? filter.Item1.Remove(0, filter.Item1.LastIndexOf(".Any(") + 5) : filter.Item1)
                    + 
                    " >= @1";
                paramList.Add(currentDate);
                paramList.Add(nextDate);
            }
            else if (filter.Item2 == "AfterDate")
            {
                predicate += " >= @0";
                paramList.Add(currentDate);
            }
            else if (filter.Item2 == "BeforeDate")
            {
                predicate += " < @0";
                paramList.Add(nextDate);
            }
            else // Never
            {
                predicate = "";
            }

            if (isIenumerablePropery)
            {
                predicate += ")";
            }
        }
        else if (nullOperators.Contains(filter.Item2))
        {
            if (filter.Item1.Contains(".Any"))
            {
                predicate = filter.Item1 + " " + (filter.Item2 == "IsNull" ? "==" : "!=") + " null)";
            }
            else
            {
                predicate = filter.Item1 + " " + (filter.Item2 == "IsNull" ? "==" : "!=") + " null";
            }
        }
        else if (filter.Item2 == "PredefinedExpression")
        {
            predicate = filter.Item1;
            paramList.Add(filter.Item3);
        }
        else if (filter.Item2 == "PredefinedExpressionWithValue")
        {
            predicate = filter.Item1;
        }
        else
        {
            if (filter.Item1.Contains(".Any"))
            {
                predicate = filter.Item1 + " " + filter.Item2 + " @0" + ")";
                paramList.Add(filter.Item3);
            }
            else
            {
                predicate = filter.Item1 + " " + filter.Item2 + " @0";
                paramList.Add(filter.Item3);
            }
        }


        return new Tuple<string, object[]>(predicate, paramList.ToArray());
    }

    public static void ApplyOrdering<TEntity>(Dictionary<string, string>? orderCollection, ref IQueryable<TEntity> query)
    {
        if (orderCollection != null && orderCollection.Count != 0)
        {
            var sortString = new StringBuilder();
            foreach (var order in orderCollection)
            {
                string sortDir = !String.IsNullOrEmpty(order.Value) ? order.Value : "ASC";
                if (sortString.Length > 0)
                {
                    sortString.Append(",");
                }
                sortString.Append(order.Key + " " + sortDir);
            }
            query = (IQueryable<TEntity>) query.OrderBy(sortString.ToString());
        }
    }
    
    
}