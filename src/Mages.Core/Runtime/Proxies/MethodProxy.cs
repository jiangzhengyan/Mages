﻿namespace Mages.Core.Runtime.Proxies
{
    using System;
    using System.Linq;
    using System.Reflection;

    sealed class MethodProxy : BaseProxy
    {
        private readonly MethodInfo[] _methods;
        private readonly Function _proxy;

        public MethodProxy(WrapperObject obj, MethodInfo[] methods)
            : base(obj)
        {
            _methods = methods;
            _proxy = new Function(Invoke);
        }

        protected override Object GetValue()
        {
            return _proxy;
        }

        protected override void SetValue(Object value)
        {
        }

        private Object Invoke(Object[] arguments)
        {
            var parameters = arguments.Select(m => m.GetType()).ToArray();
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.OptionalParamBinding | BindingFlags.InvokeMethod;
            var method = Type.DefaultBinder.SelectMethod(flags, _methods, parameters, null) ?? _methods.Find(arguments, parameters);
            var result = default(Object);

            if (method != null)
            {
                result = method.Call(_obj, arguments);
            }

            return result;
        }
    }
}