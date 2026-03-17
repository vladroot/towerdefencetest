using System;
using UnityEngine;

namespace Game.Exit
{
    public class ExitView : MonoBehaviour
    {
        public event Action<GameObject> OnTrigger;

        private void OnTriggerEnter(Collider collider)
        {
            OnTrigger?.Invoke(collider.gameObject);
        }

        private void OnDestroy()
        {
            OnTrigger = null;
        }
    }
}