﻿using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller.Utilities;

namespace controller
{
    public class DeliveryController : abstractController
    {
        private readonly Database _database = new Database();

        public string GetDeliveryMap(string orderId, Size imageSize)
        {
            var orderStatus = ExecuteScalar("SELECT status FROM order_ WHERE OrderID = @orderId",
                new Dictionary<string, object> { { "@orderId", orderId } });
            string location = orderStatus != "Processing" ? GetShippedAddress() : GetDeliveryRelay(orderId);
            return string.IsNullOrEmpty(location) ? location : GenerateMapUrl(location, orderId, imageSize);
        }

        private string GetApiKey()
        {
            return "AIzaSyCvDbMpDYOev7-eygdiIP0e9xG-gPV18H8"; //TODO: Change this to the API key from the config file
        }

        public string GenerateMapUrl(string location, string orderId, Size imageSize)
        {
            string width = $"{imageSize.Width}";
            string height = $"{imageSize.Height}";
            string relayId = ExecuteScalar("SELECT DeliveryRelayID FROM order_ WHERE OrderID = @orderId",
                new Dictionary<string, object> { { "@orderId", orderId } });
            string label = ExecuteScalar("SELECT RelayName FROM deliveryrelay WHERE RelayID = @relayId",
                new Dictionary<string, object> { { "@relayId", relayId } });

            return $"https://maps.googleapis.com/maps/api/staticmap?center={location}&zoom=15" +
                   $"&size={width}x{height}" +
                   $"&maptype=roadmap" +
                   $"&markers=color:red%7Clabel:{label}%7C{location}" +
                   $"&key={GetApiKey()}";
        }
        private string GetShippedAddress()
        {
            return "22.390715003644328, 114.19828146266907";
        }

        private string GetReadyToShipAddress()
        {
            return GetShippedAddress();
        }

        private string GetLocation(string orderId)
        {
            var orderStatus = GetOrderStatus(orderId);
            return orderStatus != "Processing" ? "" : GetDeliveryRelay(orderId);
        }

        private string GetOrderStatus(string orderId) =>
            ExecuteScalar("SELECT status FROM order_ WHERE OrderID = @orderId",
                new Dictionary<string, object> { { "@orderId", orderId } });

        private string GetDeliveryRelay(string orderId)
        {
            var relayId = GetDeliveryRelayId(orderId);
            return ExecuteScalar("SELECT CONCAT(latitude, ',', longitude) FROM deliveryrelay WHERE RelayID = @relayId",
                new Dictionary<string, object> { { "@relayId", relayId } });
        }

        private string GetDeliveryRelayId(string orderId)
        {
            return ExecuteScalar("SELECT DeliveryRelayID FROM order_ WHERE OrderID = @orderId",
                new Dictionary<string, object> { { "@orderId", orderId } });
        }

        private string GetLocationName(string relayId)
        {
            return ExecuteScalar("SELECT RelayName FROM deliveryrelay WHERE RelayID = @relayId",
                new Dictionary<string, object> { { "@relayId", relayId } });
        }

        public int GetRelayId(string relayName)
        {
            return int.Parse(ExecuteScalar("SELECT RelayID FROM deliveryrelay WHERE RelayName = @relayName",
                new Dictionary<string, object> { { "@relayName", relayName } }));
        }

        public List<string> GetCitiesByProvince(string selectedProvince)
        {
            return GetListFromDatabase("SELECT DISTINCT city FROM deliveryrelay WHERE province = @provinceName",
                new Dictionary<string, object> { { "@provinceName", selectedProvince } });
        }

        public List<string> GetRelayNamesByCity(string selectedCity)
        {
            return GetListFromDatabase("SELECT RelayName FROM deliveryrelay WHERE city = @city",
                new Dictionary<string, object> { { "@city", selectedCity } });
        }

        private List<string> GetListFromDatabase(string query, Dictionary<string, object> parameters)
        {
            var dataTable = ExecuteDataTable(query, parameters);
            return dataTable.AsEnumerable().Select(row => row[0].ToString()).ToList();
        }

        private string ExecuteScalar(string query, Dictionary<string, object> parameters)
        {
            return _database.ExecuteScalarCommand(query, parameters).ToString();
        }

        private DataTable ExecuteDataTable(string query, Dictionary<string, object> parameters)
        {
            return _database.ExecuteDataTable(query, parameters);
        }

        public string GenerateMapUrl(string location, Size imageSize, string relayName)
        {
            string width = $"{imageSize.Width}";
            string height = $"{imageSize.Height}";

            return $"https://maps.googleapis.com/maps/api/staticmap?center={location}&zoom=15" +
                   $"&size={width}x{height}" +
                   $"&maptype=roadmap" +
                   $"&markers=color:red%7Clabel:{relayName}%7C{location}" +
                   $"&key={GetApiKey()}";
        }

        public async Task LoadImageAsync(string url, PictureBox pictureBox)
        {
            using (HttpClient client = new HttpClient())
            {
                using (Stream stream = await client.GetStreamAsync(url))
                {
                    pictureBox.Image = new Bitmap(stream);
                }
            }
        }

        public string GetRelayLocation(string relayName)
        {
            var dt = _database.ExecuteDataTable("SELECT * FROM deliveryrelay WHERE RelayName = @relayName",
                new Dictionary<string, object> { { "@relayName", relayName } });
            return $"{dt.Rows[0]["latitude"]},{dt.Rows[0]["longitude"]}";
        }

        public void EditRelay(string orderId, int relayId)
        {
            var sql = "SELECT DeliveryRelayID FROM order_ WHERE OrderID = @orderId";
            _database.ExecuteDataTable(sql, new Dictionary<string, object> { { "@orderId", orderId } });

            sql = "UPDATE order_ SET DeliveryRelayID = @relayId WHERE OrderID = @orderId";

            _database.ExecuteNonQueryCommand(sql,
                new Dictionary<string, object> { { "@orderId", orderId }, { "@relayId", relayId } });
        }

        public List<string> GetProvinceList()
        {
            return GetListFromDatabase("SELECT DISTINCT province FROM deliveryrelay", null);
        }
    }
}