namespace Barebones2D.PlayerCombat
{
    public interface IPlayerCombatState
    {
        public void EnterState(PlayerManager _playerManagerInstance, PlayerCombatStateMachine _combatStateMachine);
        public void UpdateState();
        public void FixedUpdateState();
        public void ExitState();
    }
}
