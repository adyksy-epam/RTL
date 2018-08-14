using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using NUnit.Framework;
using RTL.TvMazeScraper.Data.Entities;
using RTL.TvMazeScraper.Scraper.App.Formatters;
using RTL.TvMazeScraper.Scraper.App.Http;
using RTL.TvMazeScraper.Scraper.App.Providers;
using RTL.TvMazeScraper.Scraper.App.Settings;

namespace RTL.TvMazeScraper.Scraper.App.Tests.Unit.Providers
{
    [TestFixture]
    public class TvMazeApiProviderTests
    {
        private ApiSettings _settings;
        private Mock<IHttpClient> _clientMock;
        private Mock<IContentFormatter> _formatterMock;
        private Fixture _fixture;
        private TvMazeApiProvider _provider;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _clientMock = new Mock<IHttpClient>();
            _formatterMock = new Mock<IContentFormatter>();
            _settings = _fixture.Create<ApiSettings>();

            _provider = new TvMazeApiProvider(_settings, 
                _clientMock.Object, 
                _formatterMock.Object);
        }

        [Test]
        public async Task Should_FormatShowContent()
        {
            var args = _fixture.Create<int>();
            var content = _fixture.Create<string>();
            _clientMock.Setup(c => c.GetAsync(It.IsAny<string>())).ReturnsAsync(content);

            await _provider.GetShowsPerPageAsync(args);

            _formatterMock.Verify(f => f.FormatShowContent(content), Times.Once);
        }

        [Test]
        public async Task Should_ReturnFormattedShowContent()
        {
            var args = _fixture.Create<int>();
            var expectedResult = CreateShow(3);
            _formatterMock.Setup(f => f.FormatShowContent(It.IsAny<string>())).Returns(expectedResult);

            var actualResult = await _provider.GetShowsPerPageAsync(args);

            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
        }

        [Test]
        public async Task Should_FormatCharacterContent()
        {
            var args = _fixture.Create<int>();
            var content = _fixture.Create<string>();
            _clientMock.Setup(c => c.GetAsync(It.IsAny<string>())).ReturnsAsync(content);

            await _provider.GetCastPerShowIdAsync(args);

            _formatterMock.Verify(f => f.FormatCharacterContent(content), Times.Once);
        }

        [Test]
        public async Task Should_ReturnFormattedCharacterContent()
        {
            var args = _fixture.Create<int>();
            var expectedResult = CreateCharacter(3);
            _formatterMock.Setup(f => f.FormatCharacterContent(It.IsAny<string>())).Returns(expectedResult);

            var actualResult = await _provider.GetCastPerShowIdAsync(args);

            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
        }

        [Test]
        public async Task Should_ReturnDeduplicatedCharacterContent()
        {
            var args = _fixture.Create<int>();
            long duplicateExtId = _fixture.Create<long>();
            var expectedResult = CreateCharacter(3).ToList();
            expectedResult.ForEach(c => c.ExtId = duplicateExtId);
            _formatterMock.Setup(f => f.FormatCharacterContent(It.IsAny<string>())).Returns(expectedResult);

            var actualResult = await _provider.GetCastPerShowIdAsync(args);

            Assert.That(actualResult.Count(), Is.EqualTo(1));
        }

        private IEnumerable<Show> CreateShow(int count)
        {
            return _fixture.Build<Show>()
                .Without(s => s.Cast)
                .CreateMany(count);
        }

        private IEnumerable<Character> CreateCharacter(int count)
        {
            return _fixture.Build<Character>()
                .Without(c => c.Shows)
                .CreateMany(count);
        }
    }
}
