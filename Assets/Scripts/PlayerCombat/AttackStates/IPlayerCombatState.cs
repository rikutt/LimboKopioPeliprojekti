namespace Barebones2D.PlayerCombat
{
    public interface IPlayerCombatState
    {
        public void EnterState(PlayerCombatStateMachine _combatStateMachine);
        public void UpdateState();
        public void FixedUpdateState();
        public void ExitState();
    }
}
