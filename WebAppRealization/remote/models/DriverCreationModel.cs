using System.Collections.Generic;

namespace WebAppIImpl.remote.models
{
    public class DriverCreationModel
    {
        public string? Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<CarModel> Cars { get; set; }
    }
}