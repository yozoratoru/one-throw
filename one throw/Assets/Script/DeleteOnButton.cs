using UnityEngine;

public class DeleteOnButton : MonoBehaviour
{
    public GameObject buttonObject; // 対象のボタン（GameObject）

    void Start()
    {
        // ゲーム開始時にボタンを非表示
        buttonObject.SetActive(false);
    }
    public GameObject target; // 削除したいオブジェクトをインスペクターで指定

    public void DeleteTarget()
    {
        Destroy(target);
        buttonObject.SetActive(true);
    }
}