using RefactorThis.Core.Models;
using RefactorThis.Infrastructure.Data;
using System.Collections.Generic;

public static class RefactorThisExtensions
{
    public static void FeedDataContext(this ProductsContext context, IEnumerable<Product> products)
    {
        context.Product.AddRange(products);
        context.SaveContextChanges();
    }
}