using Sound;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu {
    public class MenuButton : MonoBehaviour, ISelectHandler
    {
        public void OnSelect(BaseEventData eventData) {
            SoundEffects.PlaySfx(SoundEffects.SoundEffectID.BulletShoot);
        }
    }
}
