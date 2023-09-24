using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; //d�fini un joueur (� ajouter apr�s avec le compenent)
    public float timeOffset; //d�finit un temps de d�callage entre le mvt du joueur et le mvt de la cam
    public Vector3 posOffset; //d�finit un vecteur (x,y,z) de position de la cam

    private Vector3 velocity; //pour en faire une ref apr�s (c'est comme �a que fonctionne vs)
    void Update() //m�tode
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
        // la position de la cam(transform.position) c'est un d�placement "smooth" sur un vecteur x,y et z
        // en prenant en compte la position de la cam
        // la position du joueur (par rapport � la cam) / le d�callage 
        // une r�f de vitesse (par d�faut)
        // un temps de d�callage
    }
}
