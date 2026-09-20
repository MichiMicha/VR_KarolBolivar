using UnityEngine;

// Vivan las burras de colores que te enseñan sobre la amistad.
public class ElementoArmonia : MonoBehaviour
{
    public AudioClip sonidoRecolectar;

    public float velocidadGiro = 40f;

    public float alturaFlotar = 0.05f;

    public float velocidadFlotar = 1.5f;

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

        gameObject.SetActive(false);
    }
}
