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
    public EstadosFuzzy fCoca;
    public EstadosFuzzy fPepsi;
    public EstadosFuzzy fNull = new EstadosFuzzy();
    public EstadosFuzzy fCuba = new EstadosFuzzy();

    private Text txtPreco, txtOoks;
    private bool Gaming;

    // Start is called before the first frame update
    void Start()
    {
        Mistura = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        Maior = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        txtPreco = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        txtOoks = transform.GetChild(3).GetComponent<Text>();
        CorT = Color.white;
        CorM = Color.white;
        GetComponent<Slider>().minValue = FxFaixa.x;
        GetComponent<Slider>().maxValue = FxFaixa.y;

        fRefri = gbCoca.GetComponent<BarMls>().Fuzzyficar;
        fRum = gbRum.GetComponent<BarMls>().Fuzzyficar;
        fGelo = gbGelo.GetComponent<BarMls>().Fuzzyficar;
        fCoca = gbCoca.GetComponent<BarMls>().Fuzzyficar;
        fPepsi = gbPepsi.GetComponent<BarMls>().Fuzzyficar;
    }

    // Update is called once per frame
    void Update()
    {
        Gaming = GameObject.Find("GameMode").GetComponent<GameMode>().Gaming;
        if (Gaming)
            Selecao();

        MLS = fCuba.MLS;
        Forte = fCuba.Forte;
        Suave = fCuba.Suave;
        Fraco = fCuba.Fraco;

        if (Gaming)
            fCuba.MLS = fPepsi.MLS + fCoca.MLS + fRum.MLS + fGelo.MLS;
        else
            fCuba.MLS = fRefri.MLS + fRum.MLS + fGelo.MLS;

        MXml = 150;
        if (fCuba.MLS > 100)
            MXml = 200;
        if (fCuba.MLS > 150)
            MXml = 300;
        if (fCuba.MLS > 250)
            MXml = 400;
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
        //if (Gaming)
        //{
        //    Mistura.color = Color.white;
        //    Maior.color = Color.white;
        //}
        //else
        {
            Mistura.color = CorT;
            Maior.color = CorM;
        }


        CorT.r = Forte;
        CorT.g = Suave;
        CorT.b = Fraco;
        CorT.a = 1;
        if (CorT == Color.black)
            CorT = Color.white;

        CorM = Color.white;
        txtPreco.text = "R$";
        if (Forte > Mathf.Max(Suave, Fraco))
        {
            CorM = Color.red;
            txtPreco.text += 25;
        }
        if (Suave > Mathf.Max(Forte, Fraco))
        {
            CorM = Color.green;
            txtPreco.text += 20;
        }
        if (Fraco > Mathf.Max(Suave, Forte))
        {
            CorM = Color.blue;
            txtPreco.text += 15;
        }
        if(CorM==Color.white)
            txtPreco.text += 00;

        txtPreco.text += ",00";

        if (!Gaming)
        {
            txtOoks.text = "Refri:  ";
            if (Mathf.Max(fRefri.Forte, fRefri.Suave, fRefri.Fraco) > 0)
                txtOoks.text += "OK";
            txtOoks.text += "\nRum:  ";
            if (Mathf.Max(fRum.Forte, fRum.Suave, fRum.Fraco) > 0)
                txtOoks.text += "OK";
            txtOoks.text += "\nGelo:  ";
            if (fGelo.Suave > 0)
                txtOoks.text += "OK";
            //txtOoks.text += "\nRum:  ";
        }


    }
    void Selecao()
    {
        if (fPepsi.MLS == 0 && fCoca.MLS == 0)
            fRefri = fNull;
        else if (fCoca.MLS == 0)
            fRefri = fPepsi;
        else if (fPepsi.MLS == 0)
            fRefri = fCoca;
        else
            fRefri = fNull;
    }
    public void SelecaoPepsi()
    {
        int value = Escolha.GetComponent<Dropdown>().value;
        switch (value)
        {
            case 0:
                fRefri = fCoca;
                break;
            case 1:
                fRefri = fPepsi;
                break;

        }
    }
}
