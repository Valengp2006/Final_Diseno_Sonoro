using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLogros : MonoBehaviour
{
    public void ReiniciarYRecargar()
    {
        // 1. Borramos la memoria usando el manager que tú creaste
        AchievementPersistence.Instance.ResetearLogros();

        // 2. Obtenemos el nombre de la escena en la que estamos (Menú o Nivel)
        Scene escenaActual = SceneManager.GetActiveScene();

        // 3. Recargamos la escena. Esto fuerza a la UI de tu compañero a leer los PlayerPrefs en 0.
        SceneManager.LoadScene(escenaActual.name);
    }
}
