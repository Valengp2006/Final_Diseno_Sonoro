using UnityEngine;

public class AchievementPersistence : MonoBehaviour
{
    // Singleton para acceder desde cualquier parte (ej: la UI de tu compañero)
    public static AchievementPersistence Instance { get; private set; }

    void Awake()
    {
        // Esto asegura que el Manager no se destruya al cambiar de nivel
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

    // Activa un logro y lo guarda
    public void ActivarLogro(string id)
    {
        if (IsLogroActivado(id)) return; // Si ya lo tiene, ignoramos

        PlayerPrefs.SetInt(id, 1);
        PlayerPrefs.Save();

        Debug.Log($"[LOGROS] ¡Desbloqueado!: {id}");

        // TODO: Aquí puedes instanciar el prefab de tu pop-up visual, reproducir el sonido (.wav) y lanzar las partículas.
    }

    // Método que tu compañero usará para su ventana de logros
    public bool IsLogroActivado(string id)
    {
        return PlayerPrefs.GetInt(id, 0) == 1;
    }

    // Función auxiliar para los logros de acumulación (Enemigos)
    public void RegistrarProgreso(string keyGuardado, int cantidad, int objetivo, string idLogro)
    {
        int progresoActual = PlayerPrefs.GetInt(keyGuardado, 0) + cantidad;
        PlayerPrefs.SetInt(keyGuardado, progresoActual);

        if (progresoActual >= objetivo)
        {
            ActivarLogro(idLogro);
        }
    }
    // Función para resetear todos los logros 
    public void ResetearLogros()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Todos los estados de logros han sido reseteados a 0.");
    }
}
