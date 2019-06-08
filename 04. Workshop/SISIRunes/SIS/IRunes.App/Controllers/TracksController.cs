using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.App.Controllers
{
    public class TracksController : BaseController
    {
        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!this.IsLogedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }
            string albumId = httpRequest.QueryData["albumId"].ToString();

            this.ViewData["AlbumId"] = albumId;

            return this.View();
        }

        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!this.IsLogedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }
            string albumId = httpRequest.QueryData["albumId"].ToString();

            using (var context = new RunesDbContext())
            {
                Track track = new Track()
                {
                    Name = httpRequest.FormData["name"].ToString(),
                    Link = httpRequest.FormData["link"].ToString(),
                    Price = decimal.Parse(httpRequest.FormData["price"].ToString())
                    
                };

                if (track == null)
                {
                    return this.Redirect($"/Tracks/Create?trackId{albumId}");
                }

                Album albumFromDb = context.Albums.FirstOrDefault(album => album.Id == albumId);
                albumFromDb.Tracks.Add(track);
                albumFromDb.Price = ((albumFromDb.Tracks.Select(trackToSumm => trackToSumm.Price).Sum()) * 87) / 100;
                //context.Update(albumFromDb);
                context.SaveChanges();
            }

                return this.Redirect($"/Albums/Details?albumId={albumId}");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!this.IsLogedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string trackId = httpRequest.QueryData["trackId"].ToString();
            string albumId = httpRequest.QueryData["albumId"].ToString();

            Track trackToReturn;

            using (var context = new RunesDbContext())
            {
                var tracksFromDb = context.Tracks.SingleOrDefault(track => track.Id == trackId);
                trackToReturn = tracksFromDb;
            }

            if (trackToReturn == null)
            {
                return this.Redirect($"/Albums/Details?albumId={albumId}");
            }

            this.ViewData["Track"] = trackToReturn.ToHtmlDetails();
            this.ViewData["AlbumId"] = albumId;

            return this.View();

        }
    }
}
