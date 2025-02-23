                                                   �y�                                                                                    IGL �  namespace UnityEngine.U2D.Sprites
{
    internal interface IGL
    {
        void PushMatrix();
        void PopMatrix();
        void MultMatrix(Matrix4x4 m);
        void Begin(int mode);
        void End();
        void Color(Color c);
        void Vertex(Vector3 v);
    }

    internal class GLSystem : IGL
    {
        static IGL m_GLSystem;
        internal static void SetSystem(IGL system)
        {
            m_GLSystem = system;
        }

        internal static IGL GetSystem()
        {
            if (m_GLSystem == null)
                m_GLSystem = new GLSystem();
            return m_GLSystem;
        }

        public void PushMatrix()
        {
            GL.PushMatrix();
        }

 