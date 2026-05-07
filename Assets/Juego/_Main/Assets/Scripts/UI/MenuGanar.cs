using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools; 

public class MenuGanar : MonoBehaviour 
{
    [Header("Configuración")]
    [Tooltip("Escribe el nombre exacto de tu escena del menú principal")]
    public string nombreEscenaMenu = "MainMenu"; 

    public void IrAlHome() 
    {
        // Usamos el cargador de escenas de Corgi Engine para una transición limpia
        MMSceneLoadingManager.LoadScene(nombreEscenaMenu);
    }
}
