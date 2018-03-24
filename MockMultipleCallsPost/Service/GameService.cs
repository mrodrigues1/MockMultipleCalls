using MockMultipleCallsPost.Repository;
using System.Linq;

namespace MockMultipleCallsPost.Service
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public string GetGameNames(int[] ids)
        {
            var games = ids.Select(id => _gameRepository.FindGameById(id));
            return string.Join(", ", games.Select(g => g.Name));
        }
    }
}
