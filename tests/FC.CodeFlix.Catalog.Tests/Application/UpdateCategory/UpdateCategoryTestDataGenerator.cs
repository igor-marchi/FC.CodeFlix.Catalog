namespace FC.CodeFlix.Catalog.Tests.Application.UpdateCategory;
public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
    {
        var fixture = new UpdateCategoryTestFixture();
        for (int index = 0; index < times; index++)
        {
            var exampleCategory = fixture.GetExampleCategory();
            var exempleInput = fixture.GetValidInput(exampleCategory.Id);

            yield return new object[] {
                exampleCategory,
                exempleInput
            };
        }
    }
}
