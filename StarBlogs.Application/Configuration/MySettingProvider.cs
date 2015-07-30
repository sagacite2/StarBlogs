using System.Collections.Generic;
using Abp.Configuration;

namespace StarBlogs.Configuration
{
    public class MySettingProvider : SettingProvider
    {
        public const string StarsListDefaultPageSize = "StarsListDefaultPageSize";

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       new SettingDefinition(StarsListDefaultPageSize, "30")
                   };
        }
    }
}
