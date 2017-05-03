using System;

namespace PhotoGallery.Web.UI.Models
{
    public class Photo : IEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public DateTime DateUploaded { get; set; }
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
