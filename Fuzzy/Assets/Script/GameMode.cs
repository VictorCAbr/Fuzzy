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

    public Text TxtRelogio;
    public Vector2 MxRelogio;
    public float Relogio;
    public Vector2 R;
    public float MaisTempo;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        Att();
        Relogio -= Time.deltaTime;
        if (Relogio < 0)
            Gaming = false;
        
    }
    void Att()
    {
        
        gbEscolhas.SetActive(!Gaming);
        gbRecita.SetActive(!Gaming);
        gbPedidos.SetActive(Gaming);
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
