using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumLibrary
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> GetAlbum(int albumId);
    }
}
