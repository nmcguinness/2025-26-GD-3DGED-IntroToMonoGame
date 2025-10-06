
using IntroToMonoGame;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GDEngine
{
    public class ContentDictionary
    {
        private Dictionary<string, Texture2D> _contents;
        private Main _game;
        public ContentDictionary(Main game)
        {
            _game = game; 
            _contents = new Dictionary<string, Texture2D>();
        }
        public void Load(string resourceName, string id)  
        {
            Texture2D asset = _game.Content.Load<Texture2D>(resourceName);
            _contents.Add(id, asset);
        }
        public Texture2D TryGet(string id)
        {
            Texture2D asset;
            _contents.TryGetValue(id, out asset);
            return asset;
        }
    }
}
