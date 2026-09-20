using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class CelestiaNPC : MonoBehaviour
{
    public GameObject globoDialogo;
    public TMP_Text textoDialogo;
    public float segundosPorMensaje = 4f;
    public string mensajeFaltan = "Todavía te faltan {0} elementos de la armonía. ¡Sigue buscando!";
    public string[] mensajesEntrega;
    public string mensajeDespuesDeEntrega = "Gracias por tu ayuda.";
    public AudioClip sonidoEntrega;
    public UnityEvent alEntregar;

    public bool cambiarEscenaAlEntregar = false;

    public int escenaSiguiente = 2;

    public float segundosAntesDeCambiar = 8f;

    private bool entregado;
    private Coroutine rutinaDialogo;

    void Start()
    {
        if (globoDialogo != null) globoDialogo.SetActive(false);
    }

    // El pointer lo llama cvfjvnf
    public void OnPointerClickXR()
    {
        if (entregado)
        {
            Decir(mensajeDespuesDeEntrega);
            return;
        }

        ElementosManager manager = ElementosManager.Instance;
        if (manager == null)
        {
            Debug.LogWarning("Falta un ElementosManager en la escena.");
            return;
        }

        if (!manager.TieneTodos)
        {
            Decir(string.Format(mensajeFaltan, manager.Faltan));
            return;
        }

        Entregar();
    }

    private void Entregar()
    {
        entregado = true;

        if (sonidoEntrega != null)
        {
            AudioSource.PlayClipAtPoint(sonidoEntrega, transform.position);
        }

        Decir(mensajesEntrega);
        alEntregar?.Invoke();

        if (cambiarEscenaAlEntregar)
        {
            Invoke(nameof(CambiarEscena), segundosAntesDeCambiar);
        }
    }

    private void CambiarEscena()
    {
        SceneManager.LoadScene(escenaSiguiente);
    }

    private void Decir(params string[] mensajes)
    {
        if (globoDialogo == null || textoDialogo == null) return;
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
