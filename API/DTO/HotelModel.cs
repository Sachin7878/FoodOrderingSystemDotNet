using API.Models;

namespace API.DTO
{
    public class HotelModel
    {
        public long Id { get; set; }
        public string HotelName { get; set; }
        public string MobileNo { get; set; }

        public HotelModel(Hotel hotel)
        {
            Id = hotel.Id;
            HotelName = hotel.HotelName;
            MobileNo = hotel.MobileNo;
        }
    }
}
