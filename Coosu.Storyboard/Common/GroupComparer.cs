﻿using System.Collections.Generic;

namespace Coosu.Storyboard.Common
{
    public class GroupComparer : IComparer<Layer>
    {
        public int Compare(Layer? x, Layer? y)
        {
            if (x == null && y == null)
                return 0;
            if (y == null)
                return 1;
            if (x == null)
                return -1;
            if (x.ZDistance > y.ZDistance)
                return 1;
            if (x.ZDistance < y.ZDistance)
                return -1;
            return 0;
        }
    }
}