using System;
using System.Collections.Generic;
using Common.Input;
using Zenject;

namespace Game.Replay
{
    public class ReplayController : IReplayer, IFixedTickable, IDisposable
    {
        public event Action<EReplayStatus> OnStatusChange;
        public event Action<float> OnReplayProgressChange;

        private List<SavedFrame> _frames;
        private List<IReplayable> _watchList;
        private EReplayStatus _replayStatus;

        private int _currentFrame;
        private readonly IPlayerInput _playerInput;
        private readonly ReplayUIView _view;

        public ReplayController(IPlayerInput playerInput, ReplayUIView view)
        {
            _playerInput = playerInput;
            _playerInput.OnSpacePress += SetRewind;
            _playerInput.OnSpaceRelease += SetReplay;
            _view = view;
            _view.OnPlay += SetReplay;
            _view.OnPause += SetPause;
            _view.OnRewind += SetRewind;
            _view.OnUIReplayProgressChange += SetProgressFrame;
            _watchList = new List<IReplayable>();
            _frames = new List<SavedFrame>();
            _replayStatus = EReplayStatus.Record;
            _currentFrame = 0;
        }

        public void Dispose()
        {
            OnStatusChange = null;
            OnReplayProgressChange = null;
            _playerInput.OnSpacePress -= SetRewind;
            _playerInput.OnSpaceRelease -= SetReplay;
            _view.OnPlay -= SetReplay;
            _view.OnPause -= SetPause;
            _view.OnRewind -= SetRewind;
            _view.OnUIReplayProgressChange -= SetProgressFrame;
        }

        public void AddObject(IReplayable obj)
        {
            _watchList.Add(obj);
        }

        public void FixedTick()
        {
            switch (_replayStatus)
            {
                case EReplayStatus.Record:
                    Record();
                    break;
                case EReplayStatus.Rewind:
                    Rewind();
                    break;
                case EReplayStatus.Replay:
                    Replay();
                    break;
                default:
                    break;
            }
        }

        private void Record()
        {
            SavedFrame frame = new SavedFrame();
            for (int i = 0; i < _watchList.Count; i++)
            {
                SavedObject obj = _watchList[i].GetSaveData();
                frame.ActiveObjects.Add(obj);
            }

            _frames.Add(frame);
        }

        private void Rewind()
        {
            PlayFrame();

            _currentFrame--;
        }

        private void Replay()
        {
            PlayFrame();

            _currentFrame++;
        }

        private void SetRewind()
        {
            _replayStatus = EReplayStatus.Rewind;
            OnStatusChange?.Invoke(_replayStatus);
            _currentFrame = _frames.Count - 1;

            for (int i = 0; i < _watchList.Count; i++)
                _watchList[i].SetReplayStatus(EReplayStatus.Rewind);
        }

        private void SetReplay()
        {
            _replayStatus = EReplayStatus.Replay;
            OnStatusChange?.Invoke(_replayStatus);

            for (int i = 0; i < _watchList.Count; i++)
                _watchList[i].SetReplayStatus(EReplayStatus.Replay);
        }

        private void SetPause()
        {
            _replayStatus = EReplayStatus.Pause;
            OnStatusChange?.Invoke(_replayStatus);

            for (int i = 0; i < _watchList.Count; i++)
                _watchList[i].SetReplayStatus(EReplayStatus.Pause);
        }

        private void SetProgressFrame(float progress)
        {
            _currentFrame = (int)(_frames.Count * progress);

            PlayFrame();
        }

        private void PlayFrame()
        {
            if (_currentFrame < 0 || _currentFrame >= _frames.Count)
                return;

            SavedFrame frame = _frames[_currentFrame];

            // this list contains only objects that don't exist in the current frame
            List<IReplayable> leftOvers = new List<IReplayable>(_watchList);
            for (int i = 0; i < frame.ActiveObjects.Count; i++)
            {
                IReplayable obj = frame.ActiveObjects[i].SaveObject;

                leftOvers.Remove(obj);

                obj.SetSaveData(frame.ActiveObjects[i]);
            }

            // now we need to hide all "non-existent" objects
            for (int i = 0; i < leftOvers.Count; i++)
            {
                leftOvers[i].SetSaveData(null);
            }

            _view.UpdateProgressBar((float)_currentFrame / _frames.Count);
        }
    }
}