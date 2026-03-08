using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takeaway_restaurant
{
    public class BSTNode
    {
        public MenuItem Data{get; set; }
        public BSTNode Left {  get; set; }
        public BSTNode Right { get; set; }


        public BSTNode(MenuItem data) 
        {
            Data = data;
            Left = null;
            Right = null;
                }
    }
}
