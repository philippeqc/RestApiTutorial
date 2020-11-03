using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Domain;
using Tweetbook.Extensions;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(_mapper.Map<List<PostResponse>>(posts));
        }

        /// <summary>
        /// Returns a requested post in the system
        /// </summary>
        /// <response code="200">Returns a requested post in the system</response>
        /// <response code="404">Unable to find the requested post in the system</response>
        [HttpGet(ApiRoutes.Posts.Get)]
        [ProducesResponseType(typeof(PostResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if (post == null)
                return NotFound();

            return Ok(_mapper.Map<PostResponse>(post));
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var post = await _postService.GetPostByIdAsync(postId);
            post.Name = request.Name;

            var updated = await _postService.UpdatePostAsync(post);

            if (updated)
                return Ok(_mapper.Map<PostResponse>(post));

            return NotFound();
        }

        /// <summary>
        /// Deletes a post in the system
        /// </summary>
        /// <response code="200">Deleted the post in the system</response>
        /// <response code="400">Must own the post to delete it</response>
        /// <response code="404">Unable to find the requested post in the system</response>
        [HttpDelete(ApiRoutes.Posts.Delete)]
        //[ProducesResponseType(typeof(TagResponse), 201)]
        //[ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not own this post" } } });
            }

            var deleted = await _postService.DeletePostAsync(postId);

            if (deleted)
                return Ok();

            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var newPostId = Guid.NewGuid();
            var post = new Post
            {
                Id = newPostId,
                Name = postRequest.Name,
                UserId = HttpContext.GetUserId(),
                Tags = postRequest.Tags.Select(x => new PostTag { PostId = newPostId, TagName = x }).ToList()
            };

            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = _mapper.Map<PostResponse>(post);
            return Created(locationUri, response);
        }
    }
}
