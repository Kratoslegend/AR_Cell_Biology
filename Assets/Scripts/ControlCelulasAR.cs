using TMPro;
using UnityEngine;

public class ControlCelulasAR : MonoBehaviour
{
    [Header("Contenido de las células")]
    [SerializeField] private Transform celulaAnimal;
    [SerializeField] private Transform celulaVegetal;

    [Header("Interfaz educativa")]
    [SerializeField] private TMP_Text textoInformacion;

    [Header("Configuración de interacción")]
    [SerializeField] private float gradosPorToque = 30f;
    [SerializeField] private float pasoEscala = 0.15f;
    [SerializeField] private float escalaMinima = 0.45f;
    [SerializeField] private float escalaMaxima = 1.50f;

    private Vector3 escalaInicialAnimal;
    private Vector3 escalaInicialVegetal;
    private Quaternion rotacionInicialAnimal;
    private Quaternion rotacionInicialVegetal;

    private const string MensajeInicial =
        "Apunta la cámara a uno de los marcadores para explorar la célula.";

    private void Awake()
    {
        if (celulaAnimal != null)
        {
            escalaInicialAnimal = celulaAnimal.localScale;
            rotacionInicialAnimal = celulaAnimal.localRotation;
        }

        if (celulaVegetal != null)
        {
            escalaInicialVegetal = celulaVegetal.localScale;
            rotacionInicialVegetal = celulaVegetal.localRotation;
        }
    }

    private void Start()
    {
        MostrarInstruccionInicial();
    }

    public void Rotar()
    {
        RotarCelula(celulaAnimal);
        RotarCelula(celulaVegetal);

        Debug.Log("Se rotaron las células.");
    }

    public void Acercar()
    {
        CambiarEscala(celulaAnimal, pasoEscala);
        CambiarEscala(celulaVegetal, pasoEscala);

        Debug.Log("Se aumentó el tamaño de las células.");
    }

    public void Alejar()
    {
        CambiarEscala(celulaAnimal, -pasoEscala);
        CambiarEscala(celulaVegetal, -pasoEscala);

        Debug.Log("Se redujo el tamaño de las células.");
    }

    public void Reiniciar()
    {
        if (celulaAnimal != null)
        {
            celulaAnimal.localScale = escalaInicialAnimal;
            celulaAnimal.localRotation = rotacionInicialAnimal;
        }

        if (celulaVegetal != null)
        {
            celulaVegetal.localScale = escalaInicialVegetal;
            celulaVegetal.localRotation = rotacionInicialVegetal;
        }

        OcultarSprite(celulaAnimal);
        OcultarSprite(celulaVegetal);
        MostrarInstruccionInicial();

        Debug.Log("La experiencia de realidad aumentada fue reiniciada.");
    }

    public void MostrarInformacionAnimal()
    {
        if (textoInformacion == null)
        {
            return;
        }

        textoInformacion.text =
            "<b>CÉLULA ANIMAL - ESTRUCTURAS VISIBLES</b>\n" +
            "• <b>Núcleo:</b> gran esfera morada que dirige la actividad celular.\n" +
            "• <b>Nucléolo:</b> esfera más oscura ubicada dentro del núcleo.\n" +
            "• <b>Mitocondrias:</b> óvalos naranjas con pliegues internos; producen energía.\n" +
            "• <b>Retículo endoplasmático:</b> red azul alrededor del núcleo; fabrica proteínas y lípidos.\n" +
            "• <b>Ribosomas:</b> pequeños puntos adheridos al retículo.\n" +
            "• <b>Aparato de Golgi:</b> sacos rosados o naranjas apilados; procesa sustancias.\n" +
            "• <b>Lisosomas y vesículas:</b> pequeñas esferas de distintos colores.\n" +
            "• <b>Centríolos:</b> dos cilindros amarillos que participan en la división celular.";

        Debug.Log("Marcador de célula animal detectado.");
    }

    public void MostrarInformacionVegetal()
    {
        if (textoInformacion == null)
        {
            return;
        }

        textoInformacion.text =
            "<b>CÉLULA VEGETAL - ESTRUCTURAS VISIBLES</b>\n" +
            "• <b>Pared celular:</b> borde verde grueso que entrega rigidez y protección.\n" +
            "• <b>Membrana celular:</b> capa delgada situada dentro de la pared celular.\n" +
            "• <b>Vacuola central:</b> gran espacio celeste que almacena agua y sustancias.\n" +
            "• <b>Núcleo:</b> esfera morada que controla las funciones celulares.\n" +
            "• <b>Cloroplastos:</b> óvalos verdes con discos internos; realizan la fotosíntesis.\n" +
            "• <b>Mitocondrias:</b> óvalos naranjas que producen energía.\n" +
            "• <b>Retículo endoplasmático:</b> red azul cercana al núcleo.\n" +
            "• <b>Aparato de Golgi:</b> sacos naranjas apilados que procesan sustancias.";

        Debug.Log("Marcador de célula vegetal detectado.");
    }

    public void MostrarInstruccionInicial()
    {
        if (textoInformacion == null)
        {
            Debug.LogError("No está asignado Texto_Informacion.");
            return;
        }

        textoInformacion.gameObject.SetActive(true);
        textoInformacion.enabled = true;
        textoInformacion.color = new Color32(255, 255, 255, 255);
        textoInformacion.text = MensajeInicial;
        textoInformacion.ForceMeshUpdate();

        Debug.Log("Texto inicial actualizado correctamente.");
    }

    private void RotarCelula(Transform objetivo)
    {
        if (objetivo != null)
        {
            objetivo.Rotate(Vector3.forward, gradosPorToque, Space.Self);
        }
    }

    private void CambiarEscala(Transform objetivo, float cambio)
    {
        if (objetivo == null)
        {
            return;
        }

        float nuevaEscala = Mathf.Clamp(
            objetivo.localScale.x + cambio,
            escalaMinima,
            escalaMaxima
        );

        objetivo.localScale = new Vector3(
            nuevaEscala,
            nuevaEscala,
            nuevaEscala
        );
    }

    private void OcultarSprite(Transform objetivo)
    {
        if (objetivo == null)
        {
            return;
        }

        SpriteRenderer sprite = objetivo.GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            sprite.enabled = false;
        }
    }
}