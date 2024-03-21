namespace MusicHub
{
    using System;
    using System.Net.Http.Headers;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context = new MusicHubDbContext();

            //DbInitializer.ResetDatabase(context);

            Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var producersAlbums = context.Producers
                .Include(p => p.Albums)
                    .ThenInclude(a => a.Songs)
                        .ThenInclude(s => s.Writer)
                .FirstOrDefault(p => p.Id == producerId)!
                .Albums
                .Select(a => new
                {
                    a.Name,
                    a.ReleaseDate,
                    ProducerName = a.Producer.Name,
                    AlbumSongs = a.Songs
                    .Select(s => new
                    {
                        SongName = s.Name,
                        SongPrice = s.Price,
                        SongWriter = s.Writer.Name
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.SongWriter),
                    AlbumTotalPrice = a.Price
                })
                .OrderByDescending(a => a.AlbumTotalPrice)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var album in producersAlbums)
            {
                sb.AppendLine($"-AlbumName: {album.Name}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine("-Songs:");

                int counter = 1;

                foreach (var song in album.AlbumSongs)
                {
                    sb.AppendLine($"---#{counter++}");
                    sb.AppendLine($"---SongName: {song.SongName}");
                    sb.AppendLine($"---Price: {song.SongPrice:F2}");
                    sb.AppendLine($"---Writer: {song.SongWriter}");
                }

                sb.AppendLine($"-AlbumPrice: {album.AlbumTotalPrice:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var span = new TimeSpan(0, 0, duration);

            var songs = context.Songs
                .Where(s => s.Duration > span)
                .Select(s => new
                {
                    SongName = s.Name,
                    SongPerformers = s.SongPerformers
                        .Select(p => p.Performer.FirstName + " " + p.Performer.LastName)
                        .OrderBy(name => name)
                        .ToList(),
                    SongWriter = s.Writer.Name,
                    SongProducer = s.Album.Producer.Name,
                    SongDuration = s.Duration.ToString("c")
                })
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.SongWriter)
                .ToList();

            StringBuilder sb = new StringBuilder();

            int counter = 1;

            foreach (var song in songs)
            {
                sb.AppendLine($"-Song #{counter++}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.SongWriter}");

                if (song.SongPerformers.Any())
                {
                    sb.AppendLine(string.Join(Environment.NewLine, song.SongPerformers
                            .Select(p => $"---Performer: {p}")));
                }

                sb.AppendLine($"---AlbumProducer: {song.SongProducer}");
                sb.AppendLine($"---Duration: {song.SongDuration}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
