namespace Library.DataContracts.Abstract
{
    public interface IUniqueKey<T>
    {
        /// <summary>
        /// ���������� �������������, ��������������� ������
        /// </summary>
        T Id {
            get;
            set;
        }
    }
}
