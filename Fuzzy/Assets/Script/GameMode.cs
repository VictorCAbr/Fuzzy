using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public bool Gaming;
    public GameObject gbEscolhas;
    public GameObject gbRecita;
    public GameObject gbPedidos;
    public GameObject gbEnter;
    public GameObject gbCopo;

    public Text TxtRelogio;
    public Vector2 MxRelogio;
    public float Relogio;
    public Vector2 R;
    public float MaisTempo;
    public float MxTempoPedido;
    public float TempPedido;
    public Sprite[] sPedidos = new Sprite[7];
    public GameObject pPedidos;
    [HideInInspector]
    public Image[] iPedidos = new Image[10];
    public Vector3[] vPedidos = new Vector3[10];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < iPedidos.Length; i++)
        {
            iPedidos[i] = pPedidos.transform.GetChild(i).GetComponent<Image>();
        }
        TempPedido = MxTempoPedido;

    }
    // Update is called once per frame
    void Update()
    {
        if (Gaming)
            FazerPedidos();
        Att();
        Relogio -= Time.deltaTime;
        if (Relogio < 0)
            Gaming = false;

    }
    void FazerPedidos()
    {
        TempPedido += Time.deltaTime;
        if (TempPedido > MxTempoPedido)
        {
            TempPedido = 00;


            int j = 0;
            while (vPedidos[j].z != 0 && j < vPedidos.Length)
            {
                j++;
            }
            // Debug.Log(j);
            if (j < vPedidos.Length)
            {
                int Tipo = (int)(Random.Range(0, 10) % 2);
                int Paladar = (int)(Random.Range(0, 10) % 3);
                int Sabor = Tipo * 3 + Paladar + 1;


                vPedidos[j] = new Vector3(Tipo, Paladar, Sabor);

                iPedidos[j].sprite = sPedidos[Sabor];
            }

            for (int i = 110; i < vPedidos.Length; i++)
            {
                int Tipo = (int)(Random.Range(0, 10) % 2);
                int Paladar = (int)(Random.Range(0, 10) % 3);
                int Sabor = Tipo * 3 + Paladar + 1;


                vPedidos[i] = new Vector3(Tipo, Paladar, Sabor);

                iPedidos[i].sprite = sPedidos[Sabor];
            }
        }
    }
    void Att()
    {

        gbEscolhas.SetActive(!Gaming);
        gbRecita.SetActive(!Gaming);
        gbPedidos.SetActive(Gaming);
        gbEnter.SetActive(Gaming);
        if (Gaming)
        {
            TxtRelogio.text = (int)(Relogio / 60) + ":";
            if ((Relogio % 60) < 10)
                TxtRelogio.text += "0";
            TxtRelogio.text += (int)(Relogio % 60);
        }
        R.y = (int)(Relogio % 60);
        R.x = (int)(Relogio / 60);

    }
    public void Enter()
    {
        gbCopo.GetComponent<CopoFuzzy>().Limpar();
        Debug.Log(gbCopo.GetComponent<CopoFuzzy>().ValorPaladar);
    }
    public void Selecao()
    {
        int value = GetComponent<Dropdown>().value;
        switch (value)
        {
            case 0:
                Gaming = false;
                break;
            case 1:
                Gaming = true;
                Relogio = MxRelogio.x * 60 + MxRelogio.y;
                break;

        }
    }
}
