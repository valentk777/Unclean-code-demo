using ChristmasTreeDeliveryApp3;

namespace ChristmasTreeDeliveryAppTests
{
    public class DatabaseTests
    {
        [Fact]
        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeRedcedarTree_ThenEmptyReturn()
        {
            // Arrange
            var db = new Database();
            File.Delete("treeRecord.txt");
            // Assert

            // Act
            var result = db.AllTrees(PresentsType.RedcedarTree);

            Assert.True(result.Count == 0);

        }

        [Fact]
        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeCedarTree_ThenEmptyReturn()
        {
            // Arrange
            var db = new Database();
            File.Delete("treeRecord.txt");

            // Act
            var result = db.AllTrees(PresentsType.CedarTree);

            // Assert
            Assert.True(result.Count == 0);

        }
        [Fact]
        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeConiferTree_ThenEmptyReturn()
        {
            // Arrange
            var db = new Database();
            File.Delete("treeRecord.txt");

            var result = db.AllTrees(PresentsType.ConiferTree);
            Assert.True(result.Count == 0);

        }

        [Fact]
        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeCypressTree_ThenEmptyReturn()
        {
            // Arrange
            var db = new Database();
            File.Delete("treeRecord.txt");

            // Act
            var result = db.AllTrees(PresentsType.CypressTree);

            // Assert
            Assert.True(result.Count == 0);

        }

        [Fact]
        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeFirTree_ThenEmptyReturn()
        {
            // Arrange
            // Act
            // Assert

            var db = new Database();
            File.Delete("treeRecord.txt");

            var result = db.AllTrees(PresentsType.FirTree);

            Assert.True(result.Count == 0);

        }

        [Fact]
        public void GivenEmptyDatabase_WhenSaveTree_ThenOneSaved()
        {
            // Arrange
            var db = new Database();
            // if you remove this test will fail second time
            // Act

            File.Delete("treeRecord.txt");

            var result = db.SaveTree("test1", PresentsType.FirTree, "my");

            // Assert
            Assert.True(result.Item1);
            Assert.NotNull(result.Item2);
            Assert.Equal("test1", result.Item2.TreeName);
            Assert.Equal(result.Item2.TreeDeliveredTo, "my");
            Assert.Equal(result.Item2.TreeType, PresentsType.FirTree);
        }
    }
}