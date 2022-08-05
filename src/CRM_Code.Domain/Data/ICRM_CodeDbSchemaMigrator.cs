using System.Threading.Tasks;

namespace CRM_Code.Data;

public interface ICRM_CodeDbSchemaMigrator
{
    Task MigrateAsync();
}
