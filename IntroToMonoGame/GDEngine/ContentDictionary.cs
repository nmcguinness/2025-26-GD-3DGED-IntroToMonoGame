
using IntroToMonoGame;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GDEngine
{
    public class ContentDictionary
    {
        private Dictionary<string, Texture2D> content;
        private Main _game;

        public ContentDictionary(Main game)
        {
            _game = game;
        }
        public void Load(string resourceName, string id)
        {
            Texture2D asset 
                = _game.Content.Load<Texture2D>(resourceName);
            content.Add(id, asset);
        }
        public void UnloadAll()
        { 
        }
        public Texture2D TryGet(string id)
        {
            return null;
        }
    }
}
