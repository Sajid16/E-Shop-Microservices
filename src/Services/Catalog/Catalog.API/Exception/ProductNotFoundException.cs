using BuildingBlocks.Exceptions;

namespace Catalog.API.Exception
{
    #region with builtin exception
    //public class ProductNotFoundException : IOException
    #endregion

    #region with custom exception that extends exception class
    public class ProductNotFoundException : NotFoundException
    #endregion
    {
        public ProductNotFoundException(Guid Id) : base("Product", Id)
        {
                
        }
    }
}
