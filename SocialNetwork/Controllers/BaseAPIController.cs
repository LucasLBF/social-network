using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models;

namespace SocialNetwork.API.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected ValidationResponseModel ValidationModel = new ValidationResponseModel();
    }
}
