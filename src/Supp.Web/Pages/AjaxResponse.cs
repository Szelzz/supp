using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Web.Pages
{
    public class AjaxResponse : JsonResult
    {
        public AjaxResponse(object data)
            : base(new ResponseObject()
            {
                Succeeded = true,
                Data = data
            })
        {
        }

        public AjaxResponse(string error)
            : base(new ResponseObject()
            {
                Succeeded = false,
                Errors = new List<string>() { error }
            })
        {
        }

        public AjaxResponse(params string[] errors)
            : base(new ResponseObject()
            {
                Succeeded = false,
                Errors = errors.ToList()
            })
        {
        }

        public AjaxResponse(bool succeeded, object data)
            : base(new ResponseObject()
            {
                Succeeded = succeeded,
                Data = data
            })
        {
        }

        class ResponseObject
        {
            public bool Succeeded { get; set; }
            public List<string> Errors { get; set; }
                = new List<string>();
            public object Data { get; set; }
        }
    }
}
