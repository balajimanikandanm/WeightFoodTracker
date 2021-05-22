using System;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.Controllers
{
    public interface IUsersController
    {
        public IActionResult Post(LoginRequest request);
        public IActionResult GetAll();

    }
}