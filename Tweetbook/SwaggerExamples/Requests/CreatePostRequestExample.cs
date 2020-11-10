using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using Tweetbook.Contracts.V1.Requests;

namespace Tweetbook.SwaggerExamples.Requests
{
    public class CreatePostRequestExample : Swashbuckle.AspNetCore.Filters.IMultipleExamplesProvider<CreatePostRequest>
    {
        public IEnumerable<SwaggerExample<CreatePostRequest>> GetExamples()
        {
            yield return SwaggerExample.Create(
                "Simple post",
                new CreatePostRequest
                {
                    Name= "A brand new post",
                    Tags = new List<string> { "awesome" }
                }
                );

            yield return SwaggerExample.Create(
                "Another Simple post",
                new CreatePostRequest
                {
                    Name = "Another brand new post",
                    Tags = new List<string> { "awesom", "majestic" }
                }
                );
        }
    }
}
