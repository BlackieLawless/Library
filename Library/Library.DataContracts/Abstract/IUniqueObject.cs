using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Library.DataContracts.Abstract
{
    /// <summary>
    /// ���������� ������
    /// </summary>
    public interface IUniqueObject<T> : IComparer<T>, IComparable<T>, IEquatable<T>
    {

    }

    /// <summary>
    /// ���������� ������, ������� ���������� �������������
    /// </summary>
    public interface IUniqueObject<T, V> : IUniqueKey<T>, IUniqueObject<V>
    {

    }
}
