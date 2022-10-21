namespace D2W.Application.Common.Extensions;

public static class LinqExtensions
{
    #region Public Methods

    public static IQueryable<T> IncludeHierarchy<T>(this IQueryable<T> source,
        uint depth, string propertyName)
        where T : class
    {
        var temp = source;

        for (var i = 1; i <= depth; i++)
        {
            var sb = new StringBuilder();

            for (var j = 0; j < i; j++)
            {
                if (j > 0)
                {
                    sb.Append(".");
                }

                sb.Append(propertyName);
            }

            var path = sb.ToString();

            temp = temp.Include(path);
        }

        var result = temp;

        return result;
    }

    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name)
    {
        var propInfo = GetPropertyInfo(typeof(T), name);

        var expr = GetOrderExpression(typeof(T), propInfo);

        var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);

        if (method is null)
            throw new ArgumentNullException(nameof(method), Resource.Method_cannot_be_null);

        var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

        return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
    }

    public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> query, string name)
    {
        var propInfo = GetPropertyInfo(typeof(T), name);

        var expr = GetOrderExpression(typeof(T), propInfo);

        var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);

        if (method is null)
            throw new ArgumentNullException(nameof(method), Resource.Method_cannot_be_null);

        var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

        return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
    {
        var propInfo = GetPropertyInfo(typeof(T), name);

        var expr = GetOrderExpression(typeof(T), propInfo);

        var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);

        if (method is null)
            throw new ArgumentNullException(nameof(method), Resource.Method_cannot_be_null);

        var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

        return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
    }

    public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string name)
    {
        var propInfo = GetPropertyInfo(typeof(T), name);

        var expr = GetOrderExpression(typeof(T), propInfo);

        var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);

        if (method is null)
            throw new ArgumentNullException(nameof(method), Resource.Method_cannot_be_null);

        var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

        return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
    }

    public static IEnumerable<T> SortBy<T>(this IEnumerable<T> query, string sort)
    {
        var propertyAndOrder = sort.Split(new[] { ' ' });

        var property = propertyAndOrder[0];

        var order = propertyAndOrder[1];

        var propInfo = GetPropertyInfo(typeof(T), property);

        var expr = GetOrderExpression(typeof(T), propInfo);

        var method = order is "asc" or "Ascending"
            ? typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
            : typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);

        if (method is null)
            throw new ArgumentNullException(nameof(method), Resource.Method_cannot_be_null);

        var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

        return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
    }

    public static IQueryable<T> SortBy<T>(this IQueryable<T> query, string sort)
    {
        var propertyAndOrder = sort.Split(new[] { ' ' });

        var property = propertyAndOrder[0];

        var order = propertyAndOrder[1];

        var propInfo = GetPropertyInfo(typeof(T), property);

        var expr = GetOrderExpression(typeof(T), propInfo);

        var method = order is "asc" or "Ascending"
            ? typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
            : typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);

        if (method is null)
            throw new ArgumentNullException(nameof(method), Resource.Method_cannot_be_null);

        var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

        return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber = 1, int totalRowsPerPage = 10) where T : class
    {
        var result = new PagedList<T>
        {
            CurrentPage = pageNumber != 0 ? pageNumber : 1,
            TotalRowsPerPage = totalRowsPerPage != 0 ? totalRowsPerPage : 10,
            TotalRows = query.Count()
        };

        if (totalRowsPerPage != -1)
        {
            var totalPages = (double)result.TotalRows / result.TotalRowsPerPage;
            result.TotalPages = (int)Math.Ceiling(totalPages);
            var skip = (result.CurrentPage - 1) * result.TotalRowsPerPage;
            result.Items = await query.Skip(skip).Take(result.TotalRowsPerPage).ToListAsync();
        }
        else
        {
            result.Items = await query.ToListAsync();
        }
        return result;
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IEnumerable<T> query, int pageNumber = 1, int totalRowsPerPage = 10) where T : class
    {
        var enumerable = query.ToList();

        var result = new PagedList<T>
        {
            CurrentPage = pageNumber != 0 ? pageNumber : 1,
            TotalRowsPerPage = totalRowsPerPage != 0 ? totalRowsPerPage : 10,
            TotalRows = enumerable.Count()
        };

        if (totalRowsPerPage != -1)
        {
            var totalPages = (double)result.TotalRows / result.TotalRowsPerPage;
            result.TotalPages = (int)Math.Ceiling(totalPages);
            var skip = (result.CurrentPage - 1) * result.TotalRowsPerPage;
            result.Items = await Task.FromResult(enumerable.Skip(skip).Take(result.TotalRowsPerPage).ToList());
        }
        else
        {
            result.Items = await Task.FromResult(enumerable.ToList());
        }

        return result;
    }

    #endregion Public Methods

    #region Private Methods

    private static PropertyInfo GetPropertyInfo(Type objType, string name)
    {
        var properties = objType.GetProperties();

        var matchedProperty = properties.FirstOrDefault(p => p.Name.ToUpper() == name.ToUpper());

        if (matchedProperty == null)
            throw new ArgumentException("name");

        return matchedProperty;
    }

    private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
    {
        var paramExpr = Expression.Parameter(objType);

        var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);

        var expr = Expression.Lambda(propAccess, paramExpr);

        return expr;
    }

    #endregion Private Methods
}