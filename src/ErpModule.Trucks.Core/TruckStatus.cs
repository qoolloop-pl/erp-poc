using Ardalis.SmartEnum;

namespace ErpModule.Trucks.Core;

public abstract class TruckStatus : SmartEnum<TruckStatus>
{
    /// <summary>
    /// Status can be moved to any TruckStatus
    /// </summary>
    public static readonly TruckStatus OutOfService = new OutOfServiceStatus();
    public static readonly TruckStatus Loading = new LoadingStatus();
    public static readonly TruckStatus ToJob = new ToJobStatus();
    public static readonly TruckStatus AtJob = new AtJobStatus();
    public static readonly TruckStatus Returning = new ReturningStatus();

    protected TruckStatus(string name, int value) : base(name, value)
    {
    }

    public abstract bool CanMoveTo(TruckStatus nextStatus);

    public static bool TryParse(string name, out TruckStatus status)
    {
        return TryFromName(name, true, out status);
    }

    private sealed class OutOfServiceStatus: TruckStatus
    {
        public OutOfServiceStatus() : base("OutOfService", 0)
        {
        }

        public override bool CanMoveTo(TruckStatus nextStatus) => true;
    }

    private sealed class LoadingStatus: TruckStatus
    {
        public LoadingStatus() : base("Loading", 1)
        {
        }

        public override bool CanMoveTo(TruckStatus nextStatus) =>
            nextStatus == OutOfService || nextStatus == ToJob;
    }

    private sealed class ToJobStatus: TruckStatus
    {
        public ToJobStatus() : base("ToJob", 2)
        {
        }

        public override bool CanMoveTo(TruckStatus nextStatus) =>
            nextStatus == OutOfService || nextStatus == AtJob;
    }

    private sealed class AtJobStatus: TruckStatus
    {
        public AtJobStatus() : base("AtJob", 3)
        {
        }

        public override bool CanMoveTo(TruckStatus nextStatus) =>
            nextStatus == OutOfService || nextStatus == Returning;
    }

    private sealed class ReturningStatus: TruckStatus
    {
        public ReturningStatus() : base("Returning", 4)
        {
        }

        public override bool CanMoveTo(TruckStatus nextStatus) =>
            nextStatus == OutOfService || nextStatus == Loading;
    }
}
