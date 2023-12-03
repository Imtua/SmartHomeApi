namespace SmartHomeApi.Data.Queries
{
    public class UpdateDeviceQuery
    {
        public string NewName { get; set; }
        public string NewSerial { get; set; }

        public UpdateDeviceQuery(string newName = null, string newSerial = null)
        {
            NewName = newName;
            NewSerial = newSerial;
        }
    }
}