using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQB.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Id4Server.Controller.WX
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WXController : ControllerBase
    {
        [HttpGet]
        public void UserInfo()
        {
            NLogUtil.Id4_InfoTxt("UserIno");
         //  HttpContext.Response.Redirect("");
        }

        [HttpGet]
        public void LoginCallBack()
        {
            NLogUtil.Id4_InfoTxt("LoginCallBack");
        }
    }
}