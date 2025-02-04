using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Params
{
    public class AuthorIdParam
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
