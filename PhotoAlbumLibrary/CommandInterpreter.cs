using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumLibrary
{
    public class CommandInterpreter
    {
        private IPhotoRepository _photoRepository;

        public CommandInterpreter()
        {
            _photoRepository = new PhotoRepository();
        }
        public CommandInterpreter(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public string Evaluate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            var terms = input.Split(' ').Select(t => t.ToLower().Trim()).ToList();
            var command = terms.First();
            var parms = terms.Skip(1);

            switch(terms[0])
            {
                case "help":
                    return Constants.HelpText;
                case "photo-album":
                    var retVal = DoPhotoAlbum(parms);
                    return retVal;
                default:
                    return Constants.HelpText;
            }
        }

        private string DoPhotoAlbum(IEnumerable<string> parms)
        {
            if(parms.Count() != 1)
            {
                return Constants.PhotoAlbumUsage;
            }

            if (int.TryParse(parms.First(), out var albumNumber) == false)
            {
                return Constants.NonIntParameter;
            }

            var photos = _photoRepository.GetAlbum(albumNumber);
            if(photos.Any() == false)
            {
                return Constants.AlbumNotFound;
            }

            var photoText = photos.Select(p => $"[{p.Id}] {p.Title}");
            var retVal = string.Join("\n", photoText);

            return retVal;
        }
    }
}
