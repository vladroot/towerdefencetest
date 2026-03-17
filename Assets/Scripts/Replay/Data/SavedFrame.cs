using System.Collections.Generic;

namespace Game.Replay
{
    public class SavedFrame
    {
        public List<SavedObject> ActiveObjects => _activeObjects;

        private readonly List<SavedObject> _activeObjects;

        public SavedFrame()
        {
            _activeObjects = new List<SavedObject>();
        }
    }
}