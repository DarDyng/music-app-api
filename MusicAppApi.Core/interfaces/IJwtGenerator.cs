﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAppApi.Core.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateToken(string email, string userId);
    }
}
