using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksLT2019.MauiXApp.Common.Services
{
    public static class AppShellRoutes
    {
        // 1. Global Routings
        public const string AppLoadingPage = "AppLoadingPage";
        public const string FirstTimeUserPage = "FirstTimeUserPage";
        public const string LogInPage = "LogInPage";
        public const string MainPage = "MainPage";
        public const string RegisterUserPage = "RegisterUserPage";
        public const string SettingsPage = "SettingsPage";

        // 2. Table Routes
        // 2.1. Customer
        public const string CustomerListPage = "CustomerListPage";
        public const string CustomerCreatePage = "CustomerCreatePage";
        public const string CustomerEditPage = "CustomerEditPage";
        public const string CustomerDeletePage = "CustomerDeletePage";
        public const string CustomerDetailsPage = "CustomerDetailsPage";
    }
}
