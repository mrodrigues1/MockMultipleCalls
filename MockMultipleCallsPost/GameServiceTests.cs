using FakeItEasy;
using MockMultipleCallsPost.Model;
using MockMultipleCallsPost.Repository;
using MockMultipleCallsPost.Service;
using Moq;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MockMultipleCallsPost
{
    public class GameServiceTests
    {
        [Fact]
        public void GetGameNames_ShouldReturnAllGamesNamesSeparatedByComma_FakeItEasy_1()
        {
            //Arrange
            var games = new Game[] {
                new Game { Id = 1, Name = "Nier: Automata" },
                new Game { Id = 2, Name = "Rocket League" }
            };

            var gameRepositoryMock = A.Fake<IGameRepository>();

            A.CallTo(() => gameRepositoryMock.FindGameById(A<int>.Ignored))
                .ReturnsNextFromSequence(games);

            var sut = new GameService(gameRepositoryMock);

            //Act
            var result = sut.GetGameNames(games.Select(g => g.Id).ToArray());

            //Assert
            Assert.Equal("Nier: Automata, Rocket League", result);
        }

        [Fact]
        public void GetGameNames_ShouldReturnAllGamesNamesSeparatedByComma_FakeItEasy_2()
        {
            //Arrange 
            var games = new Dictionary<int, Game>
            {
                { 1, new Game { Id = 1, Name = "Nier: Automata" } },
                { 2, new Game { Id = 2, Name = "Rocket League" } }
            };

            var gameRepositoryMock = A.Fake<IGameRepository>();

            A.CallTo(() => gameRepositoryMock.FindGameById(A<int>.Ignored))
                .ReturnsLazily<Game, int>(id => games[id]);

            var sut = new GameService(gameRepositoryMock);

            //Act
            var result = sut.GetGameNames(games.Keys.ToArray());

            //Assert
            Assert.Equal("Nier: Automata, Rocket League", result);
        }

        [Fact]
        public void GetGameNames_ShouldReturnAllGamesNamesSeparatedByComma_Moq_1()
        {
            //Arrange             
            var games = new Game[] {
                new Game { Id = 1, Name = "Nier: Automata" },
                new Game { Id = 2, Name = "Rocket League" }
            };

            var gameRepositoryMock = new Mock<IGameRepository>();

            gameRepositoryMock.SetupSequence(_ => _.FindGameById(It.IsAny<int>()))
                .Returns(games[0])
                .Returns(games[1]);

            var sut = new GameService(gameRepositoryMock.Object);

            //Act
            var result = sut.GetGameNames(games.Select(g => g.Id).ToArray());

            //Assert
            Assert.Equal("Nier: Automata, Rocket League", result);
        }

        [Fact]
        public void GetGameNames_ShouldReturnAllGamesNamesSeparatedByComma_Moq_2()
        {
            //Arrange             
            var games = new Queue<Game>(new Game[] {
                new Game { Id = 1, Name = "Nier: Automata" },
                new Game { Id = 2, Name = "Rocket League" }
            });

            var gameRepositoryMock = new Mock<IGameRepository>();

            gameRepositoryMock.Setup(_ => _.FindGameById(It.IsAny<int>()))
                .Returns(() => games.Dequeue());

            var sut = new GameService(gameRepositoryMock.Object);

            //Act
            var result = sut.GetGameNames(games.Select(g => g.Id).ToArray());

            //Assert
            Assert.Equal("Nier: Automata, Rocket League", result);
        }

        [Fact]
        public void GetGameNames_ShouldReturnAllGamesNamesSeparatedByComma_NSubstitute_1()
        {
            //Arrange             
            var games = new Game[] {
                new Game { Id = 1, Name = "Nier: Automata" },
                new Game { Id = 2, Name = "Rocket League" }
            };

            var gameRepositoryMock = Substitute.For<IGameRepository>();

            gameRepositoryMock.FindGameById(Arg.Any<int>())
                .ReturnsForAnyArgs(games[0], games[1]);

            var sut = new GameService(gameRepositoryMock);

            //Act
            var result = sut.GetGameNames(games.Select(g => g.Id).ToArray());

            //Assert
            Assert.Equal("Nier: Automata, Rocket League", result);
        }
    }
}
