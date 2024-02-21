using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;
    public BombController controller;   //Referencia al objeto BombController que maneja las explosiones
                                        //(necesario para saber cuándo se destruye el objeto).



    [Range(0f, 1f)]                        //Probabilidad de que el objeto genere un ítem al destruirse
                                           //(entre 0 y 1, por defecto 0.2).
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;   //Lista de posibles ítems que pueden generarse.


    private void Start()
    {
       

        
    }

    private void OnDestroy()
    {

   //        (Comprueba)
  //        (hay items a generar?)      (Se genero
  //                                 numero aleatorio?)                    (existe el objeto?)

        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance && gameObject)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            //Selecciona un ítem aleatorio de la lista , Crea una instancia del ítem seleccionado en la posición del objeto destruido. 
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
            Destroy(gameObject, destructionTime);
            //Destruye el objeto original con el tiempo especificado en destructionTime
        }
    }
}
