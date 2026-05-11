using UnityEngine;

public class ResetLogros : MonoBehaviour
{
    public void ReiniciarYRecargar()
    {
        // Solo borrar los PlayerPrefs de logros
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        Debug.Log("¡Logros reiniciados!");
        
        // Refrescar la UI sin recargar la escena
        LogrosMenuManager manager = FindObjectOfType<LogrosMenuManager>();
        if (manager != null)
        {
            manager.RefrescarTodosLosLogros();
        }
    }
}