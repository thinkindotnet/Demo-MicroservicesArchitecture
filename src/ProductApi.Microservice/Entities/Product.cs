namespace ProductApi.Microservice.Entities;


public class Product
{

    // NOTE: In EF6 and above:
    //       - the first column, is marked as the PRIMARY KEY Column
    //       - if numeric/uniqueidentifier, it is also marked as an IDENTITY Column
    public int ProductId { get; set; }


    public string ProductName { get; set; } = string.Empty;


    public int ProductCategoryId { get; set; }

}
