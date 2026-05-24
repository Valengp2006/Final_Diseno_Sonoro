using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

[AddComponentMenu("Luna Game/Portales/Portal Estelar")]
public class PortalEstelar : Teleporter
{
    [MMInspectorGroup("Animación de Rotación", true, 30)]
    
    [Tooltip("Activar/desactivar la rotación del portal")]
    public bool rotacionActiva = true;
    
    [Tooltip("Velocidad de rotación en grados por segundo (positivo = derecha)")]
    public float velocidadRotacion = 100f;
    
    [Tooltip("Eje de rotación (Z para 2D)")]
    public Vector3 ejeRotacion = Vector3.forward;
    
    [Header("✨ Efectos Adicionales (Opcional)")]
    
    [Tooltip("Activar variación de velocidad (efecto pulsante)")]
    public bool usarVariacionVelocidad = false;
    
    [Tooltip("Amplitud de la variación de velocidad")]
    [MMCondition("usarVariacionVelocidad", true)]
    public float amplitudVariacion = 30f;
    
    [Tooltip("Frecuencia de la variación")]
    [MMCondition("usarVariacionVelocidad", true)]
    public float frecuenciaVariacion = 1f;
    
    [Tooltip("Activar pulsación de escala")]
    public bool usarPulsacion = false;
    
    [Tooltip("Escala mínima")]
    [MMCondition("usarPulsacion", true)]
    public float escalaMinima = 0.95f;
    
    [Tooltip("Escala máxima")]
    [MMCondition("usarPulsacion", true)]
    public float escalaMaxima = 1.05f;
    
    [Tooltip("Velocidad de pulsación")]
    [MMCondition("usarPulsacion", true)]
    public float velocidadPulsacion = 2f;

    [Header("Audio")]
    public AudioClip sonidoPortal;
    [Range(0f, 1f)]
    public float volumenPortal = 1f;
    
    // Variables privadas
    private Vector3 _escalaInicial;
    private float _tiempoInicio;
    
    protected override void Awake()
    {
        base.Awake();
        _escalaInicial = transform.localScale;
        _tiempoInicio = Time.time;
    }
    
    protected virtual void Update()
    {
        if (rotacionActiva)
        {
            RotarPortal();
        }
        
        if (usarPulsacion)
        {
            PulsarPortal();
        }
    }
    
    private void RotarPortal()
    {
        float velocidadActual = velocidadRotacion;
        
        if (usarVariacionVelocidad)
        {
            float variacion = Mathf.Sin(Time.time * frecuenciaVariacion) * amplitudVariacion;
            velocidadActual += variacion;
        }
        
        transform.Rotate(ejeRotacion, velocidadActual * Time.deltaTime, Space.Self);
    }
    
    private void PulsarPortal()
    {
        float tiempoTranscurrido = Time.time - _tiempoInicio;
        
        float escala = Mathf.Lerp(
            escalaMinima, 
            escalaMaxima, 
            (Mathf.Sin(tiempoTranscurrido * velocidadPulsacion) + 1f) / 2f
        );
        
        transform.localScale = _escalaInicial * escala;
    }
    
    protected override void Teleport(Collider2D collider)
    {
        if (sonidoPortal != null)
        {
            MMSoundManagerSoundPlayEvent.Trigger(
                sonidoPortal,
                MMSoundManager.MMSoundManagerTracks.Sfx,
                transform.position,
                false,
                volumenPortal
            );
        }
        
        base.Teleport(collider);
    }
    
    protected override void SequenceStart(Collider2D collider)
    {
        if (rotacionActiva && usarVariacionVelocidad)
        {
            velocidadRotacion *= 1.5f;
        }
        
        base.SequenceStart(collider);
    }
    
    protected override void SequenceEnd(Collider2D collider)
    {
        if (rotacionActiva && usarVariacionVelocidad)
        {
            velocidadRotacion /= 1.5f;
        }
        
        base.SequenceEnd(collider);
    }
    
    public void ActivarRotacion(bool activar)
    {
        rotacionActiva = activar;
    }
    
    public void CambiarVelocidadRotacion(float nuevaVelocidad)
    {
        velocidadRotacion = nuevaVelocidad;
    }
    
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        if (rotacionActiva)
        {
            Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
            
            Vector3 centro = transform.position;
            float radio = 1f;
            int segmentos = 20;
            
            for (int i = 0; i < segmentos; i++)
            {
                float angulo1 = (float)i / segmentos * Mathf.PI * 2f;
                float angulo2 = (float)(i + 1) / segmentos * Mathf.PI * 2f;
                
                Vector3 punto1 = centro + new Vector3(Mathf.Cos(angulo1) * radio, Mathf.Sin(angulo1) * radio, 0f);
                Vector3 punto2 = centro + new Vector3(Mathf.Cos(angulo2) * radio, Mathf.Sin(angulo2) * radio, 0f);
                
                Gizmos.DrawLine(punto1, punto2);
            }
            
            Vector3 puntoFlecha = centro + new Vector3(radio, 0f, 0f);
            Vector3 direccionFlecha = new Vector3(0f, radio * 0.3f, 0f);
            Gizmos.DrawLine(puntoFlecha, puntoFlecha + direccionFlecha);
        }
    }
}