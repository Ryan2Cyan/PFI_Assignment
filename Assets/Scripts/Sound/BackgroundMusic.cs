using FMOD.Studio;
using UnityEngine;

namespace Sound {
    public class BackgroundMusic : MonoBehaviour {
        
        public static FMOD.Studio.EventInstance BackingTrack;

        private void Start() {
            
            BackingTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Backing_Track");
            BackingTrack.start();
            BackingTrack.release();
        }
    }
}
