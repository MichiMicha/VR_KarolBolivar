using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ElementosManager : MonoBehaviour
{
    public static ElementosManager Instance;
    public int totalRequerido = 6;
    public int elementosIniciales = 0;

    public TMP_Text textoContador;

    public string formatoContador = "Elementos: {0}/{1}";

    public UnityEvent OnElementoRecolectado;

    public UnityEvent OnTodosRecolectados;

    public int Recolectados { get; private set; }

    public bool TieneTodos
    {
        get { return Recolectados >= totalRequerido; }
    }

    public int Faltan
    {
        get { return Mathf.Max(0, totalRequerido - Recolectados); }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Recolectados = elementosIniciales;
    }

    void Start()
    {
        ActualizarTexto();
    }
    public void RegistrarElemento()
    {
        Recolectados++;
        ActualizarTexto();

        OnElementoRecolectado?.Invoke();

        if (Recolectados == totalRequerido)
        {
            OnTodosRecolectados?.Invoke();
        }
    }

    private void ActualizarTexto()
    {
        if (textoContador != null)
        {
            textoContador.text = string.Format(formatoContador, Recolectados, totalRequerido);
        }
    }
}

// Apoco si muy amistoso