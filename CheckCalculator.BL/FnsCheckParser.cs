using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckCalculator.BL
{
    public class FnsCheckParser
    {
        public Check GetCheck(string checkInfo)
        {
            var jCheckInfo = JObject.Parse(checkInfo);
            return new Check
            {

            };
        }
    }
}
