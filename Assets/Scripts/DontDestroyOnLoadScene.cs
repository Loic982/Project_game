using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{

    public GameObject[] objects; //tableau d'�lements � ne pas supp qd on change de sc�ne
    void Awake() //"Awake" pour quelle soit lue d�s le d�but
    {
        foreach (var element in objects) //boucle : pour chaque element dans objects
        {
            DontDestroyOnLoad(element); //on les supp pas
        }
    }

}
