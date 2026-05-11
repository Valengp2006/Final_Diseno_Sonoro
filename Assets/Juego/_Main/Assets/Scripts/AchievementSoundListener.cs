using UnityEngine;
using MoreMountains.Tools;

/// Escucha el evento global de logros del Corgi Engine y reproduce un sonido en la pista de UI.

public class AchievementSoundListener : MonoBehaviour, MMEventListener<MMAchievementUnlockedEvent>
{
    [Header("Configuración de Sonido")]
    [Tooltip("El clip de audio que sonará al desbloquear cualquier logro.")]
    public AudioClip AchievementUnlockedSound;

    [Tooltip("Volumen del efecto de sonido.")]
    [Range(0f, 1f)]
    public float SoundVolume = 1f;

    private void OnEnable()
    {
        // Nos suscribimos al evento global cuando el objeto se activa
        this.MMEventStartListening<MMAchievementUnlockedEvent>();
    }

    private void OnDisable()
    {
        // Nos desuscribimos para evitar fugas de memoria (Memory Leaks)
        this.MMEventStopListening<MMAchievementUnlockedEvent>();
    }

    // Interfaz obligatoria de MMEventListener. Se llama sola cuando se gana un logro.
    public virtual void OnMMEvent(MMAchievementUnlockedEvent achievementEvent)
    {
        if (AchievementUnlockedSound != null)
        {
            // Reproducimos el clip usando el MMSoundManager nativo en la pista de UI
            MMSoundManagerSoundPlayEvent.Trigger(
                AchievementUnlockedSound, 
                MMSoundManager.MMSoundManagerTracks.UI, 
                Vector3.zero,
                false, // Pitch aleatorio apagado para mantener el tono original
                SoundVolume
            );
        }
    }
}