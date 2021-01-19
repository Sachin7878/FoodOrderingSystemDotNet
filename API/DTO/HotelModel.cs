namespace API.DTO
{
    public class HotelModel
    {
        public long Id { get; set; }
        public string HotelName { get; set; }
        public string MobileNo { get; set; }

        public HotelModel()
        {
            
        }
        public HotelModel(long id, string hotelName, string mobileNo)
        {
            Id = id;
            HotelName = hotelName;
            MobileNo = mobileNo;
        }
    }
}
