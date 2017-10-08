﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quaver.Localization;
using Quaver.Utility;

namespace Quaver.Tests
{
    internal static class JsonTest
    {
        internal static void DeserializeJsonTest()
        {
            var english = JsonResourceReader.Read("Quaver.Resources.Quaver_Localization.en.english.json");
            Console.WriteLine(english.play);
        }
    }
}