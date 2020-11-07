using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Common
{
    public class ApplicationSettings
    {
        public ApplicationSettings() => this.Secret = default!;

        public string Secret { get; private set; }
    }
}
