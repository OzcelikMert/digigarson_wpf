using System.Linq;
using System.Net.NetworkInformation;

namespace Digigarson.Classes.JSInterfaces
{
    public class Values
    {
        public string Token {
            get { return "CSharp"; }
        }

        public string getMacAddress {
            get {
                return NetworkInterface.GetAllNetworkInterfaces().Where(
                    networkInterFace => 
                        networkInterFace.OperationalStatus == OperationalStatus.Up && 
                        networkInterFace.NetworkInterfaceType != NetworkInterfaceType.Loopback
                ).Select(
                    networkInterFace => 
                        networkInterFace.GetPhysicalAddress().ToString()
                ).FirstOrDefault();
            }
        }
    }
}
