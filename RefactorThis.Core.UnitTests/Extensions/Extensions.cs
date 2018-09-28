using RefactorThis.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

public static class RefactorThisExtensions
{
    public static void FeedDataContext(this ProductsContext context, IEnumerable<Product> products)
    {

        context.Product.AddRange(products);
        context.SaveContextChanges();
    }
}
