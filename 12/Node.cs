using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode12
{
    internal class Node
    {
        public int x;
        public int y;
        public int elevation;
        public int distance = 0;

        public Node(int y, int x, int eleveation)
        {
            this.x = x;
            this.y = y;
            this.elevation = eleveation;
        }
    }
}
