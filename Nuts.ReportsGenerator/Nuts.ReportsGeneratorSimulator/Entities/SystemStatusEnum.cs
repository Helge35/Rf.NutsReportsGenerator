using System;
using System.Collections.Generic;
using System.Text;

namespace Nuts.ReportsGenerator.Entities
{
    [Serializable]
    public enum SystemStatusEnum
    {
        GF = -3,
        HF = -2,
        GHF = -1,
        Running = 0
    }
}
