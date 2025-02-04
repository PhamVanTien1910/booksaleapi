using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Params
{
    public class BookIdParam
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
