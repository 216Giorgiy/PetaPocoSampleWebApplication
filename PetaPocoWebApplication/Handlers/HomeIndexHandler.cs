﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using PetaPocoWebApplication.Infrastructure;
using PetaPocoWebApplication.Models;

namespace PetaPocoWebApplication.Handlers
{
    public class HomeIndexHandler : IQueryHandler<HomeIndexViewModel>
    {
        private readonly IDatabaseQuery _databaseQuery;

        public HomeIndexHandler(IDatabaseQuery databaseQuery)
        {
            _databaseQuery = databaseQuery;
        }

        public void Handle(HomeIndexViewModel viewmodel)
        {
            viewmodel.Message = "Welcome to PetaPoco";
            viewmodel.BudgetPeriod = _databaseQuery.First<BudgetPeriod>("");
            viewmodel.Expenses = _databaseQuery.Fetch<Expense>("where BudgetPeriodId = @BudgetPeriodId",
                                                               viewmodel.BudgetPeriod);
        }
    }


    public class HomeIndexViewModel
    {
        public string Message { get; set; }

        public BudgetPeriod BudgetPeriod { get; set; }
        public IList<Expense> Expenses { get; set; }
    }
}