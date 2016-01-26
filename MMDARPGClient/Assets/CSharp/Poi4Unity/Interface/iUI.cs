using Poi;

namespace UnityEngine
{
    public interface iUI:iLabel
    {
        Only IsOnly { get; }

        GameObject gameObject { get; }

        void UseDone();
        void SetDepth();
        void Refresh();
    }
}