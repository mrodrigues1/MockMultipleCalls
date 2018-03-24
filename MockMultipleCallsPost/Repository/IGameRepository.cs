using MockMultipleCallsPost.Model;

namespace MockMultipleCallsPost.Repository
{
    public interface IGameRepository
    {
        Game FindGameById(int id);
    }
}