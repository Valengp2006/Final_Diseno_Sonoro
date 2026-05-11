using UnityEngine;
using UnityEngine.UI;

public class LogroPopup : MonoBehaviour
{
    [Header("Referencias UI del Popup")]
    public GameObject panelPopup;
    public Image iconoPopup;
    public Text tituloText;
    public Text descripcionText;
    public Text estadoText;
    public Button botonCerrar;
    
    [Header("Colores de Estado")]
    public Color colorDesbloqueado = new Color(0.3f, 0.8f, 0.3f);
    public Color colorBloqueado = new Color(0.8f, 0.3f, 0.3f);

    void Start()
    {
        // Ocultar popup al inicio
        if (panelPopup != null)
        {
            panelPopup.SetActive(false);
        }
        
        // Configurar botón cerrar
        if (botonCerrar != null)
        {
            botonCerrar.onClick.AddListener(CerrarPopup);
        }
    }

    public void MostrarPopup(LogroData datos, bool desbloqueado)
    {
        if (panelPopup == null) return;
        
        // Mostrar panel
        panelPopup.SetActive(true);
        
        // Asignar icono
        if (iconoPopup != null && datos.iconoSprite != null)
        {
            iconoPopup.sprite = datos.iconoSprite;
            iconoPopup.color = desbloqueado ? Color.white : new Color(0.5f, 0.5f, 0.5f);
        }
        
        // Asignar título
        if (tituloText != null)
        {
            tituloText.text = datos.titulo;
        }
        
        // Asignar descripción
        if (descripcionText != null)
        {
            descripcionText.text = datos.descripcion;
        }
        
        // Mostrar estado
        if (estadoText != null)
        {
            if (desbloqueado)
            {
                estadoText.text = "DESBLOQUEADO";
                estadoText.color = colorDesbloqueado;
            }
            else
            {
                estadoText.text = "BLOQUEADO";
                estadoText.color = colorBloqueado;
            }
        }
    }

    public void CerrarPopup()
    {
        if (panelPopup != null)
        {
            panelPopup.SetActive(false);
        }
    }
}
