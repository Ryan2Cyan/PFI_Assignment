using UnityEngine;

namespace Sound {
    public class BackgroundMusic : MonoBehaviour {
        
        private static FMOD.Studio.EventInstance _backingTrack;

        private void Start() {
            
            _backingTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Backing_Track");
            _backingTrack.start();
            _backingTrack.release();
        }
    }
}
