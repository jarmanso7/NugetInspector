using System.Collections.Generic;

namespace NugetInspector.Packages
{
    public class PackagesEqualityComparer : IEqualityComparer<Package>
    {
        public bool Equals(Package x, Package y)
        {
            return (string.Equals(x.Name, y.Name)) &&
                   (string.Equals(x.Version, y.Version));
        }

        public int GetHashCode(Package obj)
        {
            return (string.Concat(obj.Name, obj.Version)).GetHashCode();
        }
    }
}
