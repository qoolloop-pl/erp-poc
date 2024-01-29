using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core.Filters;

namespace ErpModule.Trucks.UseCases.List;

public record ListTrucksQuery(TruckListFilter TruckFilter): IQuery<Result<IEnumerable<TruckDto>>>;

