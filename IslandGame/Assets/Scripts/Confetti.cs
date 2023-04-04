using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to play the confetti effect at end of the game
public class Confetti : MonoBehaviour
{
    public ParticleSystem confetti;
    public void makeConfetti() 
    {
        confetti.Play();
    }
}
