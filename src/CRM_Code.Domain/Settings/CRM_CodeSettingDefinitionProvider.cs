using Volo.Abp.Settings;

namespace CRM_Code.Settings;

public class CRM_CodeSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CRM_CodeSettings.MySetting1));
    }
}
