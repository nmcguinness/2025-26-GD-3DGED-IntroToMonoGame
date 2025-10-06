
using IntroToMonoGame;
using System.Collections.Generic;

namespace GDEngine
{
    public class ContentDictionary<T>
    {
        private Dictionary<string, T> _contents;
        private Main _game;
        public ContentDictionary(Main game)
        {
            _game = game; 
            _contents = new Dictionary<string, T>();
        }
        public void Load(string resourceName, string id)  
        {
            T asset = _game.Content.Load<T>(resourceName);
            _contents.Add(id, asset);
        }
        public T TryGet(string id)
        {
            T asset;
            _contents.TryGetValue(id, out asset);
            return asset;
        }
    }
}
