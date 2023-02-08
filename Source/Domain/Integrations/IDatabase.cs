using ChristmasTreeDeliveryApp.Contracts.Dto;

namespace ChristmasTreeDeliveryApp.Domain.Integrations
{
    public interface IDatabase
    {
        List<TreeRecordDto>? GetAllRecords();

        List<TreeRecordDto>? GetAllRecords(int type);

        bool SaveOrUpdateRecord(TreeRecordDto order);
    }
}