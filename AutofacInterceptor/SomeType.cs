﻿using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacInterceptor
{
    public class SomeType : ISomeType
    {
        //di called interface ,the attribute should be at interface
        //[Custom(StartLog = true)]
        public string Show(string input)
        {
            Console.WriteLine($"showdemo");
            return "resultdemo";
        }
    }
}
