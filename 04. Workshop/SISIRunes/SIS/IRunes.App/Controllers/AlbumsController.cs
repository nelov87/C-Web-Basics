using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.App.Controllers
{
    public class AlbumsController : HomeController
    {
        public IHttpResponse All(IHttpRequest httpRequest)
        {
            if (!this.IsLogedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                ICollection<Album> AllAlbums = context.Albums.ToList();

                if (AllAlbums.Count() == 0)
                {
                    this.ViewData["Albums"] = "There are currently no albums.";
                }
                else
                {
                    this.ViewData["Albums"] = string.Join("<br />", context.Albums.Select(album => album.ToHtmlAll()).ToList());
                }

                

                return this.View();
            }
        }

        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!this.IsLogedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!this.IsLogedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                Album album = new Album()
                {
                    Name = httpRequest.FormData["name"].ToString(),
                    Cover = httpRequest.FormData["cover"].ToString()
                };

                if (album == null)
                {
                    return this.Redirect("/Albums/Create");
                }

                context.Albums.Add(album);
                context.SaveChanges();
            }

                return this.Redirect("/Albums/All");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!this.IsLogedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumToReturn = "";

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums.FirstOrDefault(album => album.Id == httpRequest.QueryData["albumId"].ToString());

                if (albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                albumToReturn = albumFromDb.ToHtmlDetails();
            }

            this.ViewData["AlbumDetails"] = albumToReturn;

            return this.View();

        }

        //public IHttpResponse Delete(IHttpRequest httpRequest)
        //{
        //    return this.View();

        //}

    }
}
