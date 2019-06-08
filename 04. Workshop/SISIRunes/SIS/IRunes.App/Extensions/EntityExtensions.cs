using IRunes.Data;
using IRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IRunes.App.Extensions
{
    public static class EntityExtensions
    {
        private static string GetTracks(Album album)
        {
            List<Track> tracks = new List<Track>();

            using (var context = new RunesDbContext())
            {
                var tracksFromDb = context.Tracks.Where(track => track.AlbumId == album.Id).ToList();
                tracks = tracksFromDb;
            }

            if (tracks.Count == 0)
            {
                return "Thear curently no tracks.";
            }
            else
            {
                return string.Join("", tracks.Select((track, index) => track.ToHtmlAll(index + 1)));
            }
        }

        public static string ToHtmlAll(this Album album)
        {
            return $"<div><a href=\"/Albums/Details?albumId={album.Id}\">{WebUtility.UrlDecode(album.Name)}</a></div>";
        }

        public static string ToHtmlAll(this Track track, int index)
        {
            return $"<li><a href=\"/Tracks/Details?albumId={track.AlbumId}&trackId={track.Id}\"><strong>{index}.</strong> {WebUtility.UrlDecode(track.Name)}</a></li>";
            //return $"<li><a href=\"/Tracks/Details?albumId={track.AlbumId}&trackId={track.Id}\"><strong>{index}.</strong> {WebUtility.UrlDecode(track.Name)}</a></li>";
        }

        public static string ToHtmlDetails(this Album album)
        {
            return $"<div class=\"album-diteils d-flex justify-content-between row\">" +
                   $"<div class=\"album-data col-md-5\">" +
                   $"<img src=\"{WebUtility.UrlDecode(album.Cover)}\" alt=\"{WebUtility.UrlDecode(album.Name)}\" class=\"img-thumbnail\" width=\"640\" height=\"480\">" +
                   $"<h3 class=\"text-center\">Album name: {WebUtility.UrlDecode(album.Name)}</h3>" +
                   $"<h3 class=\"text-center\">Album price: {album.Price:F2}</h3>" +
                   $"<div class=\"d-flex justify-content-between\">" +
                   $"<a class=\"btn bg-success text-white\" href=\"/Tracks/Create?albumId={album.Id}\">Create Track</a>" +
                   $"<a class=\"btn bg-success text-white\" href=\"/Albums/All\">Back To All</a>" +
                   $"</div>" +
                   $"</div>" +
                   $"<div class=\"album-tracks col-md-5\">" +
                   $"<h1>Tracks</h1>" +
                   $"<ul class=\"track-list\">" +
                   $"{GetTracks(album)}" +
                   $"</ul>" +
                   $"</ div>" +
                   $"</ div>";
                //$"" +
                //$"" +
                //$"" +
                //$"" +
                //$"<div class=\"album-details\">" +
                //    $"<div class=\"album-data\">" +
                //        $" <img src=\"{WebUtility.UrlDecode(album.Cover)}\" alt=\"{WebUtility.UrlDecode(album.Name)}\" height=\"100\">" +
                //        $"<div><h2>Name: {WebUtility.UrlDecode(album.Name)}</h2></div>" +
                //        $"<div><h2>Price: {album.Price:F2}</h2></div>" +
                //    $"</div>" +
                //    $"<div class=\"album-tracks\">" +
                //        $"<h1>Tracks</h1>" +
                //        $"<hr style=\"heigth: 2px\" />" +
                //        $"<a href=\"/Tracks/Create?albumId={album.Id}\">Create Track</a>" +
                //        $"<hr style=\"heigth: 2px\" />" +
                //        $"<ul class=\"track-list\">" +
                //            $"{GetTracks(album)}" +
                //        $"</ul>" +
                //        $"<hr style=\"heigth: 2px\" />" +
                //    $"</div>" +
                //$"</div>";
        }



        public static string ToHtmlDetails(this Track track)
        {
            return $"<div class=\"d-flex justify-content-center\" id=\"track-details\">" +
                   $"<div class=\"track-data\">" +
                   $"<div class=\"d-flex justify-content-center\"><h2>Name: {track.Name}</h2></div>" +
                   $"<div class=\"d-flex justify-content-center\"><h2>Price: ${track.Price:F2}</h2></div>" +
                   $"<hr class=\"bg-success w-50\" style=\"height: 2px\" />" +
                   $"<div class=\"track-video\">" +
                   $"<iframe id=\"ytplayer\" type=\"text/html\" width=\"640\" height=\"360\" " +
                   $"src=\"https://www.youtube.com/embed/{WebUtility.UrlDecode(track.Link)}?autoplay=1&origin=http://example.com\"frameborder=\"0\"></iframe> " +
                   $"</div>" +
                   $"</div>";
                //$"" +
                //$"" +
                //$"" +
                //$"" +
                //$"<div class=\"track-details\">" +
                //    $"<div class=\"track-data\">" +
                //    $"<div class=\"track-video\">" +
                        
                //    $"</div>" +
                //        $"<div><h2>Name: {track.Name}</h2></div>" +
                //        $"<div><h2>Price: {track.Price:F2}</h2></div>" +
                //    $"</div>" +
                //$"</div>";
        }

    }
}
