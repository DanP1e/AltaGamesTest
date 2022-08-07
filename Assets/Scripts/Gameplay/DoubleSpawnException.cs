using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaGamesTest.Gameplay
{
    public class DoubleSpawnException : Exception
    {
        public DoubleSpawnException()
            : base("You cannot spawn a new object" +
                    " while the previous one exists!")
        {
        }
    }
}
