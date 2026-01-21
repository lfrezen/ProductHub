namespace ProductHub.Application.Products.GetProductById;

public sealed class GetProductByIdQuery
{
    public Guid Id { get; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}
