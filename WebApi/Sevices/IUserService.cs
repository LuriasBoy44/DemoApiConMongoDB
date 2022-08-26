using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Response;
using WebApi.Request;

namespace WebApi.Sevices
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
