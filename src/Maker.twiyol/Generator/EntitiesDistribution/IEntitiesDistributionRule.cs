﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Twiyol.Generator.EntitiesDistribution
{
    public interface IEntitiesDistributionRule
    {

        float GetValue(int x, int y);

    }
}
