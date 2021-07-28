using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BigSchool_1.DTOs;
using BigSchool_1.Models;
using Microsoft.AspNet.Identity;

namespace BigSchool_1.Controllers.Api
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext db;
        public FollowingsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: api/Followings
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            if (db.followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
            {
                return BadRequest("Following already exists!");
            }
            if(userId == followingDto.FolloweeId){
                return BadRequest("Not Follow yourself!");
            }
            else
            {
                var following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = followingDto.FolloweeId
                };
                db.followings.Add(following);
                db.SaveChanges();
                return Ok();
            }
                  
        }

        [HttpDelete]
        public IHttpActionResult DeleteFollowing(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = db.followings.SingleOrDefault(a => a.FollowerId == userId && a.FolloweeId == id);
            if (following == null)
            {
                return NotFound();
            }

            db.followings.Remove(following);
            db.SaveChanges();

            return Ok(following);
        }
    }
}