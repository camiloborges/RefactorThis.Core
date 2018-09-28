namespace RefactorThis.Core
{
    public class LoggingEvents
    {
        public const int GetAll = 1000;
        public const int SearchByName = 1001;
        public const int GetProduct = 1002;
        public const int AddProduct = 1003;
        public const int UpdateProduct = 1004;
        public const int DeleteProduct = 1005;

        public const int GetProductOptions = 2000;
        public const int GetProductOption = 2001;
        public const int AddProductOption = 2002;
        public const int UpdateProductOption = 2003;
        public const int DeleteProductOption = 2004;

        public const int GetProductNotFound = 4000;
        public const int UpdateProductNotFound = 4001;
        public const int GetProductOptionsNotFound = 4002;
    }
}