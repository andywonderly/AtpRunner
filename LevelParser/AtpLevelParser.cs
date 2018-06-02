using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelParser
{
    public static class AtpLevelParser
    {
        public static TiledData Parse(string fileName)
        {
            string json = File.ReadAllText(fileName);
            var result = JsonConvert.DeserializeObject<TiledData>(json);

            return result;
        }

        public class TiledData
        {
            public int height { get; set; }
            public List<Layer> layers { get; set; }
            public int nextobjectid { get; set; }
            public string orientation { get; set; }
            public string renderorder { get; set; }
            public int tileheight { get; set; }
            public List<Tileset> tilesets { get; set; }
            public int tilewidth { get; set; }
            public int version { get; set; }
            public int width { get; set; }

        }

        public class Tileset
        {
            public int columns { get; set; }
            public int firstgid { get; set; }
            public string image { get; set; }
            public int imageheight { get; set; }
            public int imagewidth { get; set; }
            public int margin { get; set; }
            public string name { get; set; }
            public int spacing { get; set; }
            public int tilecount { get; set; }
            public int tileheight { get; set; }
            public int tilewidth { get; set; }
        }

        public class Layer
        {
            public List<int> data { get; set; }
            public List<TiledObject> objects { get; set; }
            public int height { get; set; }
            public string name { get; set; }
            public int opacity { get; set; }
            public string type { get; set; }
            public bool visible { get; set; }
            public int width { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }

        public class TiledObject
        {
            public int gid { get; set; }
            public int height { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int rotation { get; set; }
            public string Type { get; set; }
            public bool visible { get; set; }
            public int width { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }

    }
}
