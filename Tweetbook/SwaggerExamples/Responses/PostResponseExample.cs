using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1.Responses;

namespace Tweetbook.SwaggerExamples.Responses
{
    public class PostResponseExample : IExamplesProvider<PostResponse>
    {
        public PostResponse GetExamples()
        {
            return new PostResponse
            {
                Id = Guid.Parse("b69b8273-aa36-46f1-3294-08d87c18baac"),
                Name = "First Post",
                UserId = "7fc50493-1814-431b-8076-0cb7846ba937",
                Tags = new List<TagResponse> {
                    new TagResponse {
                        Name = "amazing"
                    },
                    new TagResponse {
                        Name = "aspnetcore"
                    }
                }
            };
        }
    }
}
