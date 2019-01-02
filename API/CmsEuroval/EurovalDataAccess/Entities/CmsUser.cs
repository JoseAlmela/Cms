using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EurovalDataAccess.Entities
{
    public class CmsUser : IdentityUser
    {
        public bool ApiUser { get; set; }
    }
}
