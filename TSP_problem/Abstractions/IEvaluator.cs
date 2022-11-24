﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Problem.Abstractions
{
    public interface IEvaluator<TInput, TOutput>
    {
        public TOutput Evaluate(TInput input);
    }
}
