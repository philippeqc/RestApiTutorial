﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Tweetbook.Contracts.V1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    public class PostsController : Controller
    {
        private IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postService.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            var post = _postService.GetPostById(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post { Id = postId, Name = request.Name };

            var updated = _postService.UpdatePost(post);

            if (updated)
                return Ok(post);

            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Id = postRequest.Id };

            if (Guid.Empty == post.Id)
                post.Id = Guid.NewGuid();

            _postService.GetPosts().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };
            return Created(locationUri, response);
        }
    }
}
