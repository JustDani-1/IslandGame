using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    public ParticleSystem confetti;
    public void makeConfetti() 
    {
        confetti.Play();
    }
}
