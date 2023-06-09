﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityMap.Models;

namespace UniversityMap.Data
{
    public class UniversityMapContext : IdentityDbContext<User>
    {
        public UniversityMapContext (DbContextOptions<UniversityMapContext> options)
            : base(options) { }

        public DbSet<UniversityMap.Models.Panorama> Panoramas { get; set; } = default!;
        public DbSet<UniversityMap.Models.Map> Maps { get; set; } = default!;
    }
}
