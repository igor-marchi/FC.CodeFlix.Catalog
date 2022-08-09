namespace FC.CodeFlix.Catalog.Tests.Application.CreateCategory;

public class CreateCategoryTestDataGenerator
{
    public static IEnumerable<Object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new CreateCategoryTestFixture();

        var invalidInputLists = new List<object[]>();
        var totalInvalidCases = 4;

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
                        fixture.GetInvalidInputCategoryNullDescription(),
                        "Description should not be null"
                    });
                    break;

                case 3:
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
