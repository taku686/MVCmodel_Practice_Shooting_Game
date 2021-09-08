using UnityEngine;



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
