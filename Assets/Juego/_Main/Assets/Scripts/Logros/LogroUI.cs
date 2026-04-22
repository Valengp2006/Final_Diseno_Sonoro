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

    public void Inicializar(LogroData datos)
    {
        datosLogro = datos;
        
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
        // Buscar el popup manager y mostrar la descripción
        LogroPopup popup = FindObjectOfType<LogroPopup>();
        if (popup != null)
        {
            popup.MostrarPopup(datosLogro, estaDesbloqueado);
        }
    }

    // Método para refrescar el estado (por si se desbloquea durante el juego)
    public void RefrescarEstado()
    {
        estaDesbloqueado = AchievementPersistence.Instance.IsLogroActivado(datosLogro.id);
        ActualizarEstadoVisual();
    }
}
