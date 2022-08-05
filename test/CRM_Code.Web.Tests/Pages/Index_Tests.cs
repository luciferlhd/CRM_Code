using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace CRM_Code.Pages;

public class Index_Tests : CRM_CodeWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
