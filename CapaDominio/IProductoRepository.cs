﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public interface IProductoRepository
    {
        Task<int> CountAsync();
    }
}
