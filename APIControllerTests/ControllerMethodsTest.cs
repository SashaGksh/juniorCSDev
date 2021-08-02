using DataLayer.Repositories;
using NUnit.Framework;

namespace APIControllerTests
{
    public class Tests
    {
        [SetUp]
        public void Setup(IAdRepository adRepository, IAdTagRepository adTagRepository, IAdTypeRepository adTypeRepository, ICategoryRepository categoryRepository, IContentRepository contentRepository, ITagRepository tagRepository)
        {
        private readonly IAdRepository adRepository;
        private readonly IAdTagRepository adTagRepository;
        private readonly IAdTypeRepository adTypeRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IContentRepository contentRepository;
        private readonly ITagRepository tagRepository;

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}