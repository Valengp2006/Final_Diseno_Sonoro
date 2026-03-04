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
    
    // Variables privadas
    private Vector3 _escalaInicial;
    private float _tiempoInicio;
    
    /// <summary>
    /// Inicialización - guarda escala inicial y llama al Awake de Teleporter
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // Llama al Awake del Teleporter de Corgi
        
        _escalaInicial = transform.localScale;
        _tiempoInicio = Time.time;
    }
    
    /// <summary>
    /// Update - maneja las animaciones cada frame
    /// </summary>
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
    
    /// <summary>
    /// Rota el portal continuamente con variación opcional
    /// </summary>
    private void RotarPortal()
    {
        float velocidadActual = velocidadRotacion;
        
        // Si está activada la variación, modula la velocidad
        if (usarVariacionVelocidad)
        {
            float variacion = Mathf.Sin(Time.time * frecuenciaVariacion) * amplitudVariacion;
            velocidadActual += variacion;
        }
        
        // Rotar en el eje especificado
        transform.Rotate(ejeRotacion, velocidadActual * Time.deltaTime, Space.Self);
    }
    
    /// <summary>
    /// Hace que el portal pulse (cambie de tamaño suavemente)
    /// </summary>
    private void PulsarPortal()
    {
        float tiempoTranscurrido = Time.time - _tiempoInicio;
        
        // Calcular escala usando interpolación sinusoidal
        float escala = Mathf.Lerp(
            escalaMinima, 
            escalaMaxima, 
            (Mathf.Sin(tiempoTranscurrido * velocidadPulsacion) + 1f) / 2f
        );
        
        // Aplicar la escala manteniendo las proporciones originales
        transform.localScale = _escalaInicial * escala;
    }
    
    /// <summary>
    /// Sobreescribe el método de teleportación para añadir efectos personalizados
    /// </summary>
    protected override void Teleport(Collider2D collider)
    {
        // Aquí puedes añadir efectos personalizados antes del teleport
        // Por ejemplo: sonido especial, partículas, etc.
        
        // Llamar al método original del Teleporter
        base.Teleport(collider);
    }
    
    /// <summary>
    /// Se ejecuta cuando la secuencia de teleport comienza
    /// Sobreescribe para añadir efectos personalizados
    /// </summary>
    protected override void SequenceStart(Collider2D collider)
    {
        // Efectos personalizados al inicio del teleport
        // Ejemplo: aumentar velocidad de rotación
        if (rotacionActiva && usarVariacionVelocidad)
        {
            // Temporalmente acelerar la rotación
            velocidadRotacion *= 1.5f;
        }
        
        // Llamar al método original
        base.SequenceStart(collider);
    }
    
    /// <summary>
    /// Se ejecuta cuando la secuencia de teleport termina
    /// </summary>
    protected override void SequenceEnd(Collider2D collider)
    {
        // Restaurar velocidad de rotación normal
        if (rotacionActiva && usarVariacionVelocidad)
        {
            velocidadRotacion /= 1.5f;
        }
        
        // Llamar al método original
        base.SequenceEnd(collider);
    }
    
    /// <summary>
    /// Activa o desactiva la rotación (útil para control desde otros scripts)
    /// </summary>
    public void ActivarRotacion(bool activar)
    {
        rotacionActiva = activar;
    }
    
    /// <summary>
    /// Cambia la velocidad de rotación en runtime
    /// </summary>
    public void CambiarVelocidadRotacion(float nuevaVelocidad)
    {
        velocidadRotacion = nuevaVelocidad;
    }
    
    /// <summary>
    /// Visualización en el editor - añade indicadores de rotación
    /// </summary>
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos(); // Dibuja las flechas de destino de Corgi
        
        // Añadir indicador visual de rotación
        if (rotacionActiva)
        {
            Gizmos.color = new Color(0f, 1f, 1f, 0.3f); // Cyan translúcido
            
            // Dibujar círculo indicando la rotación
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
            
            // Dibujar flecha indicando dirección de rotación
            Vector3 puntoFlecha = centro + new Vector3(radio, 0f, 0f);
            Vector3 direccionFlecha = new Vector3(0f, radio * 0.3f, 0f);
            Gizmos.DrawLine(puntoFlecha, puntoFlecha + direccionFlecha);
        }
    }
}