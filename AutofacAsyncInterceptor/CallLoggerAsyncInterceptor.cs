﻿using Castle.DynamicProxy;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//https://github.com/JSkimming/Castle.Core.AsyncInterceptor/issues/42
namespace AutofacAsyncInterceptor
{
    public class CallLoggerAsyncInterceptor : AsyncInterceptorBase  
    {
        TextWriter _output;

        public CallLoggerAsyncInterceptor(TextWriter output)
        {
            _output = output;
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, Func<IInvocation, Task<TResult>> proceed)
        {
           
            _output.WriteLine("Calling method '{0}' with parameters '{1}'... ",
                invocation.Method.Name,
                string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));

            //check enable log
            bool isEnabled = AttributeHelper.IsLoggerEnabled(invocation.Method);

            //before invocation
            if (isEnabled)
            {
                _output.WriteLine("Logger is Enabled");
            }
            //invocation

            var result = await proceed(invocation).ConfigureAwait(false);


            //after invocation
            if (isEnabled)
            {
                _output.WriteLine("Done: result was '{0}'.", result);
            }

            return result;
        }

        protected override Task InterceptAsync(IInvocation invocation, Func<IInvocation, Task> proceed)
        {
            throw new NotImplementedException();
        }
    }
}