using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public Sprite N;
    public Sprite P;
    public GameObject P1;
    public GameObject P2;

    public GameObject Pl1;
    public GameObject Pl2;

    public GameObject Both;

    public Canvas Game;
    private Text T1;
    private Text T2;

    bool x = true;
    bool y = true;

    IEnumerator StartText()
    {
        ShowBoth();
        yield return new WaitForSeconds(5F);
        ClearBoth();
    }
    // Use this for initialization
    void Start () {
        T1 = Pl1.GetComponent<Text>();
        T2 = Pl2.GetComponent<Text>();
        Game.GetComponent<CanvasGroup>().alpha = 0;
        Game.GetComponent<CanvasGroup>().interactable = false;
        x = true;
        y = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (DDOL.instance.StartingC)
        {
            if (DDOL.instance.StartingC.tag == "Necro")
            {
                P1.GetComponent<Image>().sprite = N;
            }
            else if (DDOL.instance.StartingC.tag == "Paladin")
            {
                P1.GetComponent<Image>().sprite = P;
            }
        }
        if (DDOL.instance.StartingC2)
        {
            if (DDOL.instance.StartingC2.tag == "Necro")
            {
                P2.GetComponent<Image>().sprite = N;
            }
            else if (DDOL.instance.StartingC2.tag == "Paladin")
            {
                P2.GetComponent<Image>().sprite = P;
            }
        }
       /* if (DDOL.instance.IC && x)
        {
            if ((DDOL.instance.IC.GetComponent<MouseDetect>().MAX_HP)/2 < DDOL.instance.IC.GetComponent<MouseDetect>().MAX_HP)
            {
                Advantage(2);
                Disadvantage(1);
                x = false;
            }
        }
        if (DDOL.instance.IC2 && y)
        {
            if ((DDOL.instance.IC2.GetComponent<MouseDetect>().MAX_HP)/ 2 <= DDOL.instance.IC2.GetComponent<MouseDetect>().MAX_HP)
            {
                Disadvantage(2);
                Advantage(1);
                y = false;
            }
        }*/
    }
    public void Advantage(int player)
    {
        if (player == 1)
        {
            if (DDOL.instance.IC.gameObject.tag == "Paladin" && DDOL.instance.IC2.gameObject.tag == "Paladin")
            {
                T1.text = "You'd best prepare yourself.\n I must press the attack!";
            } else if (DDOL.instance.IC.gameObject.tag == "Necro" && DDOL.instance.IC2.gameObject.tag == "Paladin")
            {
                T1.text = "Feel the wrath of those you loathe!\nHahahahaha!";
            }
            else if (DDOL.instance.IC.gameObject.tag == "Paladin" && DDOL.instance.IC2.gameObject.tag == "Necro")
            {
                T1.text = "Get ready for a strike of justice!\nI’ll take this advantage.";
            }else if (DDOL.instance.IC.gameObject.tag == "Necro" && DDOL.instance.IC2.gameObject.tag == "Necro")
            {
                T1.text = "I’ve got the upper hand!/Is this all you can muster, my fellow necromancer?";
            }
    }
        else
        {
            if (DDOL.instance.IC2.gameObject.tag == "Paladin" && DDOL.instance.IC.gameObject.tag == "Paladin")
            {
                T2.text = "You'd best prepare yourself.\n I must press the attack!";
            }
            else if (DDOL.instance.IC2.gameObject.tag == "Necro" && DDOL.instance.IC.gameObject.tag == "Paladin")
            {
                T2.text = "Feel the wrath of those you loathe!\nHahahahaha!";
            }
            else if (DDOL.instance.IC2.gameObject.tag == "Paladin" && DDOL.instance.IC.gameObject.tag == "Necro")
            {
                T2.text = "Get ready for a strike of justice!\nI’ll take this advantage.";
            }
            else if (DDOL.instance.IC2.gameObject.tag == "Necro" && DDOL.instance.IC.gameObject.tag == "Necro")
            {
                T2.text = "I’ve got the upper hand!/Is this all you can muster, my fellow necromancer?";
            }
        }
    }
    public void Disadvantage(int player)
    {
        if (player == 1)
        {
            if (DDOL.instance.IC.gameObject.tag == "Paladin" && DDOL.instance.IC2.gameObject.tag == "Paladin")
            {
                T1.text = "I will not allow myself to fall here!\nAs long as I have the light, I will be all right.";
            }
            else if (DDOL.instance.IC.gameObject.tag == "Necro" && DDOL.instance.IC2.gameObject.tag == "Paladin")
            {
                T1.text = "You’ll need more than righteousness to best me./Hehehe, you’re a threat after all.";
            }
            else if (DDOL.instance.IC.gameObject.tag == "Paladin" && DDOL.instance.IC2.gameObject.tag == "Necro")
            {
                T1.text = "I must not be devoured by the dark!\nYou pose a great challenge, but I won’t lose!";
            }
            else if (DDOL.instance.IC.gameObject.tag == "Necro" && DDOL.instance.IC2.gameObject.tag == "Necro")
            {
                T1.text = "You’re tough... keep that prowess on display!\nI must rise to the challenge.";
            }
        }
        else
        {
            if (DDOL.instance.IC2.gameObject.tag == "Paladin" && DDOL.instance.IC.gameObject.tag == "Paladin")
            {
                T2.text = "I will not allow myself to fall here!\nAs long as I have the light, I will be all right.";
            }
            else if (DDOL.instance.IC2.gameObject.tag == "Necro" && DDOL.instance.IC.gameObject.tag == "Paladin")
            {
                T2.text = "You’ll need more than righteousness to best me./Hehehe, you’re a threat after all.";
            }
            else if (DDOL.instance.IC2.gameObject.tag == "Paladin" && DDOL.instance.IC.gameObject.tag == "Necro")
            {
                T2.text = "I must not be devoured by the dark!\nYou pose a great challenge, but I won’t lose!";
            }
            else if (DDOL.instance.IC2.gameObject.tag == "Necro" && DDOL.instance.IC.gameObject.tag == "Necro")
            {
                T2.text = "You’re tough... keep that prowess on display!\nI must rise to the challenge.";
            }
        }
    }
    public void WinLose(int winvar)
    {
        if (winvar == 1)
        {
            if (DDOL.instance.IC.gameObject.tag == "Paladin" && DDOL.instance.IC2.gameObject.tag == "Paladin")
            {
                T1.text = "I expected no less./I shone brilliantly, don’t you think?\nI only hope I made my brothers proud.";
                T2.text = "You were far more brilliant this time…\nI guess it can’t be helped.";
            }
            else if (DDOL.instance.IC.gameObject.tag == "Necro" && DDOL.instance.IC2.gameObject.tag == "Paladin")
            {
                T1.text = "Your soul is mine!/Hardly a worthy rival.\nDid you witness this utter beatdown, my ancestors?";
                T2.text = "I can’t believe it…/I’ve failed the paladins… forgive me, everyone.";
            }
            else if (DDOL.instance.IC.gameObject.tag == "Paladin" && DDOL.instance.IC2.gameObject.tag == "Necro")
            {
                T1.text = "See? Light must emerge victorious!\nI’d be more than happy to beat you next time, too.";
                T2.text = "The light within you is strong…\nI’ve shamed my brethren.";
            }
            else if (DDOL.instance.IC.gameObject.tag == "Necro" && DDOL.instance.IC2.gameObject.tag == "Necro")
            {
                T1.text = "Clawing my way to the top!/Care to give more of a challenge next time?";
                T2.text = "You’ve mastered necromancy further than me…/My apologies for a poor performance.";
            }
        } else if (winvar == 2)
        {
            if (DDOL.instance.IC2.gameObject.tag == "Paladin" && DDOL.instance.IC.gameObject.tag == "Paladin")
            {
                T2.text = "I expected no less./I shone brilliantly, don’t you think?\nI only hope I made my brothers proud.";
                T1.text = "You were far more brilliant this time…\nI guess it can’t be helped.";
            }
            else if (DDOL.instance.IC2.gameObject.tag == "Necro" && DDOL.instance.IC.gameObject.tag == "Paladin")
            {
                T2.text = "Your soul is mine!/Hardly a worthy rival.\nDid you witness this utter beatdown, my ancestors?";
                T1.text = "I can’t believe it…/I’ve failed the paladins… forgive me, everyone.";
            }
            else if (DDOL.instance.IC2.gameObject.tag == "Paladin" && DDOL.instance.IC.gameObject.tag == "Necro")
            {
                T2.text = "See? Light must emerge victorious!\nI’d be more than happy to beat you next time, too.";
                T1.text = "The light within you is strong…\nI’ve shamed my brethren.";
            }
            else if (DDOL.instance.IC2.gameObject.tag == "Necro" && DDOL.instance.IC.gameObject.tag == "Necro")
            {
                T2.text = "Clawing my way to the top!/Care to give more of a challenge next time?";
                T1.text = "You’ve mastered necromancy further than me…/My apologies for a poor performance.";
            }
        }
       // ShowBoth();

    }
    public void StartGame()
    {
        Game.GetComponent<CanvasGroup>().alpha = 1;
        Game.GetComponent<CanvasGroup>().interactable = true;

        if (P1.GetComponent<Image>().sprite == N && P2.GetComponent<Image>().sprite == N)
        {
            T1.text = "Play by the rules, shall we?\nI hope you’ll provide good practice.";
            T2.text = "Play by the rules, shall we?\nI hope you’ll provide good practice.";
        }
        if(P1.GetComponent<Image>().sprite == P && P2.GetComponent<Image>().sprite == N)
        {
            T1.text = "I’ll see to it that you’re defeated.\nMy light shall smother your darkness!";
            T2.text = "Justice shall be served...on a silver platter\nYou shall make a fine corpse to feed on.";
        }

        if (P2.GetComponent<Image>().sprite == P && P1.GetComponent<Image>().sprite == N)
        {
            T2.text = "I’ll see to it that you’re defeated.\nMy light shall smother your darkness!";
            T1.text = "Justice shall be served...on a silver platter\nYou shall make a fine corpse to feed on.";
        }
        if(P1.GetComponent<Image>().sprite == P && P2.GetComponent<Image>().sprite == P)
        {
            T1.text = "May the light be one with me!\nLet us see whose star shines brighter.";
            T2.text = "May the light be one with me!\nLet us see whose star shines brighter.";
        }
    }
    public void ClearBoth()
    {
        Both.SetActive(false);
    }
    public void ShowBoth()
    {
        Both.SetActive(true);
    }
}
