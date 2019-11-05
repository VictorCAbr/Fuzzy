using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopoFuzzy : MonoBehaviour
{
    private Image Mistura;
    private Image Maior;
    public Vector2 FxFaixa = new Vector2(0, 100);
    public float MXml;

    [Range(0, 100)]
    public float MLS;
    [Range(0, 1)]
    public float Forte;
    [Range(0, 1)]
    public float Suave;
    [Range(0, 1)]
    public float Fraco;
    public Color CorT;
    public Color CorM;

    public GameObject Escolha;
    public GameObject gbCoca;
    public GameObject gbPepsi;
    public GameObject gbRum;
    public GameObject gbGelo;
    public EstadosFuzzy fRefri;
    public EstadosFuzzy fRum;
    public EstadosFuzzy fGelo;
    public EstadosFuzzy fCuba = new EstadosFuzzy();


    // Start is called before the first frame update
    void Start()
    {
        Mistura = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        Maior = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        CorT = Color.white;
        CorM = Color.white;
        GetComponent<Slider>().minValue = FxFaixa.x;
        GetComponent<Slider>().maxValue = FxFaixa.y;

        fRefri = gbCoca.GetComponent<BarMls>().Fuzzyficar;
        fRum = gbRum.GetComponent<BarMls>().Fuzzyficar;
        fGelo = gbGelo.GetComponent<BarMls>().Fuzzyficar;
    }

    // Update is called once per frame
    void Update()
    {
        MLS = fCuba.MLS;
        Forte = fCuba.Forte;
        Suave = fCuba.Suave;
        Fraco = fCuba.Fraco;


        fCuba.MLS = fRefri.MLS + fRum.MLS + fGelo.MLS;
        MXml = 150;
        if (fCuba.MLS > 100)
            MXml = 200;
        if (fCuba.MLS > 150)
            MXml = 300;
        GetComponent<Slider>().maxValue = MXml;
        GetComponent<Slider>().value = fCuba.MLS;
        MisturarCores();

        fCuba.Suave = Mathf.Max(
            Mathf.Min(fRefri.Forte, fRum.Fraco, fGelo.Suave),
            Mathf.Min(fRefri.Suave, fRum.Suave, fGelo.Suave),
            Mathf.Min(fRefri.Fraco, fRum.Forte, fGelo.Suave));

        fCuba.Forte = Mathf.Max(
            Mathf.Min(fRefri.Forte, fRum.Suave, fGelo.Suave),
            Mathf.Min(fRefri.Suave, fRum.Forte, fGelo.Suave),
            Mathf.Min(fRefri.Forte, fRum.Forte, fGelo.Suave));

        fCuba.Fraco = Mathf.Max(
            Mathf.Min(fRefri.Fraco, fRum.Suave, fGelo.Suave),
            Mathf.Min(fRefri.Suave, fRum.Fraco, fGelo.Suave),
            Mathf.Min(fRefri.Fraco, fRum.Fraco, fGelo.Suave));
    }
    void MisturarCores()
    {
        Mistura.color = CorT;
        Maior.color = CorM;


        CorT.r = Forte;
        CorT.g = Suave;
        CorT.b = Fraco;
        CorT.a = 1;
        if (CorT == Color.black)
            CorT = Color.white;

        CorM = Color.white;
        if (Forte > Mathf.Max(Suave, Fraco))
            CorM = Color.red;
        if (Suave > Mathf.Max(Forte, Fraco))
            CorM = Color.green;
        if (Fraco > Mathf.Max(Suave, Forte))
            CorM = Color.blue;
    }
    public void SelecaoPepsi()
    {
        int value = Escolha.GetComponent<Dropdown>().value;
        switch (value)
        {
            case 0:
                fRefri = gbCoca.GetComponent<BarMls>().Fuzzyficar;
                break;
            case 1:
                fRefri = gbPepsi.GetComponent<BarMls>().Fuzzyficar;
                break;

        }
    }
}
