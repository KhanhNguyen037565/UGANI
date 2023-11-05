using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ugani_Restaurant.Models
{
    public class CustomDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Định dạng kiểu DateTime thành chuỗi theo định dạng bạn muốn
            var dateTime = (DateTime)value;
            var formattedDate = dateTime.ToString("dd/MM/yyyy");
            writer.WriteValue(formattedDate);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Viết mã chuyển đổi ngược lại nếu cần thiết
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            // Xác định xem kiểu dữ liệu có thể chuyển đổi bởi chuyển đổi này hay không
            return objectType == typeof(DateTime);
        }
    }

}