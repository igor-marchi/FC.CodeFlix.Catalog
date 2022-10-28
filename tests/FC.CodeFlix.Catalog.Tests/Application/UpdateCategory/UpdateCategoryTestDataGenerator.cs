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

    public static IEnumerable<Object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new UpdateCategoryTestFixture();

        var invalidInputLists = new List<object[]>();
        var totalInvalidCases = 3;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputLists.Add(new object[]
                    {
                        fixture.GetInvalidInputCategoryShortName(),
                        "Name should be at least 3 characters long"
                    });
                    break;

                case 1:
                    invalidInputLists.Add(new object[]
                    {
                        fixture.GetInvalidInputCategoryTooLongName(),
                        "Name should be less or equal 255 characters long"
                    });
                    break;
                case 2:
                    invalidInputLists.Add(new object[]
                    {
                        fixture.GetInvalidInputCategoryTooLongDescription(),
                        "Description should be less or equal 10000 characters long"
                    });
                    break;

                default:
                    break;
            }
        }

        return invalidInputLists;
    }
}
