using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public AudioSource BadLuck;
    public AudioSource Bones;
    public AudioSource Bread;
    public AudioSource Can;
    public AudioSource Cat;
    public AudioSource CharacterHit;
    public AudioSource Dice;
    public AudioSource Mirror;
    public AudioSource Nice;
    public AudioSource Scissors;
    public AudioSource Scissors2;
    public AudioSource Umbrella;
    public AudioSource Yes;

    public void PlayBadLuck()
    {
        BadLuck.Play ();
    }
    public void PlayBones()
    {
        Bones.Play ();
    }
    public void PlayBread()
    {
        Bread.Play ();
    }
    public void PlayCan()
    {
        Can.Play ();
    }
    public void PlayCat()
    {
        Cat.Play ();
    }
    public void PlayCharacterHit()
    {
        CharacterHit.Play ();
    }
    public void PlayDice()
    {
        Dice.Play ();
    }
    public void PlayMirror()
    {
        Mirror.Play ();
    }
    public void PlayNice()
    {
        Nice.Play ();
    }
    public void PlayScissors()
    {
        Scissors.Play ();
    }
    public void PlayScissors2()
    {
        Scissors2.Play ();
    }
    public void PlayUmbrella()
    {
        Umbrella.Play ();
    }
    public void PlayYes()
    {
        Yes.Play ();
    }
}
