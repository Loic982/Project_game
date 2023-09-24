using UnityEngine;
using System.Collections; //pour la coroutine (d�callage entre le fondu et le changement de sc�ne)
using UnityEngine.SceneManagement; //permet de charger une sc�ne

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    public Animator fadeSystem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(loadNextScene());
        }
    }
    public IEnumerator loadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");  //systeme de fondu (cf. animator)
        yield return new WaitForSeconds(1f); //yield = pause et ici on va attendre 1s
        SceneManager.LoadScene(sceneName); //va charger la sc�ne num2
            //a la place de mettre "Level02", on peut mettre l'index (comme renseign� si on passe la souris dans les parenth�ses
            //pour �a il faut aller dans :
            //Unity -> File -> Build settings et l'index et � droite des sc�nes :
            //Level01 -> 0
            //Level02 -> 1
            //Donc je peux mettre "SceneManager.LoadScene(1);"
            //Mais l'index d�pend de l'ordre dans laquelle les sc�nes ont �t� rang� (cf. L13) donc vaut mieux se baser sur le nom
            //Update!!! On utilise une variable "sceneName" pour le nom de la sc�ne

            //Mais ici si on passe d'une sc�ne � une autre, la vie ainsi que le nbr de pi�ce est reboot! il n'y a pas de lien entre les sc�nes
    }
}
