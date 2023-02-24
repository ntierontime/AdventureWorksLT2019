using CSharpVitamins;

namespace AdventureWorksLT2019.EFCoreRepositories
{
    public static class UtilityHelper
    {
        public static string GetShortGuid()
        {
            return ShortGuid.NewGuid().Value;
        }
    }
}

