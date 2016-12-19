﻿using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.Game.World
{
    [Serializable]
    public class ObjWorld
    {

        public ObjChunk[,] chunks;
        public Dictionary<int, ObjRegion> regions;

        public ObjWorld()
        {
            regions = new Dictionary<int, ObjRegion>();
        }

    }
}
