using UnityEngine;

/// <summary>
/// Va en cada elemento de la armonía que está sobre un pilar.
/// En el MISMO GameObject necesitas: un Collider (BoxCollider) y el Tag "Interactable".
/// Cuando el jugador lo mira 2.5 s (o toca el botón del Cardboard) se recoge,
/// desaparece del pilar y suma 1 al ElementosManager.
/// </summary>
public class ElementoArmonia : MonoBehaviour
{
    [Header("Sonido (opcional)")]
    public AudioClip sonidoRecolectar;

    [Header("Animación (pon 0 para desactivar)")]
    [Tooltip("Grados por segundo que gira sobre sí mismo")]
    public float velocidadGiro = 40f;

    [Tooltip("Cuántos metros sube y baja flotando")]
    public float alturaFlotar = 0.05f;

    public float velocidadFlotar = 1.5f;

    [Tooltip("Cuánto crece cuando el jugador lo mira (1 = no cambia)")]
    public float escalaAlMirar = 1.15f;

    private bool recolectado;
    private bool mirando;
    private Vector3 posicionBase;
    private Vector3 escalaBase;

    void Start()
    {
        posicionBase = transform.position;
        escalaBase = transform.localScale;
    }

    void Update()
    {
        if (velocidadGiro != 0f)
        {
            transform.Rotate(0f, velocidadGiro * Time.deltaTime, 0f, Space.World);
        }

        if (alturaFlotar != 0f)
        {
            Vector3 p = posicionBase;
            p.y += Mathf.Sin(Time.time * velocidadFlotar) * alturaFlotar;
            transform.position = p;
        }

        Vector3 escalaObjetivo = mirando ? escalaBase * escalaAlMirar : escalaBase;
        transform.localScale = Vector3.Lerp(transform.localScale, escalaObjetivo, Time.deltaTime * 8f);
    }

    // ---- Métodos que llama CameraPointerManager (mediante SendMessage) ----

    public void OnPointerEnterXR()
    {
        mirando = true;
    }

    public void OnPointerExitXR()
    {
        mirando = false;
    }

    public void OnPointerClickXR()
    {
        if (recolectado) return;
        recolectado = true;

        if (sonidoRecolectar != null)
        {
            AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);
        }

        if (ElementosManager.Instance != null)
        {
            ElementosManager.Instance.RegistrarElemento();
        }
        else
        {
            Debug.LogWarning("Falta un ElementosManager en la escena.");
        }

        // Se desactiva (NO se destruye): CameraPointerManager sigue guardando una referencia a este objeto.
        gameObject.SetActive(false);
    }
}
