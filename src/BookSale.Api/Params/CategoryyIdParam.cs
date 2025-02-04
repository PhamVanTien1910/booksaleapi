using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Params
{
    public class CategoryyIdParam
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
