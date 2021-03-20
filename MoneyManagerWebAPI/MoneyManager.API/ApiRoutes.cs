using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Api
{
    /// <summary>
    /// Represents a helper for simple management of API controller's endpoints.
    /// </summary>
    public static class ApiRoutes
    {
        private static readonly string _baseUrl = "https://localhost:44302/api/v1/";

        public static class Categories
        {
            private static readonly string _categoriesControllerUrl = string.Concat(_baseUrl, "Categories");

            public static readonly string GetAsync = _categoriesControllerUrl;
            public static readonly string GetByIdAsync = string.Concat(_categoriesControllerUrl, "/{id:int}");
            public static readonly string PostAsync = _categoriesControllerUrl;
            public static readonly string PatchAsync = string.Concat(_categoriesControllerUrl, "/{id:int}");
            public static readonly string DeleteAsync = string.Concat(_categoriesControllerUrl, "/{id:int}");
        }

        public static class Transactions
        {
            private static readonly string _transactionsControllerUrl = string.Concat(_baseUrl, "Transactions");

            public static readonly string GetAsync = _transactionsControllerUrl;
            public static readonly string GetByIdAsync = string.Concat(_transactionsControllerUrl, "/{id:int}");
            public static readonly string PostAsync = _transactionsControllerUrl;
            public static readonly string PatchAsync = string.Concat(_transactionsControllerUrl, "/{id:int}");
            public static readonly string DeleteAsync = string.Concat(_transactionsControllerUrl, "/{id:int}");
        }

        public static class Reports
        {
            private static readonly string _reportsControllerUrl = string.Concat(_baseUrl, "Reports");

            public static readonly string GetTotalByDateAsync = string.Concat(_reportsControllerUrl, "/GetTotalByDate");
            public static readonly string GetTotalByPeriodAsync = string.Concat(_reportsControllerUrl, "/GetTotalByPeriod");
        }

        public static class Users
        {
            private static readonly string _usersControllerUrl = string.Concat(_baseUrl, "Users");

            public static readonly string AuthenticateAsync = string.Concat(_usersControllerUrl, "/Authenticate");
            public static readonly string RegisterAsync = string.Concat(_usersControllerUrl, "/Register");
            public static readonly string DeleteAsync = string.Concat(_usersControllerUrl, "/{id:int}");
        }
    }
}
