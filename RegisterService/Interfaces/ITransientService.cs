﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterService.Interfaces
{
    public interface ITransientService<T> : IServiceLifetime where T : class { }
}