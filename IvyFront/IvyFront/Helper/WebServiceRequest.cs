﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront
{
    class WebServiceRequest : IRequest
    {

        private string url = "";
        public WebServiceRequest()
        {
            url = Appsetting.ws_svr;
        }

        string IRequest.request(string par_url, string context)
        {
            Request req = new Request();
            return req.request(par_url, context);


            //IvyFrontService.ServiceBase req = new IvyFrontService.ServiceBase();
            //string[] arr = par_url.Split('?');
            //req.Url = url + arr[0];
            //return req.Request(arr[1].Substring(2), context);
        }

    }
}
