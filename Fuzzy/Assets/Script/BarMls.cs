using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarMls : MonoBehaviour
{
    private Image Mistura;
    private Image Maior;
    private Text TxtQntidade;
    [Range(0, 255)]
    public float RGBr;
    [Range(0, 100)]
    public float Forte;
    [Range(0, 100)]
    public float Suave;
    [Range(0, 100)]
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
}

    // Update is called once per frame
    void Update()
    {
        RGBr = (int)RGBr;
        MisturarCores();
        
        TxtQntidade.text = "" + RGBr;
    }
    void MisturarCores()
    {
        Mistura.color = CorT;
        Maior.color = CorM;

        
        CorT.r = Forte / 100;
        CorT.g = Suave / 100;
        CorT.b = Fraco / 100;
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
        RGBr = Value;
    }
}
