using UnityEngine;

namespace Game.Replay
{
    public class SavedObject
    {
        public IReplayable SaveObject => _saveObject;
        public Vector3 Position => _position;
        public Quaternion Rotation => _rotation;
        public bool IsActive => _isActive;

        private readonly IReplayable _saveObject;
        private readonly Vector3 _position;
        private readonly Quaternion _rotation;
        private readonly bool _isActive;

        public SavedObject(IReplayable saveObject, Vector3 position, Quaternion rotation, bool isActive)
        {
            _saveObject = saveObject;
            _position = position;
            _rotation = rotation;
            _isActive = isActive;
        }
    }
}