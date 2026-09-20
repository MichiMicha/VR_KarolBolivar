using System.Collections;
using UnityEngine;
using TMPro;

// *Algo creativo sobre ponys*

[RequireComponent(typeof(AudioSource))]
public class PersonajeDialogo : MonoBehaviour
{
    public GameObject globoDialogo;
    public TMP_Text textoDialogo;

    public float segundosPorMensaje = 3.5f;

    public string[] mensajesSinElementos;
    public string[] mensajesConElementos;

    public string[] mensajesDespuesDeEntregar;
    public AudioClip sonido;
    [Range(0f, 1f)] public float volumen = 0.8f;

    [Range(0f, 1f)] public float mezclaEspacial = 0f;

    private AudioSource fuente;
    private Coroutine rutinaDialogo;
    private bool entregado;

    void Awake()
    {
        fuente = GetComponent<AudioSource>();
        fuente.playOnAwake = false;
        fuente.loop = false;
        fuente.spatialBlend = mezclaEspacial;
    }

    void Start()
    {
        if (globoDialogo != null) globoDialogo.SetActive(false);
    }
    public void MarcarEntregado()
    {
        entregado = true;
    }
    public void OnPointerClickXR()
    {
        if (sonido != null)
        {
            fuente.PlayOneShot(sonido, volumen);
        }

        Decir(ElegirMensajes());
    }

    private string[] ElegirMensajes()
    {
        if (entregado && mensajesDespuesDeEntregar != null && mensajesDespuesDeEntregar.Length > 0)
        {
            return mensajesDespuesDeEntregar;
        }

        ElementosManager manager = ElementosManager.Instance;
        bool tieneTodos = manager != null && manager.TieneTodos;

        return tieneTodos ? mensajesConElementos : mensajesSinElementos;
    }

    private void Decir(string[] mensajes)
    {
        if (globoDialogo == null || textoDialogo == null)
        {
            Debug.LogWarning(name + ": falta asignar 'Globo Dialogo' o 'Texto Dialogo' en PersonajeDialogo.");
            return;
        }
        if (mensajes == null || mensajes.Length == 0) return;

        if (rutinaDialogo != null) StopCoroutine(rutinaDialogo);
        rutinaDialogo = StartCoroutine(MostrarMensajes(mensajes));
    }

    private IEnumerator MostrarMensajes(string[] mensajes)
    {
        globoDialogo.SetActive(true);

        foreach (string mensaje in mensajes)
        {
            textoDialogo.text = mensaje;
            yield return new WaitForSeconds(segundosPorMensaje);
        }

        globoDialogo.SetActive(false);
        rutinaDialogo = null;
    }
}
