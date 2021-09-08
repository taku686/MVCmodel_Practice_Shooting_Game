public class EnemyModel : CharacterModel
{
    EnemyView view;

    public EnemyModel(GameController controller, EnemyView view)
    {
        this.controller = controller;
        this.view = view;
    }

    public void Update()
    {
        // 更新処理があればこちらに記載します。
        // 本書では何もありませんが、関数を用意しました。
    }
}
