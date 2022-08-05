using CRM_Code.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace CRM_Code.Permissions;

public class CRM_CodePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CRM_CodePermissions.GroupName);

        myGroup.AddPermission(CRM_CodePermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(CRM_CodePermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(CRM_CodePermissions.MyPermission1, L("Permission:MyPermission1"));

        var authorPermission = myGroup.AddPermission(CRM_CodePermissions.Authors.Default, L("Permission:Authors"));
        authorPermission.AddChild(CRM_CodePermissions.Authors.Create, L("Permission:Create"));
        authorPermission.AddChild(CRM_CodePermissions.Authors.Edit, L("Permission:Edit"));
        authorPermission.AddChild(CRM_CodePermissions.Authors.Delete, L("Permission:Delete"));

        var bookPermission = myGroup.AddPermission(CRM_CodePermissions.Books.Default, L("Permission:Books"));
        bookPermission.AddChild(CRM_CodePermissions.Books.Create, L("Permission:Create"));
        bookPermission.AddChild(CRM_CodePermissions.Books.Edit, L("Permission:Edit"));
        bookPermission.AddChild(CRM_CodePermissions.Books.Delete, L("Permission:Delete"));

        var readerPermission = myGroup.AddPermission(CRM_CodePermissions.Readers.Default, L("Permission:Readers"));
        readerPermission.AddChild(CRM_CodePermissions.Readers.Create, L("Permission:Create"));
        readerPermission.AddChild(CRM_CodePermissions.Readers.Edit, L("Permission:Edit"));
        readerPermission.AddChild(CRM_CodePermissions.Readers.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CRM_CodeResource>(name);
    }
}