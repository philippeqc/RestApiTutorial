using AutoMapper;
using Tweetbook.Contracts.V1.Requests.Queries;
using Tweetbook.Domain;
using Tweetbook.Filters;

namespace Tweetbook.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<GetAllPostsQuery, GetAllPostsFilter>();
        }
    }
}
