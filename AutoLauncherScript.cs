using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLauncherScript : MonoBehaviour
{

    public float alturaLanzamiento = 5f;

    public void ActivarLanzamientoAutomatico()
    {

        DiceNumberTextScript.diceNumber = 0;

        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);

        GameObject dado = GameObject.Find("Dice");
        if (dado != null)
        {
            dado.transform.position = new Vector3(0, alturaLanzamiento, 0);
            dado.transform.rotation = Quaternion.identity;

            Rigidbody rbDado = dado.GetComponent<Rigidbody>();

            rbDado.linearVelocity = Vector3.zero;
            rbDado.angularVelocity = Vector3.zero;

            rbDado.AddForce(dado.transform.up * 500);
            rbDado.AddTorque(dirX, dirY, dirZ);
        }
    }
}