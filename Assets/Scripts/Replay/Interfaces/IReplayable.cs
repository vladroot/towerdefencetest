namespace Game.Replay
{
    public interface IReplayable
    {
        void SetReplayStatus(EReplayStatus status);
        SavedObject GetSaveData();
        void SetSaveData(SavedObject data);
    }
}
