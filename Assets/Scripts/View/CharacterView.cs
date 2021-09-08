using UnityEngine;

<<<<<<< HEAD
public class CharacterModel
{
    public GameController controller;   // ゲームコントローラークラスの参照変数
   
}
=======

>>>>>>> 611a35310998388a0e33ad53c83ba6f01b3f7dfd

public class CharacterView : MonoBehaviour
{
    [SerializeField]
    public GameObject HitEffect;    // ヒットエフェクト
    [SerializeField]
    public GameObject shellObj;
    [SerializeField]
    public Transform shotPos;
    [HideInInspector]
    public Collider charaCollider;
 
    public void Init()
    {
        // ヒットエフェクトを非表示
        //  HitEffect.gameObject.SetActive(false);
        charaCollider = GetComponent<Collider>();
        charaCollider.isTrigger = true;
    }

    public virtual GameObject CreateShell()
    {
        return Instantiate(shellObj, shotPos.position, transform.rotation);
    }

    // 勝利時
    public void Win()
    {

    }

    // 敗北時
    public void Lose()
    {
        // ヒットエフェクト表示
        HitEffect.gameObject.SetActive(true);
    }
}
