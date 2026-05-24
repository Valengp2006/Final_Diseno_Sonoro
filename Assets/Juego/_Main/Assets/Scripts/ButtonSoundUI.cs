using UnityEngine;
using MoreMountains.Tools;

public class ButtonSoundUI : MonoBehaviour
{
    public AudioClip SoundClip;
    [Range(0f, 1f)]
    public float Volume = 1f;

    public void PlaySound()
    {
        if (SoundClip == null) return;
        
        MMSoundManagerSoundPlayEvent.Trigger(
            SoundClip,
            MMSoundManager.MMSoundManagerTracks.UI,
            Vector3.zero,
            false,
            Volume
        );
    }
}