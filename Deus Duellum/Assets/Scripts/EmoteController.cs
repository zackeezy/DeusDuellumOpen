﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmoteController : MonoBehaviour {

    public int character;

    private string HelloEmote;
    private string WowEmote;
    private string TauntEmote;
    private string VictoryEmote;

    private Sprite helloImg;
    private Sprite wowImg;
    private Sprite tauntImg;

    private float voiceLineLength;
    public AudioClip[] HelloVoices;
    public AudioClip[] WowVoices;
    public AudioClip[] TauntVoices;
    public AudioClip[] VictoryVoices;    

    private GameObject emoteButtons;
    private GameObject emotePanel;
    private Text emoteText;

    AudioSource emoteSource;

    NetworkControl netController;

    BoardManager boardmanager;

    public bool emotesMuted = false;

    // Use this for initialization
    void Start () {
        //temporary
        voiceLineLength = 3;

        emoteButtons = transform.GetChild(4).gameObject;
        emotePanel = transform.GetChild(5).gameObject;

        //set the clip the emoteSource uses
        //WILL NOT WORK IF DO NOT START AT MAIN MENU
        GameObject Audio = GameObject.FindGameObjectWithTag("Audio");
        emoteSource = Audio.GetComponent<MusicInfo>().emotesSource.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OpenEmoteButtons()
    {
        if (emoteButtons.activeSelf)
        {
            CloseEmoteButtons();
        }
        else
        {
            emoteButtons.SetActive(true);
        }
    }

    public void CloseEmoteButtons()
    {
        emoteButtons.SetActive(false);  
    }

    public void SetCharacter(int givenCharacter, bool player1)
    {
        //function is called in BoardManager's setPrefs()

        boardmanager = GameObject.FindGameObjectWithTag("boardManager").GetComponent<BoardManager>();
        character = givenCharacter;
        if (character == 0)
        {
            //athena's emotes
            HelloEmote = "Yamas! Good health to all.";
            WowEmote = "Even the goddess of strategy \ncouldn't see that one coming.";
            TauntEmote = "You remind me a lot of Koalemos. \nHe's the god of stupidity.\n Not that you would know...";
            VictoryEmote = "All according to plan!";

            if(player1)
            {
                helloImg = boardmanager.AthenaImgsLeft[4];
                wowImg = boardmanager.AthenaImgsLeft[5];
                tauntImg = boardmanager.AthenaImgsLeft[6];
            }
            else
            {
                helloImg = boardmanager.AthenaImgsRight[4];
                wowImg = boardmanager.AthenaImgsRight[5];
                tauntImg = boardmanager.AthenaImgsRight[6];
            }

        }
        else if (character == 1)
        {
            //ra's emotes
            HelloEmote = "Good morrow from the Sun.";
            WowEmote = "You shine brighter than the sun!";
            TauntEmote = "Even Anubis couldn't tip \nthe scales in your favor.";
            VictoryEmote = "I... am a star!";

            helloImg = boardmanager.RaImgs[4];
            wowImg = boardmanager.RaImgs[5];
            tauntImg = boardmanager.RaImgs[6];
        }
        else if (character == 2)
        {
            //thor's emotes
            HelloEmote = "Good health, my friend.";
            WowEmote = "By Odin's beard!";
            //TauntEmote = "You can't even spell Meal-near!";
            TauntEmote = "Ha! You can't even spell Mjölnir!";
            VictoryEmote = "Ha! another victory for the mighty Thor!";

            helloImg = boardmanager.ThorImgs[4];
            wowImg = boardmanager.ThorImgs[5];
            tauntImg = boardmanager.ThorImgs[6];
        }
    }

    public void EmoteClicked(int emote)
    {
        //deactivate emote buttons
        emoteButtons.SetActive(false);
        ToggleOpen panelManager = GameObject.FindGameObjectWithTag("panelManager").GetComponent<ToggleOpen>();
        panelManager.SelectedPanel = null;

        //get the emote text
        emoteText = emotePanel.transform.GetChild(0).GetComponent<Text>();

        PlayEmoteAudio(emote);
        StartCoroutine(AnimateLocalEmotePanel(emote));

        int gameIndex = SceneManager.GetActiveScene().buildIndex;
        if (gameIndex == 7)
        {
            //send the emote over the network
            netController = GameObject.FindGameObjectWithTag("network").GetComponent<NetworkControl>();
            string tosend = "emote|" + emote;
            netController.Send(tosend);
        }
    }

    public void OtherEmote(int emote)
    {
        if (!emotesMuted)
        {
            //get the emote panel
            emoteText = emotePanel.transform.GetChild(0).GetComponent<Text>();

            PlayEmoteAudio(emote);
            StartCoroutine(AnimateOtherEmotePanel(emote));
        }
    }

    public void PlayVictoryEmote()
    {
        //get the emote text
        emoteText = emotePanel.transform.GetChild(0).GetComponent<Text>();

        PlayEmoteAudio(3);
        StartCoroutine(AnimateLocalEmotePanel(3));
        //image gets changed in boardmanager
    }

    public void PlayEmoteAudio(int emote)
    {
        //change the text of emote panel and which voiceline to play
        AudioClip emoteClip = new AudioClip();
        if (emote == 0)
        {
            //hello
            emoteText.text = HelloEmote;
            emoteClip = HelloVoices[character];
        }
        else if (emote == 1)
        {
            //wow
            emoteText.text = WowEmote;
            emoteClip = WowVoices[character];
        }
        else if (emote == 2)
        {
            //taunt
            emoteText.text = TauntEmote;
            emoteClip = TauntVoices[character];
        }
        else if (emote == 3)
        {
            //victory
            emoteText.text = VictoryEmote;
            emoteClip = VictoryVoices[character];
        }
        voiceLineLength = emoteClip.length;

        //make sure it will be able to play
        if (emoteSource)
        {
            emoteSource.clip = emoteClip;
            //play the clip
            emoteSource.Play();
            //maybe also play an emote sound?
        }
    }

    IEnumerator AnimateLocalEmotePanel(int emote)
    {
        //activate the emote panel
        emotePanel.SetActive(true);

        //deactivate emotebutton
        Button emoteButton = transform.GetChild(3).GetChild(2).GetComponent<Button>();
        emoteButton.interactable = false;

        //change the image to match the emote
        Image playerImg = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        Sprite old = playerImg.sprite;
        if (emote == 0)
        {
            //hello
            playerImg.sprite = helloImg;
        }
        else if (emote == 1)
        {
            //wow
            playerImg.sprite = wowImg;
        }
        else if (emote == 2)
        {
            //taunt
            playerImg.sprite = tauntImg;
        }

        //animate the emote bubble
        Vector3 oldScale = emotePanel.transform.localScale;
        LeanTween.scale(emotePanel, emotePanel.transform.localScale * 1.2f, 0.1f);
        LeanTween.scale(emotePanel, oldScale, 0.1f).setDelay(.1f);

        yield return new WaitForSeconds(voiceLineLength);
        emotePanel.SetActive(false);

        //change the image back
        playerImg.sprite = old;

        //reactivate emote button
        emoteButton.interactable = true;
    }

    IEnumerator AnimateOtherEmotePanel(int emote)
    {
        //activate the emote panel
        emotePanel.SetActive(true);

        //change the image to match the emote
        Image playerImg = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        Sprite old = playerImg.sprite;
        if (emote == 0)
        {
            //hello
            playerImg.sprite = helloImg;
        }
        else if (emote == 1)
        {
            //wow
            playerImg.sprite = wowImg;
        }
        else if (emote == 2)
        {
            //taunt
            playerImg.sprite = tauntImg;
        }

        //animate the emote bubble
        Vector3 oldScale = emotePanel.transform.localScale;
        LeanTween.scale(emotePanel, emotePanel.transform.localScale * 1.2f, 0.1f);
        LeanTween.scale(emotePanel, oldScale, 0.1f).setDelay(.1f);

        yield return new WaitForSeconds(voiceLineLength);
        emotePanel.SetActive(false);

        //change the image back
        playerImg.sprite = old;
    }
}
