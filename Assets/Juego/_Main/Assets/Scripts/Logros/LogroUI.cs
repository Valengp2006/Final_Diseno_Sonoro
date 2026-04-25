using UnityEngine;
using UnityEngine.UI;

public class LogroUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public Image iconoImage;
    public Button botonLogro;
    
    [Header("Estados Visuales")]
    public Color colorDesbloqueado = Color.white;
    public Color colorBloqueado = new Color(0.3f, 0.3f, 0.3f, 0.5f);
    
    private LogroData datosLogro;
    private bool estaDesbloqueado;
    private LogroPopup popupRef;

    public void Inicializar(LogroData datos, LogroPopup popup)
    {
        datosLogro = datos;
        popupRef = popup;
        
        // Asignar sprite
        if (iconoImage != null && datos.iconoSprite != null)
        {
            iconoImage.sprite = datos.iconoSprite;
        }
        
        // Verificar estado del logro
        estaDesbloqueado = AchievementPersistence.Instance.IsLogroActivado(datos.id);
        
        // Aplicar estado visual
        ActualizarEstadoVisual();
        
        // Configurar botón
        if (botonLogro != null)
        {
            botonLogro.onClick.AddListener(AlHacerClic);
        }
    }

    private void ActualizarEstadoVisual()
    {
        if (iconoImage != null)
        {
            iconoImage.color = estaDesbloqueado ? colorDesbloqueado : colorBloqueado;
        }
    }

    private void AlHacerClic()
    {
        // Usar la referencia directa al popup
        if (popupRef != null && datosLogro != null)
        {
            popupRef.MostrarPopup(datosLogro, estaDesbloqueado);
        }
        else
        {
            Debug.LogError("Popup o datos del logro son null");
        }
    }

    // Método para refrescar el estado (por si se desbloquea durante el juego)
    public void RefrescarEstado()
    {
        estaDesbloqueado = AchievementPersistence.Instance.IsLogroActivado(datosLogro.id);
        ActualizarEstadoVisual();
    }
}