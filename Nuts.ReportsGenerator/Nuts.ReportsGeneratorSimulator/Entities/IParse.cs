using System;
using System.Collections.Generic;
using System.Text;

namespace Nuts.ReportsGenerator.Simulator.Entities
{
   public interface IParse
   {
       byte[] SerializeToByteArray();
   }
}
