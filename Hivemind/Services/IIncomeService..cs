﻿using Hivemind.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Services
{
    interface IIncomeService
    {
        IncomeReport ProcessIncome(BattleReport battleReport);
    }
}
