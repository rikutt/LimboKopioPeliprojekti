namespace Barebones2D.NPC
{
    public interface IState
    {
        public void EnterState(NPCHexagonStateManager NPC);
        public void UpdateState(NPCHexagonStateManager NPC);
        public void FixedUpdateState(NPCHexagonStateManager NPC);
        public void ExitState(NPCHexagonStateManager NPC);
    }
}
