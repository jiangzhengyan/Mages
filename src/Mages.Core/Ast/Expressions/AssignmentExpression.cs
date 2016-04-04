﻿namespace Mages.Core.Ast.Expressions
{
    using System;

    /// <summary>
    /// Represents an assignment expression.
    /// </summary>
    sealed class AssignmentExpression : AssignableExpression, IExpression
    {
        #region Fields

        private readonly IExpression _variable;
        private readonly IExpression _value;

        #endregion

        #region ctor

        public AssignmentExpression(IExpression variable, IExpression value)
            : base(variable.Start, value.End)
        {
            _variable = variable;
            _value = value;
        }

        #endregion

        #region Properties

        public IExpression Variable 
        {
            get { return _variable; }
        }

        public String VariableName 
        {
            get 
            { 
                var variable = Variable as VariableExpression;

                if (variable != null)
                {
                    return variable.Name;
                }

                return String.Empty;
            }
        }

        public IExpression Value 
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        public void Validate(IValidationContext context)
        {
            if (Variable.IsAssignable)
            {
                Variable.Validate(context);
            }
            else
            {
                var error = new ParseError(ErrorCode.AssignableExpected, Variable.Start);
                context.Report(error);
            }

            Value.Validate(context);
        }

        #endregion
    }
}