using System;

namespace Game.Replay
{
    public interface IReplayer
    {
        public event Action<EReplayStatus> OnStatusChange;
        public event Action<float> OnReplayProgressChange;

        void AddObject(IReplayable obj);
    }
}