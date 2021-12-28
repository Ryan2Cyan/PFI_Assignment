using System;
using UnityEngine;

namespace Sound {
    public class SoundEffects : MonoBehaviour
    {
        // SFX:
        private static FMOD.Studio.EventInstance ReloadBulletSfx => 
            FMODUnity.RuntimeManager.CreateInstance("event:/Bullet_Reload");
        private static FMOD.Studio.EventInstance ReloadPlasmaSfx => 
            FMODUnity.RuntimeManager.CreateInstance("event:/Plasma_Reload");

        public static void PlaySfx(SoundEffectID soundEffectID) {
            switch (soundEffectID) {
                case SoundEffectID.BulletReload:
                    ReloadBulletSfx.start();
                    break;
                case SoundEffectID.PlasmaReload:
                    ReloadPlasmaSfx.start();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(soundEffectID), soundEffectID, null);
            }
        }

        public enum SoundEffectID {
            BulletReload, PlasmaReload
        }
    }
}
