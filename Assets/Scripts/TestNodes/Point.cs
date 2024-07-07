using System;

namespace TestNodes
{
    public class Point
    {
        public event Action ValueChanged;
        public event Action PointClosed;
        public int Id { get; private set; }
        public ConnectionState ConnectionState { get; set; } = ConnectionState.Disconnected;
        public LogicValue Value 
        {         
            get => _value;
            set
            {
                if (_value == value) return;

                _value = value;
                ValueChanged?.Invoke();
            }
        }

        private LogicValue _value = LogicValue.zero;

        public void SetId(int id) => Id = id;
        public void Close() => PointClosed?.Invoke();
    }

    public enum LogicValue
    {
        zero
    }

    public enum ConnectionState
    {
        Disconnected
    }
}