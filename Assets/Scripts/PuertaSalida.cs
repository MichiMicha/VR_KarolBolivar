using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Va en la puerta de la sala del trono. En el MISMO GameObject necesitas:
/// un Collider (BoxCollider) y el Tag "Interactable".
/// Empieza bloqueada; CelestiaNPC la desbloquea (evento "Al Entregar" -> PuertaSalida.Desbloquear).
/// Cuando está desbloqueada y el jugador la mira, carga la escena final.
/// </summary>
public class PuertaSalida : MonoBehaviour
{
    [Header("Destino")]
    [Tooltip("Número de la escena final en Build Profiles / Build Settings")]
    public int escenaDestino = 3;

    [Header("Estado")]
    public bool desbloqueada = false;

    [Tooltip("Se enciende al desbloquear (luz, partículas, un brillo...). Opcional.")]
    public GameObject efectoDesbloqueo;

    [Header("Aviso cuando está cerrada (opcional)")]
    [Tooltip("Un Canvas/texto que dice, por ejemplo, 'Entrégale los elementos a Celestia'")]
    public GameObject avisoBloqueada;
    public float segundosAviso = 3f;

    [Header("Audio (opcional)")]
    public AudioClip sonidoDesbloqueo;

    void Start()
    {
        if (avisoBloqueada != null) avisoBloqueada.SetActive(false);
        if (efectoDesbloqueo != null) efectoDesbloqueo.SetActive(desbloqueada);
    }

    /// <summary>Se llama desde el evento de CelestiaNPC.</summary>
    public void Desbloquear()
    {
        desbloqueada = true;

        if (efectoDesbloqueo != null) efectoDesbloqueo.SetActive(true);
        if (avisoBloqueada != null) avisoBloqueada.SetActive(false);

        if (sonidoDesbloqueo != null)
        {
            AudioSource.PlayClipAtPoint(sonidoDesbloqueo, transform.position);
        }
    }

    // Lo llama CameraPointerManager (mediante SendMessage)
    public void OnPointerClickXR()
    {
        if (!desbloqueada)
        {
            if (avisoBloqueada != null)
            {
                avisoBloqueada.SetActive(true);
                CancelInvoke(nameof(OcultarAviso));
                Invoke(nameof(OcultarAviso), segundosAviso);
            }
            return;
        }

        SceneManager.LoadScene(escenaDestino);
    }

    private void OcultarAviso()
    {
        if (avisoBloqueada != null) avisoBloqueada.SetActive(false);
    }
}
