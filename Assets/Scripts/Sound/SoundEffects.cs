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
        private static FMOD.Studio.EventInstance Ability1StartSfx => 
            FMODUnity.RuntimeManager.CreateInstance("event:/Ability_1_Start");
        private static FMOD.Studio.EventInstance Ability1EndSfx => 
            FMODUnity.RuntimeManager.CreateInstance("event:/Ability_1_End");
        private static FMOD.Studio.EventInstance LargeAsteroidExplosionSfx => 
            FMODUnity.RuntimeManager.CreateInstance("event:/Large_Asteroid_Explosion");
        private static FMOD.Studio.EventInstance AsteroidExplosionSfx => 
            FMODUnity.RuntimeManager.CreateInstance("event:/Asteroid_Explosion_2");

        public static void PlaySfx(SoundEffectID soundEffectID) {
            switch (soundEffectID) {
                case SoundEffectID.BulletReload:
                    ReloadBulletSfx.start();
                    break;
                case SoundEffectID.PlasmaReload:
                    ReloadPlasmaSfx.start();
                    break;
                case SoundEffectID.Ability1Start:
                    Ability1StartSfx.start();
                    break;
                case SoundEffectID.Ability1End:
                    Ability1EndSfx.start();
                    break;
                case SoundEffectID.LargeAsteroidExplosion:
                    LargeAsteroidExplosionSfx.start();
                    break;
                case SoundEffectID.AsteroidExplosion:
                    AsteroidExplosionSfx.start();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(soundEffectID), soundEffectID, null);
            }
        }

        public enum SoundEffectID {
            BulletReload, PlasmaReload, Ability1Start, Ability1End, LargeAsteroidExplosion, AsteroidExplosion
        }
    }
}
