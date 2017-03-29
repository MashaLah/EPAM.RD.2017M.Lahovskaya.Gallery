using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gallery.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Url { get; set; }
        public string Name { get; set; }
        /*public string Extension { get; set; }
        public int Star { get; set; } = 0;*/
        public DateTime? Date { get; set; }
        public int? AlbumId { get; set; }
    }
}