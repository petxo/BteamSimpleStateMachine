using NUnit.Framework;

namespace Bteam.SimpleStateMachine.Test
{
    [TestFixture]
    public class StateMachineEventTest
    {
        private IStateMachine<TeleponeStatus> _stateMachine;
        private bool _ringingCalled = false;

        [SetUp]
        public void SetUp()
        {
            _stateMachine = StateMachineFactory.Create(TeleponeStatus.OffHook);
            _stateMachine.Permit(TeleponeStatus.OffHook, TeleponeStatus.Ringing, (t => _ringingCalled = true ))
                        .Permit(TeleponeStatus.Ringing, TeleponeStatus.OffHook)
                        .Permit(TeleponeStatus.Ringing, TeleponeStatus.Connected)
                        .Permit(TeleponeStatus.Connected, TeleponeStatus.OffHook)
                        .Permit(TeleponeStatus.Connected, TeleponeStatus.OnHold)
                        .Permit(TeleponeStatus.OnHold, TeleponeStatus.Connected)
                        .Permit(TeleponeStatus.OnHold, TeleponeStatus.OffHook);
        }

        [Test]
        public void StateMachineChangeValidStateAssertValid()
        {
            _stateMachine.ChangeState(TeleponeStatus.Ringing);
            Assert.AreEqual(TeleponeStatus.Ringing, _stateMachine.CurrentState);
            Assert.IsTrue(_ringingCalled);
        } 
        
    }
}