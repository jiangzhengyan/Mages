﻿namespace Mages.Core.Vm.Operations
{
    using Mages.Core.Runtime.Functions;
    using System;

    /// <summary>
    /// Takes three objects from the stack and returns one.
    /// </summary>
    sealed class SetcOperation : IOperation
    {
        private readonly Object[] _arguments;

        public SetcOperation(Int32 length)
        {
            _arguments = new Object[length];
        }

        public void Invoke(IExecutionContext context)
        {
            var value = context.Pop();
            var obj = context.Pop();
            var function = default(Procedure);

            for (var i = 0; i < _arguments.Length; i++)
            {
                _arguments[i] = context.Pop();
            }

            if (obj != null && TypeFunctions.TryFindSetter(obj, out function))
            {
                function.Invoke(_arguments, value);
            }

            context.Push(value);
        }
    }
}
