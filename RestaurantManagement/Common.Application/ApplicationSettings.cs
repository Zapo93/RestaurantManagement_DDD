using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Common.Application
{
    public class ApplicationSettings
    {
        public ApplicationSettings() => this.Secret = default!;

        public string Secret { get; private set; }
    }
}
