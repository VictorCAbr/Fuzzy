using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarMls : MonoBehaviour
{
    private Image Mistura;
    private Image Maior;
    private Text TxtQntidade;
    public Vector2 FxFaixa = new Vector2(0, 100);
    public Vector4 FxForte;
    public Vector4 FxSuave;
    public Vector4 FxFraco;

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

    // Start is called before the first frame update
    void Start()
    {
        Mistura = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        Maior = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        TxtQntidade = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        CorT = Color.white;
        CorM = Color.white;
        GetComponent<Slider>().minValue = FxFaixa.x;
        GetComponent<Slider>().maxValue = FxFaixa.y;
}

    // Update is called once per frame
    void Update()
    {
        MisturarCores();

        Forte = Pertinencia(FxForte);
        Suave = Pertinencia(FxSuave);
        Fraco = Pertinencia(FxFraco);

        TxtQntidade.text = "" + (int) MLS;
    }
    float Pertinencia(Vector4 Fx)
    {
        float p = 0;
        if (CorT==Color.white)
        {
            MLS = (int)MLS;
        }

        if (Fx.x < MLS && MLS <= Fx.y)
        {
            p = (MLS - Fx.x) / (Fx.y - Fx.x);
        }
        else if (Fx.y <= MLS && MLS <= Fx.z)
        {
            p = 1;
        }
        else if (Fx.z < MLS && MLS <= Fx.w)
        {
            p = (Fx.w - MLS) / (Fx.w - Fx.z);
        }



        return p;
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
    public void QantidadeML(float Value)
    {
        MLS = Value;
        if (MLS == FxFaixa.x)
            MLS = 0;
    }
}
