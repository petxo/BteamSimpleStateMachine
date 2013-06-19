using NUnit.Framework;

namespace Bteam.SimpleStateMachine.Test
{
    [TestFixture]
    public class StateMachineTest
    {
        private IStateMachine<TeleponeStatus> _stateMachine;

        [SetUp]
        public void SetUp()
        {
            _stateMachine = StateMachineFactory.Create(TeleponeStatus.OffHook);
            _stateMachine.Permit(TeleponeStatus.OffHook, TeleponeStatus.Ringing)
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
        }

        [Test, ExpectedException]
        public void StateMachineChangeInvalidStateAssertException()
        {
            _stateMachine.ChangeState(TeleponeStatus.Connected);
        }

        [Test]
        public void StateMachineCanChangeValidStateAssertTrue()
        {
            Assert.IsTrue(_stateMachine.CanChangeState(TeleponeStatus.Ringing));
        }

        [Test]
        public void StateMachineChangeInvalidStateAssertFalse()
        {
            Assert.IsFalse(_stateMachine.CanChangeState(TeleponeStatus.Connected));
        }


        [Test]
        public void StateMachineCompletedFlowAssertValid()
        {
            _stateMachine.ChangeState(TeleponeStatus.Ringing);
            Assert.AreEqual(TeleponeStatus.Ringing, _stateMachine.CurrentState);

            _stateMachine.ChangeState(TeleponeStatus.Connected);
            Assert.AreEqual(TeleponeStatus.Connected, _stateMachine.CurrentState);
            
            _stateMachine.ChangeState(TeleponeStatus.OffHook);
            Assert.AreEqual(TeleponeStatus.OffHook, _stateMachine.CurrentState);
        }
    }
}
