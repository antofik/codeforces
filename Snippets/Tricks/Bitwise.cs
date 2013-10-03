using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snippets.Tricks
{
    /*
        x & (x-1)
            = x with the lowest set bit cleared.
        x & ~(x-1)
            = extracts the lowest set bit of x (all others are clear). Pretty patterns when applied to a linear sequence.
        x & (x+(1<<n))
            = x, with the run of set bits (possibly length 0) starting at bit n cleared.
        x & ~(x+(1<<n))
            = the run of set bits (possibly length 0) in x, starting at bit n.
        x | (x+1)
            = x with the lowest cleared bit set.
        x | ~(x+1)
            = extracts the lowest cleared bit of x (all others are set).
        x | (x-(1<<n))
            = x, with the run of cleared bits (possibly length 0) starting at bit n set.
        x | ~(x-(1<<n))
            = the lowest run of cleared bits (possibly length 0) in x, starting at bit n are the only clear bits.
      
      
        Gray code

        x ^ (x >> 1)
        = The standard (binary-reflected) Gray code for x.
        z = y = x; do { y = y >> 1; z ^= y } while (y); return z;
        = decoder for the above Gray code.     
     */
    public class Bitwise
    {

    }
}
