//using ChristmasTreeDeliveryApp.Contracts.Enitites;

//namespace ChristmasTreeDeliveryApp.Api.Test
//{
//    public class DatabaseTests
//    {
//        [Fact]
//        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeRedcedarTree_ThenEmptyReturn()
//        {
//            // Arrange
//            var db = new Database();
//            File.Delete("treeRecord.txt");
//            // Assert

//            // Act
//            var result = db.GetAllTrees(TreeType.RedcedarTree);

//            Assert.True(result.Count == 0);

//        }

//        [Fact]
//        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeCedarTree_ThenEmptyReturn()
//        {
//            // Arrange
//            var db = new Database();
//            File.Delete("treeRecord.txt");

//            // Act
//            var result = db.GetAllTrees(TreeType.CedarTree);

//            // Assert
//            Assert.True(result.Count == 0);

//        }
//        [Fact]
//        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeConiferTree_ThenEmptyReturn()
//        {
//            // Arrange
//            var db = new Database();
//            File.Delete("treeRecord.txt");

//            var result = db.GetAllTrees(TreeType.ConiferTree);
//            Assert.True(result.Count == 0);

//        }

//        [Fact]
//        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeCypressTree_ThenEmptyReturn()
//        {
//            // Arrange
//            var db = new Database();
//            File.Delete("treeRecord.txt");

//            // Act
//            var result = db.GetAllTrees(TreeType.CypressTree);

//            // Assert
//            Assert.True(result.Count == 0);

//        }

//        [Fact]
//        public void GivenEmptyDatabase_WhenAllTreesPresentsTypeFirTree_ThenEmptyReturn()
//        {
//            // Arrange
//            // Act
//            // Assert

//            var db = new Database();
//            File.Delete("treeRecord.txt");

//            var result = db.GetAllTrees(TreeType.FirTree);

//            Assert.True(result.Count == 0);

//        }

//        [Fact]
//        public void GivenEmptyDatabase_WhenSaveTree_ThenOneSaved()
//        {
//            // Arrange
//            var db = new Database();
//            // if you remove this test will fail second time
//            // Act

//            File.Delete("treeRecord.txt");
//            var result = db.SaveTree("test1", TreeType.FirTree, "my");

//            // Assert
//            Assert.True(result.IsSaveWasSucessful);
//            Assert.NotNull(result.Data);
//            Assert.Equal("test1", result.Data?.Name);
//            Assert.Equal(result.Data?.DeliveryAddress, "my");
//            Assert.Equal(result.Data?.Type, TreeType.FirTree);
//        }
//    }
//}