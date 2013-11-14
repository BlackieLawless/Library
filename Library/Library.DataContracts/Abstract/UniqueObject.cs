using System.Runtime.Serialization;

namespace Library.DataContracts.Abstract
{
    [DataContract]
    public abstract class UniqueObject<TIdentificator, TObject> : IUniqueObject<TIdentificator, TObject>
    {
        [DataMember]
        public TIdentificator Id {
            get;
            set;
        }

        public abstract int CompareTo(TObject other);

        public abstract bool Equals(TObject other);

        public abstract int Compare(TObject x, TObject y);
    }
}
