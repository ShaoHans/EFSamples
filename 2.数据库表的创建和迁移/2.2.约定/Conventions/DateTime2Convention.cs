﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._2.约定.Conventions
{
    public class DateTime2Convention: Convention
    {
        public DateTime2Convention()
        {
            Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));
        }
    }
}
