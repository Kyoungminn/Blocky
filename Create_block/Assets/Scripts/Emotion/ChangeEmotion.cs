using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmotion : MonoBehaviour
{
    private SpriteRenderer MainRenderer;
    public Sprite angry;
    public Sprite beard;
    public Sprite happy;
    public Sprite heart;
    public Sprite notgood;
    public Sprite quiet;
    public Sprite sad;
    public Sprite smile;
    public Sprite star;
    public Sprite surprise;
    public Sprite talk;
    public Sprite tired;

    void Start()
    {
        
    }

    private void Update()
    {
        if (MainRenderer == null)
        {
            MainRenderer = GameObject.FindWithTag("myAvatar").GetComponentInChildren<SpriteRenderer>();
        }
    }

    public void Angry()
    {
        MainRenderer.sprite = angry;
    }

    public void Beard()
    {
        MainRenderer.sprite = beard;
    }

    public void Happy()
    {
        MainRenderer.sprite = happy;
    }

    public void Heart()
    {
        MainRenderer.sprite = heart;
    }

    public void Notgood()
    {
        MainRenderer.sprite = notgood;
    }

    public void Quiet()
    {
        MainRenderer.sprite = quiet;
    }

    public void Sad()
    {
        MainRenderer.sprite = sad;
    }

    public void Smile()
    {
        MainRenderer.sprite = smile;
    }

    public void Star()
    {
        MainRenderer.sprite = star;
    }

    public void Surprise()
    {
        MainRenderer.sprite = surprise;
    }

    public void Talk()
    {
        MainRenderer.sprite = talk;
    }
    public void Tired()
    {
        MainRenderer.sprite = tired;
    }
}
