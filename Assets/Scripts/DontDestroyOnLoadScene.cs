using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{

    public GameObject[] objects; //tableau d'élements à ne pas supp qd on change de scène
    void Awake() //"Awake" pour quelle soit lue dès le début
    {
        foreach (var element in objects) //boucle : pour chaque element dans objects
        {
            DontDestroyOnLoad(element); //on les supp pas
        }
    }

}
