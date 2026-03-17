using System;
using UnityEngine;
using UnityEngine.UI;

public class ReplayUIView : MonoBehaviour
{
    public event Action OnRewind;
    public event Action OnPause;
    public event Action OnPlay;
    public event Action<float> OnUIReplayProgressChange;

    [SerializeField] private Button pause;
    [SerializeField] private Button play;
    [SerializeField] private Button rewind;
    [SerializeField] private Slider progressBar;

    public void UpdateProgressBar(float val)
    {
        progressBar.SetValueWithoutNotify(val);
    }

    private void OnEnable()
    {
        pause.onClick.AddListener(PauseClicked);
        play.onClick.AddListener(PlayClicked);
        rewind.onClick.AddListener(RewindClicked);
        progressBar.onValueChanged.AddListener(ProgressChanged);
    }

    private void OnDisable()
    {
        pause.onClick.RemoveListener(PauseClicked);
        play.onClick.RemoveListener(PlayClicked);
        rewind.onClick.RemoveListener(RewindClicked);
        progressBar.onValueChanged.RemoveListener(ProgressChanged);
    }

    private void PlayClicked()
    {
        OnPlay?.Invoke();
    }

    private void PauseClicked()
    {
        OnPause?.Invoke();
    }

    private void RewindClicked()
    {
        OnRewind?.Invoke();
    }

    private void ProgressChanged(float val)
    {
        OnUIReplayProgressChange?.Invoke(val);
    }
}
