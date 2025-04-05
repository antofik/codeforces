using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAddin.TaskA
{
    internal class Test
    {
#if TASKA
        public bool taskA;
#endif
#if TASK_B
        public bool taskB;
#endif
#if TASK_C1
        public bool taskC1;
#endif
#if TASKC2
        public bool taskC2;
#endif
    }
}
