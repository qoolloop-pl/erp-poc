namespace ErpModule.Trucks.Core.Tests.Units;

public class TruckStatus_Transitions_Tests
{
    public static IEnumerable<object[]> PossibleStatusTransitions =>
        new List<object[]>
        {
            new object[] { TruckStatus.OutOfService, TruckStatus.Loading },
            new object[] { TruckStatus.OutOfService, TruckStatus.ToJob },
            new object[] { TruckStatus.OutOfService, TruckStatus.AtJob },
            new object[] { TruckStatus.OutOfService, TruckStatus.Returning },
            new object[] { TruckStatus.OutOfService, TruckStatus.OutOfService },

            new object[] { TruckStatus.Loading, TruckStatus.ToJob },
            new object[] { TruckStatus.Loading, TruckStatus.OutOfService },

            new object[] { TruckStatus.ToJob, TruckStatus.AtJob },
            new object[] { TruckStatus.ToJob, TruckStatus.OutOfService },

            new object[] { TruckStatus.AtJob, TruckStatus.Returning },
            new object[] { TruckStatus.AtJob, TruckStatus.OutOfService },

            new object[] { TruckStatus.Returning, TruckStatus.Loading },
            new object[] { TruckStatus.Returning, TruckStatus.OutOfService },
        };

    [Theory]
    [MemberData(nameof(PossibleStatusTransitions))]
    public void TruckStatus_PossibleMigrations(TruckStatus initialStatus, TruckStatus nextStatus)
    {
        Assert.True(initialStatus.CanMoveTo(nextStatus));
    }

    [Fact]
    public void TrickStatus_OtherNotPossible()
    {
        var allStatuses = TruckStatus.List;

        var allCombinations = new List<(TruckStatus first, TruckStatus second)>();

        foreach (var first in allStatuses)
        {
            allCombinations.AddRange(allStatuses.Select(second => (first, second)));
        }

        foreach (var statusTransitionPair in allCombinations)
        {
            if (IsTransitionInPossiblesList(statusTransitionPair.first, statusTransitionPair.second)) continue;

            Assert.False(statusTransitionPair.first.CanMoveTo(statusTransitionPair.second));
        }
    }

    private bool IsTransitionInPossiblesList(TruckStatus current, TruckStatus next)
    {
        return PossibleStatusTransitions.Any(pair => (TruckStatus)pair[0] == current && (TruckStatus)pair[1] == next);
    }
}
