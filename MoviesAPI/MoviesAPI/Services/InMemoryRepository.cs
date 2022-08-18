using MoviesAPI.Entities;

namespace MoviesAPI.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<Genre> _genres;
        private readonly ILogger<InMemoryRepository> logger;

        public InMemoryRepository(ILogger<InMemoryRepository> logger)
        {
            _genres = new List<Genre>()
            {
                new Genre(){Id = 1, Name = "Comdey"},
                new Genre(){Id = 2, Name = "Action"}
            };
            this.logger = logger;
        }
        public async Task<List<Genre>> GetAllGenres() // add async Task<return type>
        {
            logger.LogInformation("Executing GetAllGenres");
            await Task.Delay(1); 
            return _genres;
        }
        public Genre GetGenreById (int id)
        {
            return _genres.FirstOrDefault(x => x.Id == id);
        }

        public void AddGenre (Genre genre)
        {
            genre.Id = _genres.Max(x => x.Id) + 1;
            _genres.Add(genre);
        }
    }
}
