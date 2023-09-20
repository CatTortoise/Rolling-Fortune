interface ICollectable
{
    static string COLLECTABLE_TAG = "Collectable";

    void OnCollect(Player collector);
}
