using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
 public Rigidbody rb3D;


	// Funçao de atualizaçao quadro a quadro
    void Update () {



GameObject Paraquedas;
GameObject CorpoNariz;
GameObject Fogo;

CorpoNariz = GameObject.Find("Corpo_Nariz");
Paraquedas = GameObject.Find("Paraquedas");
Fogo = GameObject.Find("Particle System");

 rb3D = GetComponent<Rigidbody>();

//timer
float myTimer  = 5.0f;

if(myTimer > 0){
myTimer = Time.deltaTime;
}
if(myTimer <= 0){
Debug.Log ("GAME OVER");
}


//girar foguete
//velocidade constante
//começar fogo apos start


 
        // So loga as informaçoes
        Debug.Log (GetComponent<Rigidbody>().velocity.magnitude);
        Debug.Log (noChao ());

        // Acelera se o jogador apertar a tecla ESPACO
        if(Input.GetKeyDown(KeyCode.Space))
        {
            acelera ();
        
GetComponent<AudioSource>().Play();

}
        // Quando detectar que a velocidade inverteu (ficou negativa no y e magnitude bem pequena,
        // ou seja, o foguete parou la no alto), aplica uma força para girar.
        if (!noChao () && GetComponent<Rigidbody>().velocity.y < 0 && GetComponent<Rigidbody>().velocity.magnitude <= 1)
            //gira();
        {
        Destroy (CorpoNariz);
    Destroy (Fogo);

            StartCoroutine(fazRetorno());
            Paraquedas.GetComponent<Renderer>().enabled = true;

            //suavizar descida
            GetComponent<Rigidbody>().drag = 5;
        }
    }

    // Acelera se o jogador clicar no foguete
    void OnMouseDown() {
        acelera();
    }

    // Acelera o foguete aplicando uma força para a sua direçao "cima".
    void acelera() {


//balistico
    	//Vector3 forca = (transform.up * 1000) + (transform.right * 150);
        //GetComponent<Rigidbody>().AddForce(forca);


         GetComponent<Rigidbody>().AddForce(transform.up * 800);
    }




    // Gira o foguete aplicando uma força para a sua direçao direita a partir de seu topo.
    void gira() {
        Vector3 ponta = transform.position;
        ponta.y += GetComponent<Renderer>().bounds.size.y / 2;
        GetComponent<Rigidbody>().AddForceAtPosition (transform.right * 20, ponta);
    }

    // Indica se o foguete esta colidindo com o chao
    bool noChao() {
        return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f);
    }

    // Gira o foguete 180 graus sobre o eixo X, fazendo-o inverter completamente a direçao de subida
    IEnumerator fazRetorno() {

        Quaternion angulo = Quaternion.Euler(180, 0, 0);
        float velocidade = 0.5f;

        while(Quaternion.Angle(transform.rotation, angulo) > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, angulo, Time.deltaTime * velocidade);
            yield return new WaitForEndOfFrame();
        }  
        transform.rotation = angulo;
    }





}