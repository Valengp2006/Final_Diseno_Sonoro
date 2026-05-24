using UnityEngine;
using MoreMountains.Tools;

public class AchievementPersistence : MonoBehaviour
{
    public static AchievementPersistence Instance { get; private set; }

    [Header("Sonido de Logro")]
    public AudioClip AchievementUnlockedSound;
    [Range(0f, 1f)]
    public float SoundVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivarLogro(string id)
    {
        if (IsLogroActivado(id)) return;

        PlayerPrefs.SetInt(id, 1);
        PlayerPrefs.Save();

        Debug.Log($"[LOGROS] ¡Desbloqueado!: {id}");

        if (AchievementUnlockedSound != null)
        {
            MMSoundManagerSoundPlayEvent.Trigger(
                AchievementUnlockedSound,
                MMSoundManager.MMSoundManagerTracks.UI,
                Vector3.zero,
                false,
                SoundVolume
            );
        }
    }

    public bool IsLogroActivado(string id)
    {
        return PlayerPrefs.GetInt(id, 0) == 1;
    }

    public void RegistrarProgreso(string keyGuardado, int cantidad, int objetivo, string idLogro)
    {
        int progresoActual = PlayerPrefs.GetInt(keyGuardado, 0) + cantidad;
        PlayerPrefs.SetInt(keyGuardado, progresoActual);

        if (progresoActual >= objetivo)
        {
            ActivarLogro(idLogro);
        }
    }

    public void ResetearLogros()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Todos los estados de logros han sido reseteados a 0.");
    }
}