using BigBlueButton.Net.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.BaseClasses
{
    public class BasePostFileRequest : BaseRequest
    {
        public FileContentData file { get; set; }
    }
}
