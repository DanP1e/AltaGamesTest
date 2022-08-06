namespace AltaGamesTest.Scenes
{
    public interface ISceneFinisher
    {
        bool IsGameFinishing { get; }

        void FinishWithDefeat();

        void FinishWithSuccess();
    }
}