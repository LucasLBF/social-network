using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models;

namespace SocialNetwork.API.Controllers
{
    public abstract class BaseAPIController : ControllerBase
    {
        public ValidationResponseModel ValidationModel = new ValidationResponseModel();
    }
}
