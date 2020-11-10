using Swashbuckle.AspNetCore.Filters;
using Tweetbook.Contracts.V1.Requests;

namespace Tweetbook.SwaggerExamples.Requests
{
    public class UserLoginRequestExample : IExamplesProvider<UserLoginRequest>
    {
        public UserLoginRequest GetExamples()
        {
            return new UserLoginRequest
            {
                Email = "nickfury@shield.com",
                Password = "Foobar!0"
            };
        }
    }
}
