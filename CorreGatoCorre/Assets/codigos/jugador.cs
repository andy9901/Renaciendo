using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugador : MonoBehaviour
{
    public float FuerzaSalto = 5f; //Declaramos la fuerza con la que nuestro personaje podra saltar
    bool EnElPiso = true; // Dectecta si el personaje esta en el suelo, evita saltar infinitamente
    
    public GameObject Jugador;
    private Rigidbody2D rb;

    float incrementoFinal = 0f;
    float timer = 0f;
    public int vidaTotal=2;
    //int vidaP;

    // Esta funcion se ejecuta una unica vez al inicio del juego
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        incrementoFinal = Incremento(0f, 0.5f);
    }

    // Update se ejecuta contunuamente cada frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EnElPiso == true)
        {
            Salto();
        }
        if(vidaTotal<=0){
            Destroy(Jugador);
        }
        timer += Time.deltaTime;
        //Debug.Log("Mi incremento es: " + incrementoFinal);
        rb.velocity = new Vector2(incrementoFinal + Time.deltaTime, rb.velocity.y);

    }

    void Salto()
    {
        // Aplica una fuerza hacia arriba al personaje
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * FuerzaSalto, ForceMode2D.Impulse);
        EnElPiso = false;

    }
    //Incremento obtiene la velocidad base para el jugador de forma recursiva
    float Incremento(float number, float increment)
    {
        if(timer % 2 == 0){
            if (number >= 10)
                return number;
            else
                return Incremento(number + increment, increment);
        }
        else{
            
            return Incremento(number + increment, increment);
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Obstaculo"){
            vidaTotal=Lamuertearribo(1000);
            //vidaTotal=vidadelpersonaje(vidaTotal,100);
            Debug.Log("Chocaste con una piedra jaja\n"+vidaTotal);
            
        }
        // Si el personaje colisiona con el suelo, cambiamos EnElPiso a true
        if (collision.gameObject.tag == "Suelo")
        {
            EnElPiso = true;
        }
    }
    int Lamuertearribo(int number){
        //Debug.Log(number);
        if(number>0){
            number=Lamuertearribo(number-1);
        }
        return(number);
    }
//Vida del personaje
    /*int vidadelpersonaje (int vida, int vidaP){
        if(vidaP>0){
            vida=vidadelpersonaje(vida--,vidaP--);
        }
        return(vida);
        
    }*/
}
