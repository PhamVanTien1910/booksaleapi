using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Params
{
    public class UserIdParam
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
