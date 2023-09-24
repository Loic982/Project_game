using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public void OnTriggerEnter2D(Collider2D collision) //collision est l'�l�ment qui est entr� dans la zone
    {
        if (collision.CompareTag("Player"))
        {
            //si c'est bien le joueur qui est entr� dans cette zone : 
            Destroy(objectToDestroy);
            //on va v�rifier si l'�l�ment qui est entr� en collision porte le tag player
            //le tag est une option � mettre dans unity pour identifier un objet sans se baser sur son nom
            //le parent de weakspot c'est l'ennemi
            //On ne veut pas supp weakspot mais l'ennemi (et tous ces sous objet)
        }
    }
}
