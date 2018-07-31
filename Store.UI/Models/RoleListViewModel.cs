﻿using Microsoft.AspNet.Identity.EntityFramework;
using Store.Domain;
using Store.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.UI.Models
{
    public class RoleListViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; } = Enumerable.Empty<IdentityRole>();
    }
}